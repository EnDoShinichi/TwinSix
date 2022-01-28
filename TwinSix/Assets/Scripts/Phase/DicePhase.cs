using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwinSix
{
    public class DicePhase : MonoBehaviour, IPhase
    {
        enum Dice_State
        {
            DICEROLL,
            DOUBTCHECK,
            CONFIRMATION,
            END,
            LENGTH
        }

        [SerializeField] DiceRoll dice;
        [SerializeField] PlayerStatus player;
        [SerializeField] private GameObject buttonObject; // �{�^���i�[�I�u�W�F�N�g
        [SerializeField] private GameObject numberButtonObject; // true,false��\�������̂��A�ǂ̔ԍ��̎��s���邩�̃{�^�����i�[���ꂽ�I�u�W�F�N�g

        PlayerStatus thisTurnstatus; // ���̃^�[���̃v���C���[�X�e�[�^�X
        DiceRoll diceRoll;

        int setDoubtNumber = 1; // �R�̒l��ۑ����Ă����ϐ�
        int diceEyeNum = 0;
        bool CountTime = true; // �J�E���g�R���[�`�������s����Ă��邩�ǂ���

        public event Action NextPhase; // ���̃t�F�[�Y�ֈڍs����֐�
        int money = 0;
        int ID = 0;
        int doubtCount = 0;
        string name = "";
        MapInfoScriptableObject defaultPosition;
        PlayerStatus status;

        Dice_State state = Dice_State.DICEROLL;
        int doubtNumber;
        PhotonView view;
        bool startFlg;

        private void Start()
        {
            view = GetComponent<PhotonView>();
            diceRoll = dice.GetComponent<DiceRoll>();
            startFlg = false;
        }

        [PunRPC]
        public void PhaseEnd()
        {
            Debug.Log("dicePhase_End");
            GameStatus.lockMenber.DrawMessage("");
            startFlg = false;
            //if (setDoubtNumber == -1) player.SetDoubt(false); // �����R�Ɛ錾���������̌�l�̍X�V������Ȃ������ꍇ�A�R�����ĂȂ��������Ƃɏ���������
            //if (player.doubt) player.SetDoubtDice(setDoubtNumber); // �����R�����Ă����Ȃ�A���U�̐U�����l���v���C���[�X�e�[�^�X�ɐݒ肷��

            //// �e�{�^���̔�\��
            //buttonObject.SetActive(false);
            //numberButtonObject.SetActive(false);
            if (PhotonNetwork.IsMasterClient) NextPhase();
        }

        [PunRPC]
        public void PhaseStart(PlayerStatus status)
        {
            state = Dice_State.DICEROLL;
            doubtNumber = 0;
            player = status;
            if (status.id == PhotonNetwork.LocalPlayer.ActorNumber - 1) diceRoll.DiceOn();
            GameStatus.lockMenber.DrawMessage($"{status.playerName}�̎�Ԃł�");
            Debug.Log("dicePhase" + GameStatus.lockMenber.playingNumber);
            setDoubtNumber = -1; // �l����͂���Ă��Ȃ���Ԃ�
            thisTurnstatus = status; // �v���C���[�X�e�[�^�X���X�V
            startFlg = true;
        }

        [PunRPC]
        public void PhaseUpdate()
        {
            if (!startFlg) return;
            if (GameStatus.lockMenber.CompleateCheck())
            {
                PhaseEnd();
                startFlg = false;
                return;
            }

            if (GameStatus.lockMenber.IsCompleate(PhotonNetwork.LocalPlayer.ActorNumber - 1))
            {
                GameStatus.lockMenber.DrawMessage("���v���C���[�̓��͂�҂��Ă��܂�...");
                return;
            }
            if (player.id != PhotonNetwork.LocalPlayer.ActorNumber - 1)
            {
                view.RPC(nameof(PhaseCompleatesynchronize), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                //GameStatus.lockMenber.CompleateScene(PhotonNetwork.LocalPlayer.ActorNumber - 1);
                return;
            }

            switch (state)
            {
                case Dice_State.DICEROLL:
                    if (diceRoll.GetDiceEyes() == 0) return;

                    Debug.Log("PhaseStart");
                    //int rand = UnityEngine.Random.Range(1, 7); // �����_���Ȓl�𐶐�
                    diceEyeNum = diceRoll.GetDiceEyes();
                    Debug.Log("aaaa" + diceRoll.GetDiceEyes());
                    player.SetDice(diceEyeNum); // ���̃^�[���̃_�C�X�̒l�ɐݒ�

                    //if (player.id == GameStatus.lockMenber.myNumber) buttonObject.SetActive(true); 
                    // if (diceEyeNum == 0)
                    {
                        Debug.Log("�ڂ����܂�܂���");
                        // StartCoroutine(Count()); // �J�E���g�R���[�`�����s
                        diceRoll.DiceReset();
                        state = Dice_State.DOUBTCHECK;
                    }
                    break;
                case Dice_State.DOUBTCHECK:
                    if (Input.GetKeyDown(KeyCode.Return)) state = Dice_State.CONFIRMATION;
                    else if (Input.GetKeyDown(KeyCode.Alpha1) && player.dice != 1) doubtNumber = 1;
                    else if (Input.GetKeyDown(KeyCode.Alpha2) && player.dice != 2) doubtNumber = 2;
                    else if (Input.GetKeyDown(KeyCode.Alpha3) && player.dice != 3) doubtNumber = 3;
                    else if (Input.GetKeyDown(KeyCode.Alpha4) && player.dice != 4) doubtNumber = 4;
                    else if (Input.GetKeyDown(KeyCode.Alpha5) && player.dice != 5) doubtNumber = 5;
                    else if (Input.GetKeyDown(KeyCode.Alpha6) && player.dice != 6) doubtNumber = 6;

                    if (doubtNumber == 0) GameStatus.lockMenber.DrawMessage($"�R�����Ȃ�o�ڈȊO�̐��l�̃L�[��\n���Ȃ��Ȃ�enter�L�[�������Ă�������\n(���݂̏o��{player.dice})");
                    else state = Dice_State.CONFIRMATION;
                    break;
                case Dice_State.CONFIRMATION:
                    if (doubtNumber == 0) GameStatus.lockMenber.DrawMessage("�R�͂��܂���@��낵���ł���?\nEnter �L�[�@OK!\nbackSpace �L�[�@�l������");
                    else GameStatus.lockMenber.DrawMessage($"{doubtNumber}�ŉR�����܂��@��낵���ł���?\nEnter �L�[�@OK!\nbackSpace �L�[�@�l������");

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (doubtNumber != 0)
                        {
                            player.SetDoubt(true);
                            player.SetDoubtDice(doubtNumber);
                        }
                        view.RPC(nameof(PhaseCompleatesynchronize),RpcTarget.All,PhotonNetwork.LocalPlayer.ActorNumber - 1);
                    }
                    else if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        doubtNumber = 0;
                        state = Dice_State.DOUBTCHECK;
                    }
                    break;
            }
        }

        [PunRPC]
        public void Clear()
        {
            GameStatus.lockMenber.CompleateClear();
        }

        [PunRPC]
        public void PhaseCompleatesynchronize(int number)
        {
            GameStatus.lockMenber.CompleateScene(number);
        }
    }
}

