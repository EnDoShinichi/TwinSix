using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventType
{
    MANEY,
    STAND,
    MOVE,
    NONE
}

[Serializable]
public class MapInfoScriptableObject : ScriptableObject
{
    [Header("マップの名称")][SerializeField] public string mapName = "newMapName";

    [Header("マップの座標")][SerializeField] public Vector3 mapPosition = Vector3.zero;

    [Header("マップに設定するイベント")][SerializeField] public EventType type = EventType.NONE;

    [Header("このマップの次のマップ一覧")][SerializeField] public MapInfoScriptableObject[] nextMap;

    [Header("このマップの前のマップ")][SerializeField] public MapInfoScriptableObject beforeMap;

    public void OnValidate()
    {
        // インスペクターからの変数変動時に呼ばれる関数
    }
}
