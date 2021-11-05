using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinSix
{
    public class GameDirector : MonoBehaviour
    {
        public int playerCount = 4; // プレイヤーの数
        [SerializeField] private GameObject instanceObject;

        // スタートよりも早く処理を行うためにAwakeを使用
        void Awake()
        {
            PlayerStatus[] status_s = new PlayerStatus[playerCount]; // プレイヤーステータス配列作成
            for (int i = 0; i < playerCount; i++)
            {
                GameObject obj = Instantiate(instanceObject); // 設定されているオブジェクト作成
                status_s[i] = obj.GetComponent<PlayerStatus>(); // 生成したオブジェクトからステータス取得
                GameStatus.lockMenber.statusSeter = status_s[i]; // ステータスをゲームステータスに格納
                PlayerStatus status_ = GameStatus.lockMenber.PlayerStatusGeter(i); // 登録したステータスを取得
                status_s[i].StatusActiveate(GameStatus.PLAYER_INITIAL_MANEY, i, GameStatus.PLAYER_INITIAL_DOUBTCOUNT, "player" + i); // ステータスを初期化する関数を実行
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}