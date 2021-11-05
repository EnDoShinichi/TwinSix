using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using online;

namespace TwinSix
{
    public class TurnManager : MonoBehaviour
    {
        IPhase[] phases = new IPhase[5]; // フェーズの基本機能をまとめたインターフェイスの配列を作成
        PlayerStatus[] statuses = new PlayerStatus[4]; // プレイヤーステータスのリストを作成
        ThisPhase thisPhase = ThisPhase.DICE_PHASE; // 現在のフェーズを管理するenum

        [SerializeField] private Text[] texts = new Text[4]; // プロトタイプ用　プレイヤー情報をまとめたテキスト配列

        [SerializeField] private Text text; // プロトタイプ用　現在動かしているプレイヤーの移動量と虚偽の移動量をまとめたテキスト
        [SerializeField] private Text announcement; // プロトタイプ用　ゲームの進捗をまとめたテキスト

        [SerializeField] private GameObject[] phaseseObjects = new GameObject[(int)ThisPhase.count]; // フェーズの数だけ存在するフェーズのオブジェクト(これからフェーズをGetCommponentする)
        [SerializeField] private OnlineSynchronizeData synchonizeObject; // 同期用のスクリプト(シリアライズ化してあるためオブジェクトを入れるだけで可)
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < statuses.Length; i++)
            {
                statuses[i] = GameStatus.lockMenber.PlayerStatusGeter(i); // ゲームステータスからプレイヤーステータスを取得
            }

            // フェーズインターフェイスの配列に対して各フェーズを取得、格納
            phases[0] = phaseseObjects[0].GetComponent<DicePhase>();
            phases[1] = phaseseObjects[1].GetComponent<DoubtPhase>();
            phases[2] = phaseseObjects[2].GetComponent<MovePhase>();
            phases[3] = phaseseObjects[3].GetComponent<EventPhase>();
            phases[4] = phaseseObjects[4].GetComponent<EndPhase>();

            for (int i = 0; i < phases.Length; i++)
            {
                phases[i].NextPhase += NextPhase; // 各フェーズのイベントに次のフェーズへ移るための関数を代入
            }

            phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]); // 最初のキャラクターのスタート処理を実行
        }

        // Update is called once per frame
        void Update()
        {
            // テキスト表示用for文 実際の処理には影響なし おそらく不必要なためコメントアウト
            //for (int i = 0; i < status_s.Length; i++)
            //{
            //    texts[i].text = status_s[i].playerName + " : " + status_s[i].position.ToString() + "\n所持金：" + status_s[i].maney.ToString();
            //    if (status_s[i].doubt) texts[i].color = Color.red;
            //    else texts[i].color = Color.black;
            //    text.text = "出た目の数" + status_s[GameStatus.lockMenber.playingNumber].dice.ToString() + "  虚偽値(" + status_s[GameStatus.lockMenber.playingNumber].doubtDice + ")";
            //    announcement.text = "現在のフェーズ:" + thisPhase + "  行動順プレイヤー:" + status_s[GameStatus.lockMenber.playingNumber].playerName + "  現在ターン数:" + GameStatus.lockMenber.GameTurn.ToString();
            //}

            if (GameStatus.lockMenber.GameTurn < GameStatus.MAX_GAMETURN) phases[(int)thisPhase].PhaseUpdate(); // ゲームターンが終わるまで現在のフェーズのupdataを実行
            else Debug.Log("GameEnd");
        }

        // 次のフェーズへ移行する関数(イベント登録されるもの)
        public void NextPhase()
        {
            if (thisPhase == ThisPhase.END_PHASE) // もしエンドフェーズの処理が実行されていたなら
            {
                GameStatus.lockMenber.PlayingNumberOrder(); // ゲームステータスへ現在処理中のプレイヤーの変更を要請
                thisPhase = ThisPhase.DICE_PHASE; // フェーズをダイスフェーズ(最初のフェーズ)に変更
            }
            else thisPhase++; // エンドフェーズ以外ならフェーズ進行のみを行う

            Debug.Log("nextPhase = " + thisPhase);

            // ゲームが終了していないなら更新したフェーズのスタート処理を実行する
            if (GameStatus.lockMenber.GameTurn < GameStatus.MAX_GAMETURN) phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]);
        }
    }
}