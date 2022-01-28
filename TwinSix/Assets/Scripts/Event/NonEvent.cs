using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEvent : EventBase
{
    [SerializeField, Header("このイベントによって変動する値")] private int value = 0;
    [SerializeField, Header("このイベントの名称")] private string eventName = "defaultName";

    public override void MapEvent()
    {

    }

    public override string EventNameGet()
    {
        return eventName;
    }

    public override EventType EventTypeGet()
    {
        return EventType.NONE;
    }
}
