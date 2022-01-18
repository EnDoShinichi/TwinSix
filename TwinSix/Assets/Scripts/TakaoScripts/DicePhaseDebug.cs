using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class DicePhaseDebug : MonoBehaviour
    {
        PlayerStatus thisTurnstatus; // このターンのプレイヤーステータス
        DiceRoll diceRoll;

        int setDoubtNumber = 1; // 嘘の値を保存しておく変数
        int diceEyeNum = 0;
        bool CountTime = true; // カウントコルーチンが実行されているかどうか

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            PhaseUpdate();
        }

        public void PhaseEnd()
        {
            Debug.Log("dicePhase_End");
            if (setDoubtNumber == -1) thisTurnstatus.SetDoubt(false); // もし嘘と宣言したがその後値の更新がされなかった場合、嘘をついてなかったことに書き換える
            if (thisTurnstatus.doubt) thisTurnstatus.SetDoubtDice(setDoubtNumber); // もし嘘をついていたなら、虚偽の振った値をプレイヤーステータスに設定する
        }

        public void PhaseStart(PlayerStatus status)
        {
            Debug.Log("dicePhase" + GameStatus.lockMenber.playingNumber);
            CountTime = true; // フラグをtrueに戻す
            setDoubtNumber = -1; // 値を入力されていない状態に
            thisTurnstatus = status; // プレイヤーステータスを更新
        }

        public void PhaseUpdate()
        {
            if (CountTime) // まだカウントコルーチンが実行されていないなら
            {
                //int rand = UnityEngine.Random.Range(1, 7); // ランダムな値を生成
                diceEyeNum = diceRoll.GetDiceEyes();
                Debug.Log("aaaa" + diceRoll.GetDiceEyes());
                thisTurnstatus.SetDice(diceEyeNum); // このターンのダイスの値に設定

                if (diceEyeNum == 0)
                {
                    StartCoroutine(Count()); // カウントコルーチン実行
                }
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
    }

}
