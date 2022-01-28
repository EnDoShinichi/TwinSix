using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MovePhase : MonoBehaviour,IPhase
{
    public event Action NextPhase;
    public PlayerStatus playerStatus;

    [SerializeField] private float waitTime;
    [SerializeField] private MapInfoScriptableObject playerMap;

    private bool choiceFlg;
    PhotonView view;
    bool startFlg;
    private void Start()
    {
        //システムのランダムとUnityが競合した
        //int random = UnityEngine.Random.Range(1, 6);
        //int random1 = UnityEngine.Random.Range(1, 6);
        //playerStatus.SetDice(3);
        //playerStatus.SetDoubtDice(4);
        //playerStatus.SetDoubt(false);

        //playerStatus.SetMap(playerMap);

        //Debug.Log($"真({playerStatus.dice})");
        //Debug.Log($"偽({playerStatus.doubtDice})");
        //Debug.Log(playerStatus.doubt);
        //Debug.Log(playerStatus.myMapPosition.nextMapData.Length);

        //PhaseStart(playerStatus);
        view = GetComponent<PhotonView>();
        startFlg = false;
    }

    [PunRPC]
    public void PhaseEnd()
    {
        
    }

    [PunRPC]
    public void PhaseStart(PlayerStatus turnObject)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber - 1 != turnObject.id) view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        // if (GameStatus.lockMenber.IsCompleate(PhotonNetwork.LocalPlayer.ActorNumber - 1)) return;
        playerStatus = turnObject;

        if (playerStatus.doubt) StartCoroutine(PlayerSetMapDoubt());
        else StartCoroutine(PlayerSetMapTruth());
    }

    [PunRPC]
    public void PhaseUpdate()
    {
        if (!startFlg) return;
        if (GameStatus.lockMenber.CompleateCheck() && PhotonNetwork.IsMasterClient)
        {
            startFlg = false;
            NextPhase();
        }
    }

    /// <summary>
    /// サイコロの目が嘘
    /// </summary>
    IEnumerator PlayerSetMapDoubt()
    {
        // 嘘の数値分、進む
        for (int i = 0; i < playerStatus.doubtDice; i++)
        {
            //playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);

            ChoiceRoad();

            if (choiceFlg == true) break;

            if (playerStatus.myMapPosition.mapEventData.EventTypeGet() == EventType.STAND) break;

            yield return new WaitForSeconds(waitTime);

            playerStatus.Object.transform.position = playerStatus.myMapPosition.mapPosition;
            Debug.Log("asd");
        }
        playerStatus.AddDoubtCount(1);
        choiceFlg = false;
        view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);

        startFlg = true;
    }

    /// <summary>
    /// サイコロの目が正しい
    /// </summary>
    IEnumerator PlayerSetMapTruth()
    {
        // サイコロの出た目通りに進む
        for (int i = 0; i < playerStatus.dice; i++)
        {
            // Vector3 nextPos = playerStatus.myMapPosition.nextMapData[0];
            // 今の場所からnextPosに進む処理
            //playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);

            ChoiceRoad();

            if (choiceFlg == true) break;
            
            if (playerStatus.myMapPosition.mapEventData.EventTypeGet() == EventType.STAND) break;

            yield return new WaitForSeconds(waitTime);

            playerStatus.Object.transform.position = playerStatus.myMapPosition.mapPosition;
            Debug.Log(playerStatus.myMapPosition.mapName);
        }
        choiceFlg = false;
        view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);

        startFlg = true;
    }

    /// <summary>
    /// 分かれ道があるかどうか
    /// </summary>
    void ChoiceRoad()
    {
        if (playerStatus.myMapPosition.nextMapData.Length >= 2)
        {
            choiceFlg = true;

            // 進行方向を決める処理を追加しないと行けない
        }
        else playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);
    }

    [PunRPC]
    public void PhaseCompleatesynchronize(int number)
    {
        GameStatus.lockMenber.CompleateScene(number);
    }
}
