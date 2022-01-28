using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DoubtPhase : MonoBehaviour,IPhase
{
    enum Doubt_State
    { 
        THINKING,
        CONFIRMATION,
        OPEN,
        END,
        LENGTH
    }

    public event Action NextPhase;

    PlayerStatus status;
    Doubt_State state;
    bool check;
    bool startFlg;

    [SerializeField] private AudioSource doubtsource;
    [SerializeField] private AudioClip doubtSE;

    PhotonView view;
    private void Start()
    {
        startFlg = false;
        view = GetComponent<PhotonView>();
        doubtsource = GetComponent<AudioSource>();
    }

    [PunRPC]
    public void PhaseEnd()
    {
        startFlg = false;
        GameStatus.lockMenber.DrawMessage("");
        if (PhotonNetwork.IsMasterClient) NextPhase();
    }

    [PunRPC]
    public void PhaseStart(PlayerStatus turnObject)
    {
        status = turnObject;
        state = Doubt_State.THINKING;
        check = true;
        startFlg = true;
    }

    [PunRPC]
    public void PhaseUpdate()
    {
        if (!startFlg)
        {
            Debug.Log("Startを通過していません");
            return;
        }
        if (GameStatus.lockMenber.CompleateCheck())
        {
            Debug.Log("oh");
            PhaseEnd();
        }
        if (GameStatus.lockMenber.IsCompleate(PhotonNetwork.LocalPlayer.ActorNumber - 1))
        {
            GameStatus.lockMenber.DrawMessage("他プレイヤーの入力を待っています...");
            return;
        }
        if (status.id == PhotonNetwork.LocalPlayer.ActorNumber - 1)
        {
            Debug.Log("this is Compleate");
            view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            return;
        }

        switch (state)
        {
            case Doubt_State.THINKING:
                GameStatus.lockMenber.DrawMessage("ウソをついていると思う場合Enterキーを\nついていないと思う場合はBackSpaceキーを押してください");
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    check = true;
                    state = Doubt_State.CONFIRMATION;
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    check = false;
                    state = Doubt_State.CONFIRMATION;
                }
                break;
            case Doubt_State.CONFIRMATION:
                if (check) GameStatus.lockMenber.DrawMessage("ウソをついている　でいいですか?\nEnterキー　はい\nBackSpaceキー　いいえ");
                else GameStatus.lockMenber.DrawMessage("ウソをついていない　でいいですか?\nEnterキー　はい\nBackSpaceキー　いいえ");

                if (Input.GetKeyDown(KeyCode.Return)) state = Doubt_State.OPEN;
                else if (Input.GetKeyDown(KeyCode.Backspace)) state = Doubt_State.THINKING;
                break;
            case Doubt_State.OPEN:
                if (!check)
                {
                    view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                    state = Doubt_State.END;
                }
                else
                {
                    if (status.doubt)
                    {
                        for (int i = 0; i < GameStatus.MAX_PLAYER_NUMBER; i++)
                        {
                            if (i != PhotonNetwork.LocalPlayer.ActorNumber - 1) view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, i);
                        }
                        status.SetDoubt(false);
                        status.AddMoney((status.money / 2) * -1);
                        GameStatus.lockMenber.DrawMessage("お見事！ウソを見破った!");
                    }
                    else
                    {
                        doubtsource.PlayOneShot(doubtSE);
                        GameStatus.lockMenber.DrawMessage("残念!ウソをついていなかった...");
                    }

                    StartCoroutine(stop());
                    state = Doubt_State.END;
                }
                break;
        }
    }

    private IEnumerator stop()
    {
        yield return new WaitForSeconds(3);
        view.RPC(nameof(PhaseCompleatesynchronize),RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
    }

    [PunRPC]
    public void PhaseCompleatesynchronize(int number)
    {
        GameStatus.lockMenber.CompleateScene(number);
    }
}
