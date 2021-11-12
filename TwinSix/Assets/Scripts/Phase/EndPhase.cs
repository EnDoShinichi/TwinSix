using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class EndPhase : MonoBehaviour, IPhase
    {
        //PlayerStatus playerStatus; // ���݂̃v���C���[�X�e�[�^�X
        public event Action NextPhase; // ���̃t�F�[�Y�ւ̈ڍs�֐�

        public void PhaseEnd()
        {
            Debug.Log("EndPhase_End");
            // ���̃v���C���[�Ɉڍs & ���̃^�[����
            GameStatus.lockMenber.PlayingNumberOrder();
            NextPhase(); // ���̃t�F�[�Y�ֈڍs
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
