using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwinSix
{
    public class EndPhase : MonoBehaviour, IPhase
    {
        private const float NEXT_TURN_WAIT = 1f; // 次のターンへの移行時間
        //PlayerStatus playerStatus; // 現在のプレイヤーステータス
        public event Action NextPhase; // 次のフェーズへの移行関数

        public void PhaseCompleatesynchronize(int number)
        {
            throw new NotImplementedException();
        }

        [PunRPC]
        public void PhaseEnd()
        {
            Debug.Log("EndPhase_End");
            // 次のプレイヤーに移行 & 次のターンへ
            //GameStatus.lockMenber.PlayingNumberOrder();
            // NextPhase(); // 次のフェーズへ移行
        }

        [PunRPC]
        public void PhaseStart(PlayerStatus turnObject)
        {
            Debug.Log("EndPhase_Start");
            turnObject.SetDice(0);      // ダイスの目を初期化
            turnObject.SetDoubt(false); // ダウトフラグ初期化
            turnObject.SetDoubtDice(0); // 申告したダイスの目を初期化
            StartCoroutine(NextTurnCorutine()); // 暫くしてから次のターンへ
        }

        [PunRPC]
        public void PhaseUpdate()
        {

        }
        private IEnumerator NextTurnCorutine()
        {
            // 少し待機
            yield return new WaitForSeconds(NEXT_TURN_WAIT);
            if (PhotonNetwork.IsMasterClient) NextPhase();
        }
    }
}
