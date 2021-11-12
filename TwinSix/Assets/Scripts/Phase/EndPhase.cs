using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class EndPhase : MonoBehaviour, IPhase
    {
        //PlayerStatus playerStatus; // 現在のプレイヤーステータス
        public event Action NextPhase; // 次のフェーズへの移行関数

        public void PhaseEnd()
        {
            Debug.Log("EndPhase_End");
            // 次のプレイヤーに移行 & 次のターンへ
            GameStatus.lockMenber.PlayingNumberOrder();
            NextPhase(); // 次のフェーズへ移行
        }

        public void PhaseStart(PlayerStatus turnObject)
        {
            Debug.Log("EndPhase_Start");
            turnObject.SetDice(0);
            turnObject.SetDoubt(false);
            turnObject.SetDoubtDice(0);
        }

        public void PhaseUpdate()
        {

        }
    }
}
