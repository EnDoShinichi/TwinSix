using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventType
{
    MONEY,
    STAND,
    MOVE,
    UNIQUE,
    NONE
}

[Serializable]
public class MapInfoScriptableObject : ScriptableObject
{
    [Header("マップの名称")] [SerializeField] private string mapNameData = "newMapName";

    [Header("マップの座標")] [SerializeField] private Vector3 mapPositionData = Vector3.zero;

    [Header("このマップに存在するイベント")] [SerializeField] public EventBase mapEventData;

    [Header("このマップの次のマップ一覧")] [SerializeField] public MapInfoScriptableObject[] nextMapData;

    [Header("このマップの前のマップ一覧")] [SerializeField] public MapInfoScriptableObject[] beforeMapData;

    [HideInInspector] private string defaultMapName;

    [HideInInspector] private Vector3 defaultMapPosition;

    [HideInInspector] private EventBase defaultEventData;

    [HideInInspector] private MapInfoScriptableObject[] defaultNextMapData;

    [HideInInspector] private MapInfoScriptableObject[] defaultBeforeMapData;

    private bool lockFlg = false;

    // スクリプタブルオブジェクトの値を設定する際に呼ぶ関数
    public void ActivateScriptableObject(string name, Vector3 position, EventBase eventData, MapInfoScriptableObject[] nexts, MapInfoScriptableObject[] befores)
    {
        if (lockFlg) return;
        mapName = name;
        defaultMapName = name;
        mapPositionData = position;
        defaultMapPosition = position;
        mapEventData = eventData;
        defaultEventData = eventData;
        nextMapData = nexts;
        defaultNextMapData = nexts;
        beforeMapData = befores;
        defaultBeforeMapData = befores;

        lockFlg = true;
    }

    // コンパイル時のデータ状況に戻す関数
    public void ResetData()
    {
        mapNameData = defaultMapName;
        mapPositionData = defaultMapPosition;
        mapEventData = defaultEventData;
        nextMapData = defaultNextMapData;
        beforeMapData = defaultBeforeMapData;
    }

    // コンパイル直後の値を保管用の値に設定
    public void Initialize()
    {
        defaultMapName = mapNameData;
        defaultMapPosition = mapPositionData;
        defaultEventData = mapEventData;
        defaultNextMapData = nextMapData;
        defaultBeforeMapData = beforeMapData;
    }

    // mapNameのプロパティ
    public string mapName
    {
        get => mapNameData;
        private set => mapNameData = value;
    }

    // mapPositionのプロパティ
    public Vector3 mapPosition
    {
        get => mapPositionData;
        private set => mapPositionData = value;
    }

    public void OnValidate()
    {
        // インスペクターからの変数変動時に呼ばれる関数
    }
}