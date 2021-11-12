using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class DicePhase : MonoBehaviour, IPhase
    {
        [SerializeField] private GameObject buttonObject; // ボタン格納オブジェクト
        [SerializeField] private GameObject numberButtonObject; // true,falseを表示したのち、どの番号の実行するかのボタンが格納されたオブジェクト

        PlayerStatus thisTurnstatus; // このターンのプレイヤーステータス

        int setDoubtNumber = 1; // 嘘の値を保存しておく変数
        bool CountTime = true; // カウントコルーチンが実行されているかどうか

        public event Action NextPhase; // 次のフェーズへ移行する関数

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
            CountTime = true; // フラグをtrueに戻す
            setDoubtNumber = -1; // 値を入力されていない状態に
            thisTurnstatus = status; // プレイヤーステータスを更新
        }

        public void PhaseUpdate()
        {
            if (CountTime) // まだカウントコルーチンが実行されていないなら
            {
                int rand = UnityEngine.Random.Range(1, 7); // ランダムな値を生成
                thisTurnstatus.SetDice(rand); // このターンのダイスの値に設定

                if (thisTurnstatus.id == GameStatus.lockMenber.myNumber) buttonObject.SetActive(true); // 今回のプレイヤーが自身のステータスと合致しているならボタンオブジェクトを表示
                else // そうでない場合(他キャラ)、嘘をつくかの判定を行ったのち、その状況に応じた値を設定
                {
                    int doubtRand = UnityEngine.Random.Range(0, 2);
                    if (doubtRand == 0)
                    {
                        int rand_doubt = UnityEngine.Random.Range(1, 7);
                        setDoubtNumber = rand_doubt;
                        thisTurnstatus.SetDoubt(true);
                    }
                }
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

