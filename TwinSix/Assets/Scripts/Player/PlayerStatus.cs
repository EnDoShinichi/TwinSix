using System;
using UnityEngine;

[Serializable]
public class PlayerStatus : MonoBehaviour
{
    // 前回のPlayerStatusと異なり実体を持つPlayerStatus シリアライズ化されている

    [SerializeField, NonSerialized] private int moneyData = 10000;

    [SerializeField, NonSerialized] private int ID = -1;

    [SerializeField, NonSerialized] private int diceData = 0;

    [SerializeField, NonSerialized] private int doubtDiceData = 0;

    [SerializeField, NonSerialized] private int doubtCountData = 0;

    [SerializeField, NonSerialized] private string nameData = "nonPlayer";

    [SerializeField, NonSerialized] private bool doubtData = false;

    [SerializeField, NonSerialized] private bool actionData = true;

    [SerializeField, NonSerialized] private MapInfoScriptableObject myMapPositionData;

    private bool activeateFlg = false; // 初期化フラグ


    // 初期化を行うクラス(一回のみ実行可能)
    public void StatusActiveate(int money, int ID, int doubtCount, string name, MapInfoScriptableObject defaultPosition)
    {
        if (activeateFlg)
        {
            Debug.Log("このステータスは既に初期化されています");
            return;
        }
        moneyData = money;
        this.ID = ID;
        doubtCountData = doubtCount;
        nameData = name;
        actionData = true;
        activeateFlg = true;
        myMapPosition = defaultPosition;
    }

    #region getプロパティ

    // getプロパティはこの関数を呼び出すことで対応した変数を取得することができます
    // ただし、getのみの記述となるので変数の書き換えは下記セッターを使用してください

    public int money
    {
        get => moneyData;
        private set => moneyData = value;
    }
    public int id
    {
        get => ID;
        private set => ID = value;
    }
    public int dice
    {
        get => diceData;
        private set => diceData = value;
    }
    public int doubtDice
    {
        get => doubtDiceData;
        private set => doubtDiceData = value;
    }
    public int doubtCount
    {
        get => doubtCountData;
        private set => doubtCountData = value;
    }
    public string playerName
    {
        get => nameData;
        private set => nameData = value;
    }
    public bool doubt
    {
        get => doubtData;
        private set => doubtData = value;
    }

    public MapInfoScriptableObject myMapPosition
    {
        get => myMapPositionData;
        private set => myMapPositionData = value;
    }
    #endregion

    #region セッター

    // セッターは対応した変数に対しての書換えが実行できます
    // ただし、一部変数は追加するものだけの物も存在するので、注意してください
    // 追加するだけのものは Add が先頭につき、完全に変更できるものは Set が先頭についています

    public void AddMoney(int addAmount)
    {
        moneyData += addAmount;
    }

    public void SetID(int newID)
    {
        if (ID == -1) ID = newID;
        else Debug.Log("既にユーザーIDが設定されています");
    }

    public void SetDice(int newDice)
    {
        dice = newDice;
    }

    public void AddDoubtCount(int addAmount)
    {
        doubtCountData += addAmount;
    }

    public void SetPlayerName(string newName)
    {
        if (playerName == "nonPlayer") playerName = newName;
        else Debug.Log("既にユーザー名が設定されています");
    }

    public void SetDoubt(bool newFlg)
    {
        doubt = newFlg;
    }

    public void SetDoubtDice(int newDoubtDice)
    {
        doubtDiceData = newDoubtDice;
    }

    public void SetAction(bool newAction)
    {
        actionData = newAction;
    }

    public void SetMap(MapInfoScriptableObject newMap)
    {
        myMapPositionData = newMap;
    }
    #endregion
}
