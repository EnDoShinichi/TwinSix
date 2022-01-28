using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwinSix
{
    public class DicePhase : MonoBehaviour, IPhase
    {
        enum Dice_State
        {
            DICEROLL,
            DOUBTCHECK,
            CONFIRMATION,
            END,
            LENGTH
        }

        [SerializeField] DiceRoll dice;
        [SerializeField] PlayerStatus player;
        [SerializeField] private GameObject buttonObject; // ボタン格納オブジェクト
        [SerializeField] private GameObject numberButtonObject; // true,falseを表示したのち、どの番号の実行するかのボタンが格納されたオブジェクト

        PlayerStatus thisTurnstatus; // このターンのプレイヤーステータス
        DiceRoll diceRoll;

        int setDoubtNumber = 1; // 嘘の値を保存しておく変数
        int diceEyeNum = 0;
        bool CountTime = true; // カウントコルーチンが実行されているかどうか

        public event Action NextPhase; // 次のフェーズへ移行する関数
        int money = 0;
        int ID = 0;
        int doubtCount = 0;
        string name = "";
        MapInfoScriptableObject defaultPosition;
        PlayerStatus status;

        Dice_State state = Dice_State.DICEROLL;
        int doubtNumber;
        PhotonView view;
        bool startFlg;

        private void Start()
        {
            view = GetComponent<PhotonView>();
            diceRoll = dice.GetComponent<DiceRoll>();
            startFlg = false;
        }

        [PunRPC]
        public void PhaseEnd()
        {
            Debug.Log("dicePhase_End");
            GameStatus.lockMenber.DrawMessage("");
            startFlg = false;
            //if (setDoubtNumber == -1) player.SetDoubt(false); // もし嘘と宣言したがその後値の更新がされなかった場合、嘘をついてなかったことに書き換える
            //if (player.doubt) player.SetDoubtDice(setDoubtNumber); // もし嘘をついていたなら、虚偽の振った値をプレイヤーステータスに設定する

            //// 各ボタンの非表示
            //buttonObject.SetActive(false);
            //numberButtonObject.SetActive(false);
            if (PhotonNetwork.IsMasterClient) NextPhase();
        }

        [PunRPC]
        public void PhaseStart(PlayerStatus status)
        {
            state = Dice_State.DICEROLL;
            doubtNumber = 0;
            player = status;
            if (status.id == PhotonNetwork.LocalPlayer.ActorNumber - 1) diceRoll.DiceOn();
            GameStatus.lockMenber.DrawMessage($"{status.playerName}の手番です");
            Debug.Log("dicePhase" + GameStatus.lockMenber.playingNumber);
            setDoubtNumber = -1; // 値を入力されていない状態に
            thisTurnstatus = status; // プレイヤーステータスを更新
            startFlg = true;
        }

        [PunRPC]
        public void PhaseUpdate()
        {
            if (!startFlg) return;
            if (GameStatus.lockMenber.CompleateCheck())
            {
                PhaseEnd();
                startFlg = false;
                return;
            }

            if (GameStatus.lockMenber.IsCompleate(PhotonNetwork.LocalPlayer.ActorNumber - 1))
            {
                GameStatus.lockMenber.DrawMessage("他プレイヤーの入力を待っています...");
                return;
            }
            if (player.id != PhotonNetwork.LocalPlayer.ActorNumber - 1)
            {
                view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                //GameStatus.lockMenber.CompleateScene(PhotonNetwork.LocalPlayer.ActorNumber - 1);
                return;
            }

            switch (state)
            {
                case Dice_State.DICEROLL:
                    if (diceRoll.GetDiceEyes() == 0) return;

                    Debug.Log("PhaseStart");
                    //int rand = UnityEngine.Random.Range(1, 7); // ランダムな値を生成
                    diceEyeNum = diceRoll.GetDiceEyes();
                    Debug.Log("aaaa" + diceRoll.GetDiceEyes());
                    player.SetDice(diceEyeNum); // このターンのダイスの値に設定

                    //if (player.id == GameStatus.lockMenber.myNumber) buttonObject.SetActive(true); 
                    // if (diceEyeNum == 0)
                    {
                        Debug.Log("目が決まりました");
                        // StartCoroutine(Count()); // カウントコルーチン実行
                        diceRoll.DiceReset();
                        state = Dice_State.DOUBTCHECK;
                    }
                    break;
                case Dice_State.DOUBTCHECK:
                    if (Input.GetKeyDown(KeyCode.Return)) state = Dice_State.CONFIRMATION;
                    else if (Input.GetKeyDown(KeyCode.Alpha1) && player.dice != 1) doubtNumber = 1;
                    else if (Input.GetKeyDown(KeyCode.Alpha2) && player.dice != 2) doubtNumber = 2;
                    else if (Input.GetKeyDown(KeyCode.Alpha3) && player.dice != 3) doubtNumber = 3;
                    else if (Input.GetKeyDown(KeyCode.Alpha4) && player.dice != 4) doubtNumber = 4;
                    else if (Input.GetKeyDown(KeyCode.Alpha5) && player.dice != 5) doubtNumber = 5;
                    else if (Input.GetKeyDown(KeyCode.Alpha6) && player.dice != 6) doubtNumber = 6;

                    if (doubtNumber == 0) GameStatus.lockMenber.DrawMessage($"嘘をつくなら出目以外の数値のキーを\nつかないならenterキーを押してください\n(現在の出目{player.dice})");
                    else state = Dice_State.CONFIRMATION;
                    break;
                case Dice_State.CONFIRMATION:
                    if (doubtNumber == 0) GameStatus.lockMenber.DrawMessage("嘘はつきません　よろしいですか?\nEnter キー　OK!\nbackSpace キー　考え直す");
                    else GameStatus.lockMenber.DrawMessage($"{doubtNumber}で嘘をつきます　よろしいですか?\nEnter キー　OK!\nbackSpace キー　考え直す");

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (doubtNumber != 0)
                        {
                            player.SetDoubt(true);
                            player.SetDoubtDice(doubtNumber);
                        }
                        view.RPC(nameof(PhaseCompleatesynchronize),RpcTarget.All,PhotonNetwork.LocalPlayer.ActorNumber - 1);
                    }
                    else if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        doubtNumber = 0;
                        state = Dice_State.DOUBTCHECK;
                    }
                    break;
            }
        }

        [PunRPC]
        public void Clear()
        {
            GameStatus.lockMenber.CompleateClear();
        }

        [PunRPC]
        public void PhaseCompleatesynchronize(int number)
        {
            GameStatus.lockMenber.CompleateScene(number);
        }
    }
}

