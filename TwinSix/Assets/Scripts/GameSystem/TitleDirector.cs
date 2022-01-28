using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class TitleDirector : MonoBehaviourPunCallbacks
{
    bool inputFlg = false; // ���̓t���O
    // �v���C���[���
    //PlayerStatus playerStatus;
    // ���O���͗�
    [SerializeField] InputField nameField;
    // �ڍs��
    [SerializeField] SceneObject nextScene;

    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.InRoom) return;
        //PlayerStatus playerStatus = new PlayerStatus();
        //// ���O����ݒ�
        //playerStatus.SetPlayerName(nameField.text);
        //GameStatus.lockMenber.statusSeter = playerStatus;

        if (PhotonNetwork.PlayerList.Length > 3 && PhotonNetwork.IsMasterClient) view.RPC(nameof(NextScene), RpcTarget.All);
    }
    /// <summary> ���O���� </summary>
    public void Matching()
    {
        if (inputFlg || nameField.text == null) return; // 1�񂾂�����
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nameField.text;
        inputFlg = true;
    }
    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        Debug.Log("match");
        // �����_���ȃ��[���ɎQ������
        PhotonNetwork.JoinRandomRoom();
    }

    // �����_���ŎQ���ł��郋�[�������݂��Ȃ��Ȃ�A�V�K�Ń��[�����쐬����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("create");
        // ���[���̎Q���l����4�l�ɐݒ肷��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        int playerNum;
        // �v���C���[�̔ԍ���t����
        playerNum = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    [PunRPC]
    public void NextScene()
    {
        // �V�[���ψ�
        SceneManager.LoadScene(nextScene);
    }
}
