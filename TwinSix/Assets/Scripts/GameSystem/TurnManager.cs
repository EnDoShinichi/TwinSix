using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
// using online;

namespace TwinSix
{
    public class TurnManager : MonoBehaviour
    {
        IPhase[] phases = new IPhase[5]; // フェーズの基本機能をまとめたインターフェイスの配列を作成
        PlayerStatus[] statuses = new PlayerStatus[4]; // プレイヤーステータスのリストを作成
        ThisPhase thisPhase = ThisPhase.DICE_PHASE; // 現在のフェーズを管理するenum
        ResultManager resultData;
        List<PlayerStatus> state = new List<PlayerStatus>();

        [SerializeField] private GameObject[] phaseseObjects = new GameObject[(int)ThisPhase.count]; // フェーズの数だけ存在するフェーズのオブジェクト(これからフェーズをGetCommponentする)
        [SerializeField] private GameObject resultObject;
        [SerializeField] private OnlineSynchronizeData synchonizeObject; // 同期用のスクリプト(シリアライズ化してあるためオブジェクトを入れるだけで可)

        [SerializeField] Text phaseText;
        [SerializeField] Text turntext;
        [SerializeField] Text[] playerTexts;
        PhotonView view;
        bool statusSetFlg = false;
        // Start is called before the first frame update
        void Start()
        {
            statusSetFlg = false;
            //for (int i = 0; i < statuses.Length; i++)
            //{
            //    statuses[i] = GameStatus.lockMenber.PlayerStatusGeter(i); // ゲームステータスからプレイヤーステータスを取得
            //}
            //state = statuses.ToList();
            //thisPhase = ThisPhase.DICE_PHASE;
            //view = GetComponent<PhotonView>();
            //GameStatus.lockMenber.CompleateClear();

            //// フェーズインターフェイスの配列に対して各フェーズを取得、格納
            //phases[0] = phaseseObjects[0].GetComponent<DicePhase>();
            //phases[1] = phaseseObjects[1].GetComponent<DoubtPhase>();
            //phases[2] = phaseseObjects[2].GetComponent<MovePhase>();
            //phases[3] = phaseseObjects[3].GetComponent<EventPhase>();
            //phases[4] = phaseseObjects[4].GetComponent<EndPhase>();

            //resultData = resultObject.GetComponent<ResultManager>();

            //for (int i = 0; i < phases.Length; i++)
            //{
            //    phases[i].NextPhase += NextPhase; // 各フェーズのイベントに次のフェーズへ移るための関数を代入
            //}

            //phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]); // 最初のキャラクターのスタート処理を実行
        }

        // Update is called once per frame
        void Update()
        {
            if (!statusSetFlg)
            {
                if (GameStatus.lockMenber.PlayerStatusGeter(GameStatus.MAX_PLAYER_NUMBER - 1) == null)
                {
                    return;
                }
                else
                {
                    statusSetFlg = true;
                    for (int i = 0; i < statuses.Length; i++)
                    {
                        statuses[i] = GameStatus.lockMenber.PlayerStatusGeter(i); // ゲームステータスからプレイヤーステータスを取得
                    }
                    state = statuses.ToList();
                    thisPhase = ThisPhase.DICE_PHASE;
                    view = GetComponent<PhotonView>();
                    GameStatus.lockMenber.CompleateClear();

                    // フェーズインターフェイスの配列に対して各フェーズを取得、格納
                    phases[0] = phaseseObjects[0].GetComponent<DicePhase>();
                    phases[1] = phaseseObjects[1].GetComponent<DoubtPhase>();
                    phases[2] = phaseseObjects[2].GetComponent<MovePhase>();
                    phases[3] = phaseseObjects[3].GetComponent<EventPhase>();
                    phases[4] = phaseseObjects[4].GetComponent<EndPhase>();

                    resultData = resultObject.GetComponent<ResultManager>();

                    for (int i = 0; i < phases.Length; i++)
                    {
                        phases[i].NextPhase += NextPhase; // 各フェーズのイベントに次のフェーズへ移るための関数を代入
                    }

                    phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]); // 最初のキャラクターのスタート処理を実行
                }
            }

            // view.RPC(nameof(PhaseUpdateExe),RpcTarget.All);
            PhaseUpdateExe();
        }

        [PunRPC]
        void PhaseUpdateExe()
        {
            // phaseText.text = thisPhase.ToString();
            turntext.text = $"{GameStatus.lockMenber.GameTurn}";

            for (int i = 0;i < playerTexts.Length;i++)
            {
                playerTexts[i].text = $"{statuses[i].playerName}\n所持金：{statuses[i].money}";
            }

            if (GameStatus.lockMenber.GameTurn < GameStatus.MAX_GAMETURN) phases[(int)thisPhase].PhaseUpdate(); // ゲームターンが終わるまで現在のフェーズのupdataを実行
            else SceneManager.LoadScene("ResultScene");
        }

        [PunRPC]
        // 次のフェーズへ移行する関数(イベント登録されるもの)
        public void NextPhase()
        {
            if (PhotonNetwork.IsMasterClient) view.RPC(nameof(Clear), RpcTarget.All);
        }

        [PunRPC]
        public void Clear()
        {
            GameStatus.lockMenber.CompleateClear();

            if (thisPhase == ThisPhase.END_PHASE) // もしエンドフェーズの処理が実行されていたなら
            {
                GameStatus.lockMenber.PlayingNumberOrder(); // ゲームステータスへ現在処理中のプレイヤーの変更を要請
                thisPhase = ThisPhase.DICE_PHASE; // フェーズをダイスフェーズ(最初のフェーズ)に変更

                var rankList = state.OrderByDescending(state => state.money);
                resultData.setRank = rankList.ToList();
            }
            else thisPhase++; // エンドフェーズ以外ならフェーズ進行のみを行う

            Debug.Log("nextPhase = " + thisPhase);
            // ゲームが終了していないなら更新したフェーズのスタート処理を実行する
            if (GameStatus.lockMenber.GameTurn < GameStatus.MAX_GAMETURN) phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]);
        }
    }
}