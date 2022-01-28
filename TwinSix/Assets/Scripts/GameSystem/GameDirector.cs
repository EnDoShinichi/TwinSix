using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwinSix
{
    public class GameDirector : MonoBehaviour
    {
        public int playerCount = 4; // �v���C���[�̐�
        [SerializeField] private GameObject instanceObject;
        [SerializeField] private MapInfoScriptableObject defaultMap;
        [SerializeField] private Material[] mts;

        [SerializeField] private bool testMode = true;
        PlayerStatus[] statuses = new PlayerStatus[GameStatus.MAX_PLAYER_NUMBER]; // �v���C���[�X�e�[�^�X�z��쐬
        PhotonView view;
        // �X�^�[�g���������������s�����߂�Awake���g�p
        void Awake()
        {
            if (testMode)
            {
                Debug.Log("director_on");
                PlayerStatus[] statuses = new PlayerStatus[playerCount]; // �v���C���[�X�e�[�^�X�z��쐬
                for (int i = 0; i < playerCount; i++)
                {
                    GameObject obj = Instantiate(instanceObject); // �ݒ肳��Ă���I�u�W�F�N�g�쐬
                                                                  //Renderer r = obj.GetComponent<Renderer>();
                                                                  //if (mts[i] != null) /*m.SetColor("",new Color(i * 10,i * 10, i * 10));*/ r.material = mts[i];
                    statuses[i] = obj.GetComponent<PlayerStatus>(); // ���������I�u�W�F�N�g����X�e�[�^�X�擾
                    GameStatus.lockMenber.statusSeter = statuses[i]; // �X�e�[�^�X���Q�[���X�e�[�^�X�Ɋi�[
                    PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // �o�^�����X�e�[�^�X���擾
                    statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, "player" + i, defaultMap); // �X�e�[�^�X������������֐������s
                    statuses[i].SetGameObject(obj);
                }
            }
            else
            {
                view = GetComponent<PhotonView>();
                if (PhotonNetwork.IsMasterClient)
                {
                    for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                    {
                        Debug.Log("roop" + i);
                        GameObject obj = PhotonNetwork.Instantiate("Avatar" + i.ToString(), Vector3.zero, Quaternion.identity); // �ݒ肳��Ă���I�u�W�F�N�g�쐬
                        statuses[i] = obj.GetComponent<PlayerStatus>(); // ���������I�u�W�F�N�g����X�e�[�^�X�擾
                        GameStatus.lockMenber.statusSeter = statuses[i]; // �X�e�[�^�X���Q�[���X�e�[�^�X�Ɋi�[
                        PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // �o�^�����X�e�[�^�X���擾
                        statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, PhotonNetwork.PlayerList[i].NickName, defaultMap); // �X�e�[�^�X������������֐������s
                        statuses[i].SetGameObject(obj);
                    }

                    view.RPC(nameof(StatusCreate), RpcTarget.All);
                }
            }
        }

        [PunRPC]
        public void StatusCreate()
        {
            if (PhotonNetwork.IsMasterClient) return;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                Debug.Log("roop" + i);
                GameObject obj = GameObject.Find("Avatar" + i.ToString() + "(Clone)"); // �ݒ肳��Ă���I�u�W�F�N�g�쐬
                statuses[i] = obj.GetComponent<PlayerStatus>(); // ���������I�u�W�F�N�g����X�e�[�^�X�擾
                GameStatus.lockMenber.statusSeter = statuses[i]; // �X�e�[�^�X���Q�[���X�e�[�^�X�Ɋi�[
                PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // �o�^�����X�e�[�^�X���擾
                statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, PhotonNetwork.PlayerList[i].NickName, defaultMap); // �X�e�[�^�X������������֐������s
                statuses[i].SetGameObject(obj);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}