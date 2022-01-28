using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class TitleDirector : MonoBehaviourPunCallbacks
{
    bool inputFlg = false; // 入力フラグ
    // プレイヤー情報
    //PlayerStatus playerStatus;
    // 名前入力欄
    [SerializeField] InputField nameField;
    // 移行先
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
        //// 名前情報を設定
        //playerStatus.SetPlayerName(nameField.text);
        //GameStatus.lockMenber.statusSeter = playerStatus;

        if (PhotonNetwork.PlayerList.Length > 3 && PhotonNetwork.IsMasterClient) view.RPC(nameof(NextScene), RpcTarget.All);
    }
    /// <summary> 名前入力 </summary>
    public void Matching()
    {
        if (inputFlg || nameField.text == null) return; // 1回だけ処理
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nameField.text;
        inputFlg = true;
    }
    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("match");
        // ランダムなルームに参加する
        PhotonNetwork.JoinRandomRoom();
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("create");
        // ルームの参加人数を4人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        int playerNum;
        // プレイヤーの番号を付ける
        playerNum = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    [PunRPC]
    public void NextScene()
    {
        // シーン変移
        SceneManager.LoadScene(nextScene);
    }
}
