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
        private const float NEXT_TURN_WAIT = 1f; // ���̃^�[���ւ̈ڍs����
        //PlayerStatus playerStatus; // ���݂̃v���C���[�X�e�[�^�X
        public event Action NextPhase; // ���̃t�F�[�Y�ւ̈ڍs�֐�

        public void PhaseCompleatesynchronize(int number)
        {
            throw new NotImplementedException();
        }

        [PunRPC]
        public void PhaseEnd()
        {
            Debug.Log("EndPhase_End");
            // ���̃v���C���[�Ɉڍs & ���̃^�[����
            //GameStatus.lockMenber.PlayingNumberOrder();
            // NextPhase(); // ���̃t�F�[�Y�ֈڍs
        }

        [PunRPC]
        public void PhaseStart(PlayerStatus turnObject)
        {
            Debug.Log("EndPhase_Start");
            turnObject.SetDice(0);      // �_�C�X�̖ڂ�������
            turnObject.SetDoubt(false); // �_�E�g�t���O������
            turnObject.SetDoubtDice(0); // �\�������_�C�X�̖ڂ�������
            StartCoroutine(NextTurnCorutine()); // �b�����Ă��玟�̃^�[����
        }

        [PunRPC]
        public void PhaseUpdate()
        {

        }
        private IEnumerator NextTurnCorutine()
        {
            // �����ҋ@
            yield return new WaitForSeconds(NEXT_TURN_WAIT);
            if (PhotonNetwork.IsMasterClient) NextPhase();
        }
    }
}
