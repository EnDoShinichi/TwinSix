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

        public static bool phaseFlg { get; private set; } = true; // フェーズの進行を管理するフラグ

        public static int myNumber { get; private set; } = 0; // 自身の端末の番号

        public static int playingNumber { get; private set; } = 0; // 現在動かされている端末番号

        private static List<PlayerStatus> status_s = new List<PlayerStatus>(); // プレイヤーステータスを格納するリスト

        /// <summary>！！アクセス注意！！　引数のidを元に対応したステータスを返します</summary>
        /// <param name="orderID">受け取りたいキャラクターid</param>
        /// <returns></returns>
        public static PlayerStatus PlayerStatusGeter(int orderID)
        {
            if (status_s.Count <= orderID)
            {
                Debug.LogError($"予期せぬ番号を確認しました　値の再確認を行ってください 対応番号({orderID})");
                return null;
            }
            else
            {
                return status_s[orderID];
            }
        }

        /// <summary>！！アクセス注意！！　引数のキャラデータを元に対応したキャラデータに再格納を行います</summary>
        /// <param name="orderStatus">更新したいプレイヤーデータ</param>
        /// <returns></returns>
        public static PlayerStatus PlayerStatusSeter(PlayerStatus orderStatus)
        {
            if (status_s.Count <= orderStatus.id)
            {
                Debug.LogError($"予期せぬ番号を確認しました　値の再確認を行ってください 対応番号({orderStatus.id})");
                return null;
            }
            else
            {
                return status_s[orderStatus.id] = orderStatus;
            }
        }

        /// <summary>！！アクセス注意！！ ゲームステータス内のプレイヤーステータスリストに情報を格納します</summary>
        public static PlayerStatus statusSeter
        {
            set
            {
                if (status_s.Count <= MAX_PLAYER_NUMBER) status_s.Add(value);
                else Debug.LogError("上限以上のステータス設定の可能性があります　上限値を確認して下さい");
            }
        }

        /// <summary>！！アクセス注意！！　ゲームターンを増やします</summary>
        public static void AddGameTurn()
        {
            GameTurn++;
        }

        /// <summary>！！アクセス注意！！　フェーズ移行のフラグを変更します</summary>
        public static void SetPhaseFlg(bool newPhaseFlg)
        {
            phaseFlg = newPhaseFlg;
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

    }

}
