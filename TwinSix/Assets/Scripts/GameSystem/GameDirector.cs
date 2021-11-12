using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class GameDirector : MonoBehaviour
    {
        public int playerCount = 4; // �v���C���[�̐�
        [SerializeField] private GameObject instanceObject;
        [SerializeField] private MapInfoScriptableObject defaultMap;

        // �X�^�[�g���������������s�����߂�Awake���g�p
        void Awake()
        {
            PlayerStatus[] statuses = new PlayerStatus[playerCount]; // �v���C���[�X�e�[�^�X�z��쐬
            for (int i = 0; i < playerCount; i++)
            {
                GameObject obj = Instantiate(instanceObject); // �ݒ肳��Ă���I�u�W�F�N�g�쐬
                statuses[i] = obj.GetComponent<PlayerStatus>(); // ���������I�u�W�F�N�g����X�e�[�^�X�擾
                GameStatus.lockMenber.statusSeter = statuses[i]; // �X�e�[�^�X���Q�[���X�e�[�^�X�Ɋi�[
                PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // �o�^�����X�e�[�^�X���擾
                statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, "player" + i,defaultMap); // �X�e�[�^�X������������֐������s
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}