using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwinSix
{
    public class GameDirector : MonoBehaviour
    {
        public int playerCount = 4; // プレイヤーの数
        [SerializeField] private GameObject instanceObject;
        [SerializeField] private MapInfoScriptableObject defaultMap;
        [SerializeField] private Material[] mts;

        [SerializeField] private bool testMode = true;
        PlayerStatus[] statuses = new PlayerStatus[GameStatus.MAX_PLAYER_NUMBER]; // プレイヤーステータス配列作成
        PhotonView view;
        // スタートよりも早く処理を行うためにAwakeを使用
        void Awake()
        {
            if (testMode)
            {
                Debug.Log("director_on");
                PlayerStatus[] statuses = new PlayerStatus[playerCount]; // プレイヤーステータス配列作成
                for (int i = 0; i < playerCount; i++)
                {
                    GameObject obj = Instantiate(instanceObject); // 設定されているオブジェクト作成
                                                                  //Renderer r = obj.GetComponent<Renderer>();
                                                                  //if (mts[i] != null) /*m.SetColor("",new Color(i * 10,i * 10, i * 10));*/ r.material = mts[i];
                    statuses[i] = obj.GetComponent<PlayerStatus>(); // 生成したオブジェクトからステータス取得
                    GameStatus.lockMenber.statusSeter = statuses[i]; // ステータスをゲームステータスに格納
                    PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // 登録したステータスを取得
                    statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, "player" + i, defaultMap); // ステータスを初期化する関数を実行
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
                        GameObject obj = PhotonNetwork.Instantiate("Avatar" + i.ToString(), Vector3.zero, Quaternion.identity); // 設定されているオブジェクト作成
                        statuses[i] = obj.GetComponent<PlayerStatus>(); // 生成したオブジェクトからステータス取得
                        GameStatus.lockMenber.statusSeter = statuses[i]; // ステータスをゲームステータスに格納
                        PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // 登録したステータスを取得
                        statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, PhotonNetwork.PlayerList[i].NickName, defaultMap); // ステータスを初期化する関数を実行
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
                GameObject obj = GameObject.Find("Avatar" + i.ToString() + "(Clone)"); // 設定されているオブジェクト作成
                statuses[i] = obj.GetComponent<PlayerStatus>(); // 生成したオブジェクトからステータス取得
                GameStatus.lockMenber.statusSeter = statuses[i]; // ステータスをゲームステータスに格納
                PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // 登録したステータスを取得
                statuses[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, PhotonNetwork.PlayerList[i].NickName, defaultMap); // ステータスを初期化する関数を実行
                statuses[i].SetGameObject(obj);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}