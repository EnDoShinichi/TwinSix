using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EventPhase : MonoBehaviour,IPhase
{
    public event Action NextPhase;

    [SerializeField] private AudioSource moneySouce;
    [SerializeField] private AudioClip moneySE;

    PhotonView view;
    bool startFlg;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        startFlg = false;
        moneySouce = GetComponent<AudioSource>();
    }

    [PunRPC]
    public void PhaseCompleatesynchronize(int number)
    {
        GameStatus.lockMenber.CompleateScene(number);
    }

    [PunRPC]
    public void PhaseEnd()
    {
        startFlg = false;
        if (PhotonNetwork.IsMasterClient) NextPhase();
    }

    [PunRPC]
    public void PhaseStart(PlayerStatus turnObject)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber - 1 != turnObject.id)
        {
            view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        }
        GameStatus.lockMenber.TargetListBind_Player(turnObject);
        if (turnObject.myMapPosition.mapEventData.EventTypeGet() == EventType.MONEY)
        {
            Debug.Log("MoneySEÇ»Ç¡ÇƒÇ‹ÇüÇüÇüÇüÇ∑ÅI");
            moneySouce.PlayOneShot(moneySE);
        }
        turnObject.myMapPosition.mapEventData.MapEvent();
        startFlg = true;
        StartCoroutine(stop());
    }

    [PunRPC]
    public void PhaseUpdate()
    {
        if (!startFlg) return;
        if (GameStatus.lockMenber.CompleateCheck()) PhaseEnd();
    }

    IEnumerator stop()
    {
        yield return new WaitForSeconds(2);
        view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
    }
}
