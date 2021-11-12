using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EventBase:MonoBehaviour
{
    /// <summary>
    /// マップのイベント内容を実行します
    /// </summary>
    public virtual void MapEvent()
    {
        Debug.LogError("直接EventBaseにアクセスされています");
    }

    /// <summary>
    /// イベントの名称を返却します
    /// </summary>
    /// <returns>イベントの名称</returns>
    public virtual string EventNameGet()
    {
        Debug.LogError("直接EventBaseにアクセスされています");
        return null;
    }

    /// <summary>
    /// イベントタイプを返却します
    /// </summary>
    /// <returns>イベントタイプ</returns>
    public virtual EventType EventTypeGet()
    {
        Debug.LogError("直接EventBaseにアクセスされています");
        return EventType.NONE;
    }
}
