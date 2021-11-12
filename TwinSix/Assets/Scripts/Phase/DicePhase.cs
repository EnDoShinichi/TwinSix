using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class DicePhase : MonoBehaviour, IPhase
    {
        [SerializeField] private GameObject buttonObject; // �{�^���i�[�I�u�W�F�N�g
        [SerializeField] private GameObject numberButtonObject; // true,false��\�������̂��A�ǂ̔ԍ��̎��s���邩�̃{�^�����i�[���ꂽ�I�u�W�F�N�g

        PlayerStatus thisTurnstatus; // ���̃^�[���̃v���C���[�X�e�[�^�X

        int setDoubtNumber = 1; // �R�̒l��ۑ����Ă����ϐ�
        bool CountTime = true; // �J�E���g�R���[�`�������s����Ă��邩�ǂ���

        public event Action NextPhase; // ���̃t�F�[�Y�ֈڍs����֐�

        public void PhaseEnd()
        {
            Debug.Log("dicePhase_End");
            if (setDoubtNumber == -1) thisTurnstatus.SetDoubt(false); // �����R�Ɛ錾���������̌�l�̍X�V������Ȃ������ꍇ�A�R�����ĂȂ��������Ƃɏ���������
            if (thisTurnstatus.doubt) thisTurnstatus.SetDoubtDice(setDoubtNumber); // �����R�����Ă����Ȃ�A���U�̐U�����l���v���C���[�X�e�[�^�X�ɐݒ肷��

            // �e�{�^���̔�\��
            buttonObject.SetActive(false);
            numberButtonObject.SetActive(false);

            NextPhase();
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
                int rand = UnityEngine.Random.Range(1, 7); // �����_���Ȓl�𐶐�
                thisTurnstatus.SetDice(rand); // ���̃^�[���̃_�C�X�̒l�ɐݒ�

                if (thisTurnstatus.id == GameStatus.lockMenber.myNumber) buttonObject.SetActive(true); // ����̃v���C���[�����g�̃X�e�[�^�X�ƍ��v���Ă���Ȃ�{�^���I�u�W�F�N�g��\��
                else // �����łȂ��ꍇ(���L����)�A�R�������̔�����s�����̂��A���̏󋵂ɉ������l��ݒ�
                {
                    int doubtRand = UnityEngine.Random.Range(0, 2);
                    if (doubtRand == 0)
                    {
                        int rand_doubt = UnityEngine.Random.Range(1, 7);
                        setDoubtNumber = rand_doubt;
                        thisTurnstatus.SetDoubt(true);
                    }
                }
                StartCoroutine(Count()); // �J�E���g�R���[�`�����s
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

        #region �{�^���p�֐�

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

