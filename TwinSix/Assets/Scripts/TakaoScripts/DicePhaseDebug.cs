using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class DicePhaseDebug : MonoBehaviour
    {
        PlayerStatus thisTurnstatus; // ���̃^�[���̃v���C���[�X�e�[�^�X
        DiceRoll diceRoll;

        int setDoubtNumber = 1; // �R�̒l��ۑ����Ă����ϐ�
        int diceEyeNum = 0;
        bool CountTime = true; // �J�E���g�R���[�`�������s����Ă��邩�ǂ���

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
            if (setDoubtNumber == -1) thisTurnstatus.SetDoubt(false); // �����R�Ɛ錾���������̌�l�̍X�V������Ȃ������ꍇ�A�R�����ĂȂ��������Ƃɏ���������
            if (thisTurnstatus.doubt) thisTurnstatus.SetDoubtDice(setDoubtNumber); // �����R�����Ă����Ȃ�A���U�̐U�����l���v���C���[�X�e�[�^�X�ɐݒ肷��
        }

        public void PhaseStart(PlayerStatus status)
        {
            Debug.Log("dicePhase" + GameStatus.lockMenber.playingNumber);
            CountTime = true; // �t���O��true�ɖ߂�
            setDoubtNumber = -1; // �l����͂���Ă��Ȃ���Ԃ�
            thisTurnstatus = status; // �v���C���[�X�e�[�^�X���X�V
        }

        public void PhaseUpdate()
        {
            if (CountTime) // �܂��J�E���g�R���[�`�������s����Ă��Ȃ��Ȃ�
            {
                //int rand = UnityEngine.Random.Range(1, 7); // �����_���Ȓl�𐶐�
                diceEyeNum = diceRoll.GetDiceEyes();
                Debug.Log("aaaa" + diceRoll.GetDiceEyes());
                thisTurnstatus.SetDice(diceEyeNum); // ���̃^�[���̃_�C�X�̒l�ɐݒ�

                if (diceEyeNum == 0)
                {
                    StartCoroutine(Count()); // �J�E���g�R���[�`�����s
                }
            }
        }

        private IEnumerator Count()
        {
            CountTime = false; // ���s�t���O��false�ɕύX
            Debug.Log("CountStart_byDice");
            yield return new WaitForSeconds(GameStatus.THINKING_TIME); // �v�l���ԕ�������~
            Debug.Log("endCount_byDice");
            PhaseEnd();
        }
    }

}
