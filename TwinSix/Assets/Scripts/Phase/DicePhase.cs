using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class DicePhase : MonoBehaviour, IPhase
    {
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

        private void Start()
        {
            diceRoll = dice.GetComponent<DiceRoll>();
            player.StatusActiveate(money, ID, doubtCount, name, defaultPosition);
            PhaseStart(status);
        }

        private void Update()
        {
            PhaseUpdate();
        }
        public void PhaseEnd()
        {
            Debug.Log("dicePhase_End");
            if (setDoubtNumber == -1) thisTurnstatus.SetDoubt(false); // もし嘘と宣言したがその後値の更新がされなかった場合、嘘をついてなかったことに書き換える
            if (thisTurnstatus.doubt) thisTurnstatus.SetDoubtDice(setDoubtNumber); // もし嘘をついていたなら、虚偽の振った値をプレイヤーステータスに設定する

            // 各ボタンの非表示
            buttonObject.SetActive(false);
            numberButtonObject.SetActive(false);

            NextPhase();
        }

        public void PhaseStart(PlayerStatus status)
        {
            Debug.Log("dicePhase" + GameStatus.lockMenber.playingNumber);
            setDoubtNumber = -1; // 値を入力されていない状態に
            thisTurnstatus = status; // プレイヤーステータスを更新
        }

        public void PhaseUpdate()
        {
            if (diceRoll.GetDiceEyes() == 0) return;

            Debug.Log("PhaseStart");
            //int rand = UnityEngine.Random.Range(1, 7); // ランダムな値を生成
            diceEyeNum = diceRoll.GetDiceEyes();
            Debug.Log("aaaa" + diceRoll.GetDiceEyes());
            player.SetDice(diceEyeNum); // このターンのダイスの値に設定

            //if (player.id == GameStatus.lockMenber.myNumber) buttonObject.SetActive(true); 
            if (diceEyeNum == 0)
            {
                Debug.Log("目が決まりました");
                StartCoroutine(Count()); // カウントコルーチン実行
            }
        }

        private IEnumerator Count()
        {
            CountTime = false; // 実行フラグをfalseに変更
            Debug.Log("CountStart_byDice");
            yield return new WaitForSeconds(GameStatus.THINKING_TIME); // 思考時間分処理停止
            Debug.Log("endCount_byDice");
            PhaseEnd();
        }

        #region ボタン用関数

        public void TrueButton()
        {
            thisTurnstatus.SetDoubt(false);
        }

        public void FalseButton()
        {
            thisTurnstatus.SetDoubt(true);
            numberButtonObject.SetActive(true);
        }

        public void Button_One()
        {
            setDoubtNumber = 1;
            Debug.Log(setDoubtNumber);
            numberButtonObject.SetActive(false);
        }

        public void Button_Two()
        {
            setDoubtNumber = 2;
            numberButtonObject.SetActive(false);
        }

        public void Button_Three()
        {
            setDoubtNumber = 3;
            numberButtonObject.SetActive(false);
        }

        public void Button_Four()
        {
            setDoubtNumber = 4;
            numberButtonObject.SetActive(false);
        }

        public void Button_Five()
        {
            setDoubtNumber = 5;
            numberButtonObject.SetActive(false);
        }

        public void Button_Six()
        {
            setDoubtNumber = 6;
            numberButtonObject.SetActive(false);
        }

        #endregion
    }
}

