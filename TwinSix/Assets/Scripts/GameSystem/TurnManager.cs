using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using online;

namespace TwinSix
{
    public class TurnManager : MonoBehaviour
    {
        IPhase[] phases = new IPhase[5]; // �t�F�[�Y�̊�{�@�\���܂Ƃ߂��C���^�[�t�F�C�X�̔z����쐬
        PlayerStatus[] statuses = new PlayerStatus[4]; // �v���C���[�X�e�[�^�X�̃��X�g���쐬
        ThisPhase thisPhase = ThisPhase.DICE_PHASE; // ���݂̃t�F�[�Y���Ǘ�����enum

        [SerializeField] private Text[] texts = new Text[4]; // �v���g�^�C�v�p�@�v���C���[�����܂Ƃ߂��e�L�X�g�z��

        [SerializeField] private Text text; // �v���g�^�C�v�p�@���ݓ������Ă���v���C���[�̈ړ��ʂƋ��U�̈ړ��ʂ��܂Ƃ߂��e�L�X�g
        [SerializeField] private Text announcement; // �v���g�^�C�v�p�@�Q�[���̐i�����܂Ƃ߂��e�L�X�g

        [SerializeField] private GameObject[] phaseseObjects = new GameObject[(int)ThisPhase.count]; // �t�F�[�Y�̐��������݂���t�F�[�Y�̃I�u�W�F�N�g(���ꂩ��t�F�[�Y��GetCommponent����)
        [SerializeField] private OnlineSynchronizeData synchonizeObject; // �����p�̃X�N���v�g(�V���A���C�Y�����Ă��邽�߃I�u�W�F�N�g�����邾���ŉ�)
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < statuses.Length; i++)
            {
                statuses[i] = GameStatus.lockMenber.PlayerStatusGeter(i); // �Q�[���X�e�[�^�X����v���C���[�X�e�[�^�X���擾
            }

            // �t�F�[�Y�C���^�[�t�F�C�X�̔z��ɑ΂��Ċe�t�F�[�Y���擾�A�i�[
            phases[0] = phaseseObjects[0].GetComponent<DicePhase>();
            phases[1] = phaseseObjects[1].GetComponent<DoubtPhase>();
            phases[2] = phaseseObjects[2].GetComponent<MovePhase>();
            phases[3] = phaseseObjects[3].GetComponent<EventPhase>();
            phases[4] = phaseseObjects[4].GetComponent<EndPhase>();

            for (int i = 0; i < phases.Length; i++)
            {
                phases[i].NextPhase += NextPhase; // �e�t�F�[�Y�̃C�x���g�Ɏ��̃t�F�[�Y�ֈڂ邽�߂̊֐�����
            }

            phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]); // �ŏ��̃L�����N�^�[�̃X�^�[�g���������s
        }

        // Update is called once per frame
        void Update()
        {
            // �e�L�X�g�\���pfor�� ���ۂ̏����ɂ͉e���Ȃ� �����炭�s�K�v�Ȃ��߃R�����g�A�E�g
            //for (int i = 0; i < status_s.Length; i++)
            //{
            //    texts[i].text = status_s[i].playerName + " : " + status_s[i].position.ToString() + "\n�������F" + status_s[i].maney.ToString();
            //    if (status_s[i].doubt) texts[i].color = Color.red;
            //    else texts[i].color = Color.black;
            //    text.text = "�o���ڂ̐�" + status_s[GameStatus.lockMenber.playingNumber].dice.ToString() + "  ���U�l(" + status_s[GameStatus.lockMenber.playingNumber].doubtDice + ")";
            //    announcement.text = "���݂̃t�F�[�Y:" + thisPhase + "  �s�����v���C���[:" + status_s[GameStatus.lockMenber.playingNumber].playerName + "  ���݃^�[����:" + GameStatus.lockMenber.GameTurn.ToString();
            //}

            if (GameStatus.lockMenber.GameTurn < GameStatus.MAX_GAMETURN) phases[(int)thisPhase].PhaseUpdate(); // �Q�[���^�[�����I���܂Ō��݂̃t�F�[�Y��updata�����s
            else Debug.Log("GameEnd");
        }

        // ���̃t�F�[�Y�ֈڍs����֐�(�C�x���g�o�^��������)
        public void NextPhase()
        {
            if (thisPhase == ThisPhase.END_PHASE) // �����G���h�t�F�[�Y�̏��������s����Ă����Ȃ�
            {
                GameStatus.lockMenber.PlayingNumberOrder(); // �Q�[���X�e�[�^�X�֌��ݏ������̃v���C���[�̕ύX��v��
                thisPhase = ThisPhase.DICE_PHASE; // �t�F�[�Y���_�C�X�t�F�[�Y(�ŏ��̃t�F�[�Y)�ɕύX
            }
            else thisPhase++; // �G���h�t�F�[�Y�ȊO�Ȃ�t�F�[�Y�i�s�݂̂��s��

            Debug.Log("nextPhase = " + thisPhase);

            // �Q�[�����I�����Ă��Ȃ��Ȃ�X�V�����t�F�[�Y�̃X�^�[�g���������s����
            if (GameStatus.lockMenber.GameTurn < GameStatus.MAX_GAMETURN) phases[(int)thisPhase].PhaseStart(statuses[GameStatus.lockMenber.playingNumber]);
        }
    }
}