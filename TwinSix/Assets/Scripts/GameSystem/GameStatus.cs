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

public static class GameStatus // ゲーム進行に関わる変数、定数をまとめた静的クラス　変数の値のセットには最大限の注意を
{
    public const int MAX_GAMETURN = 10; // 最大ゲームターン

    public const int THINKING_TIME = 3; // 思考時間

    public const int MAX_PLAYER_NUMBER = 4;

    public const int PLAYER_INITIAL_MANEY = 10000; // お金の初期値

    public const int PLAYER_INITIAL_DOUBTCOUNT = 3; // 嘘制限回数の初期値

    public class lockMenber // 静的変数は再度クラスにまとめてある
    {
        public static int GameTurn { get; private set; } = 1; // 現在のゲームターン

        public static int myNumber { get; private set; } = 0; // 自身の端末の番号

        public static int playingNumber { get; private set; } = 0; // 現在動かされている端末番号

        private static List<PlayerStatus> targetPlayerNumber = new List<PlayerStatus>(); // 対象にとる番号(プレイヤー用)

        private static List<MapInfoScriptableObject> targetMapNumber = new List<MapInfoScriptableObject>(); // 対象にとるマップ名称(マップ用)

        private static List<PlayerStatus> statuses = new List<PlayerStatus>(); // プレイヤーステータスを格納するリスト

        private static List<MapInfoScriptableObject> maps = new List<MapInfoScriptableObject>();

        /// <summary>！！アクセス注意！！　引数のidを元に対応したステータスを返します</summary>
        /// <param name="orderID">受け取りたいキャラクターid</param>
        /// <returns></returns>
        public static PlayerStatus PlayerStatusGeter(int orderID)
        {
            if (statuses.Count <= orderID)
            {
                Debug.LogError($"予期せぬ番号を確認しました　値の再確認を行ってください 対応番号({orderID})");
                return null;
            }
            else
            {
                return statuses[orderID];
            }
        }

        /// <summary>！！アクセス注意！！　引数のキャラデータを元に対応したキャラデータに再格納を行います</summary>
        /// <param name="orderStatus">更新したいプレイヤーデータ</param>
        /// <returns></returns>
        public static PlayerStatus PlayerStatusSeter(PlayerStatus orderStatus)
        {
            if (statuses.Count <= orderStatus.id)
            {
                Debug.LogError($"予期せぬ番号を確認しました　値の再確認を行ってください 対応番号({orderStatus.id})");
                return null;
            }
            else
            {
                return statuses[orderStatus.id] = orderStatus;
            }
        }

        /// <summary>！！アクセス注意！！ ゲームステータス内のプレイヤーステータスリストに情報を格納します</summary>
        public static PlayerStatus statusSeter
        {
            set
            {
                if (statuses.Count <= MAX_PLAYER_NUMBER) statuses.Add(value);
                else Debug.LogError("上限以上のステータス設定の可能性があります　上限値を確認して下さい");
            }
        }


        public static void MapStatusSeter(List<MapInfoScriptableObject> newMapList)
        {
            maps = newMapList;
        }

        /// <summary>！！アクセス注意！！　ゲームターンを増やします</summary>
        public static void AddGameTurn()
        {
            GameTurn++;
        }

        /// <summary>！！アクセス注意！！　自身の番号を格納します</summary>
        public static void SetMyNumber(int newMyNumber)
        {
            myNumber = newMyNumber;
        }

        /// <summary>！！アクセス注意！！　現在動かされている番号を格納します</summary>
        public static void SetPlayingNumber(int thisTurmMenber)
        {
            playingNumber = thisTurmMenber;
        }

        /// <summary> ！！ アクセス注意 ！！　ゲームターンおよび操作番号の変更を行います</summary>
        public static void PlayingNumberOrder()
        {
            if (playingNumber >= MAX_PLAYER_NUMBER - 1) GameTurn++;

            if (playingNumber < MAX_PLAYER_NUMBER - 1) playingNumber++;
            else playingNumber = 0;
        }

        /// <summary> ！！ アクセス注意 ！！ 登録済みのマップ状況を初期状態まで戻します</summary>
        public static void MapResetOrder()
        {
            for (int i = 0;i < maps.Count;i++)
            {
                maps[i].ResetData();
            }
        }

        /// <summary> ！！ アクセス注意 ！！ 対象プレイヤーを追加します</summary>
        /// <param name="target"></param>
        public static void TargetListBind_Player(PlayerStatus target)
        {
            targetPlayerNumber.Add(target);
        }

        /// <summary> ！！ アクセス注意 ！！ 対象マップを追加します</summary>
        /// <param name="target"></param>
        public static void TargetListBind_Map(MapInfoScriptableObject target)
        {
            targetMapNumber.Add(target);
        }

        /// <summary> ！！ アクセス注意 ！！ 対象に全てのプレイヤーを追加します</summary>
        public static void TargetAllBind_Player()
        {
            targetPlayerNumber.Clear();
            targetPlayerNumber = statuses;
        }

        /// <summary> ！！ アクセス注意 ！！ 対象にすべてのマップを追加します</summary>
        public static void TargetAllBind_Map()
        {
            targetMapNumber.Clear();
            targetMapNumber = maps;
        }

        /// <summary> ！！ アクセス注意 ！！ 対象プレイヤーのリストを返します</summary>
        /// <returns></returns>
        public static List<PlayerStatus> GetTargetList_Player()
        {
            return targetPlayerNumber;
        }

        /// <summary> ！！ アクセス注意 ！！ 対象マップのリストを返します</summary>
        /// <returns></returns>
        public static List<MapInfoScriptableObject> GetTargetList_Map()
        {
            return targetMapNumber;
        }
    }
}
