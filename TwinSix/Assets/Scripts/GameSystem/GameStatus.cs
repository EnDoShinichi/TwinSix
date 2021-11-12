using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ThisPhase
{
    DICE_PHASE,
    DOUBT_PHASE,
    MOVE_PHASE,
    EVENT_PHASE,
    END_PHASE,
    count
}

public static class GameStatus // �Q�[���i�s�Ɋւ��ϐ��A�萔���܂Ƃ߂��ÓI�N���X�@�ϐ��̒l�̃Z�b�g�ɂ͍ő���̒��ӂ�
{
    public const int MAX_GAMETURN = 10; // �ő�Q�[���^�[��

    public const int THINKING_TIME = 3; // �v�l����

    public const int MAX_PLAYER_NUMBER = 4;

    public const int PLAYER_INITIAL_MANEY = 10000; // �����̏����l

    public const int PLAYER_INITIAL_DOUBTCOUNT = 3; // �R�����񐔂̏����l

    public class lockMenber // �ÓI�ϐ��͍ēx�N���X�ɂ܂Ƃ߂Ă���
    {
        public static int GameTurn { get; private set; } = 1; // ���݂̃Q�[���^�[��

        public static int myNumber { get; private set; } = 0; // ���g�̒[���̔ԍ�

        public static int playingNumber { get; private set; } = 0; // ���ݓ�������Ă���[���ԍ�

        private static List<PlayerStatus> targetPlayerNumber = new List<PlayerStatus>(); // �ΏۂɂƂ�ԍ�(�v���C���[�p)

        private static List<MapInfoScriptableObject> targetMapNumber = new List<MapInfoScriptableObject>(); // �ΏۂɂƂ�}�b�v����(�}�b�v�p)

        private static List<PlayerStatus> statuses = new List<PlayerStatus>(); // �v���C���[�X�e�[�^�X���i�[���郊�X�g

        private static List<MapInfoScriptableObject> maps = new List<MapInfoScriptableObject>();

        /// <summary>�I�I�A�N�Z�X���ӁI�I�@������id�����ɑΉ������X�e�[�^�X��Ԃ��܂�</summary>
        /// <param name="orderID">�󂯎�肽���L�����N�^�[id</param>
        /// <returns></returns>
        public static PlayerStatus PlayerStatusGeter(int orderID)
        {
            if (statuses.Count <= orderID)
            {
                Debug.LogError($"�\�����ʔԍ����m�F���܂����@�l�̍Ċm�F���s���Ă������� �Ή��ԍ�({orderID})");
                return null;
            }
            else
            {
                return statuses[orderID];
            }
        }

        /// <summary>�I�I�A�N�Z�X���ӁI�I�@�����̃L�����f�[�^�����ɑΉ������L�����f�[�^�ɍĊi�[���s���܂�</summary>
        /// <param name="orderStatus">�X�V�������v���C���[�f�[�^</param>
        /// <returns></returns>
        public static PlayerStatus PlayerStatusSeter(PlayerStatus orderStatus)
        {
            if (statuses.Count <= orderStatus.id)
            {
                Debug.LogError($"�\�����ʔԍ����m�F���܂����@�l�̍Ċm�F���s���Ă������� �Ή��ԍ�({orderStatus.id})");
                return null;
            }
            else
            {
                return statuses[orderStatus.id] = orderStatus;
            }
        }

        /// <summary>�I�I�A�N�Z�X���ӁI�I �Q�[���X�e�[�^�X���̃v���C���[�X�e�[�^�X���X�g�ɏ����i�[���܂�</summary>
        public static PlayerStatus statusSeter
        {
            set
            {
                if (statuses.Count <= MAX_PLAYER_NUMBER) statuses.Add(value);
                else Debug.LogError("����ȏ�̃X�e�[�^�X�ݒ�̉\��������܂��@����l���m�F���ĉ�����");
            }
        }


        public static void MapStatusSeter(List<MapInfoScriptableObject> newMapList)
        {
            maps = newMapList;
        }

        /// <summary>�I�I�A�N�Z�X���ӁI�I�@�Q�[���^�[���𑝂₵�܂�</summary>
        public static void AddGameTurn()
        {
            GameTurn++;
        }

        /// <summary>�I�I�A�N�Z�X���ӁI�I�@���g�̔ԍ����i�[���܂�</summary>
        public static void SetMyNumber(int newMyNumber)
        {
            myNumber = newMyNumber;
        }

        /// <summary>�I�I�A�N�Z�X���ӁI�I�@���ݓ�������Ă���ԍ����i�[���܂�</summary>
        public static void SetPlayingNumber(int thisTurmMenber)
        {
            playingNumber = thisTurmMenber;
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I�@�Q�[���^�[������ё���ԍ��̕ύX���s���܂�</summary>
        public static void PlayingNumberOrder()
        {
            if (playingNumber >= MAX_PLAYER_NUMBER - 1) GameTurn++;

            if (playingNumber < MAX_PLAYER_NUMBER - 1) playingNumber++;
            else playingNumber = 0;
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �o�^�ς݂̃}�b�v�󋵂�������Ԃ܂Ŗ߂��܂�</summary>
        public static void MapResetOrder()
        {
            for (int i = 0;i < maps.Count;i++)
            {
                maps[i].ResetData();
            }
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �Ώۃv���C���[��ǉ����܂�</summary>
        /// <param name="target"></param>
        public static void TargetListBind_Player(PlayerStatus target)
        {
            targetPlayerNumber.Add(target);
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �Ώۃ}�b�v��ǉ����܂�</summary>
        /// <param name="target"></param>
        public static void TargetListBind_Map(MapInfoScriptableObject target)
        {
            targetMapNumber.Add(target);
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �ΏۂɑS�Ẵv���C���[��ǉ����܂�</summary>
        public static void TargetAllBind_Player()
        {
            targetPlayerNumber.Clear();
            targetPlayerNumber = statuses;
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �Ώۂɂ��ׂẴ}�b�v��ǉ����܂�</summary>
        public static void TargetAllBind_Map()
        {
            targetMapNumber.Clear();
            targetMapNumber = maps;
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �Ώۃv���C���[�̃��X�g��Ԃ��܂�</summary>
        /// <returns></returns>
        public static List<PlayerStatus> GetTargetList_Player()
        {
            return targetPlayerNumber;
        }

        /// <summary> �I�I �A�N�Z�X���� �I�I �Ώۃ}�b�v�̃��X�g��Ԃ��܂�</summary>
        /// <returns></returns>
        public static List<MapInfoScriptableObject> GetTargetList_Map()
        {
            return targetMapNumber;
        }
    }
}
