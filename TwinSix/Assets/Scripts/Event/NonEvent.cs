using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEvent : EventBase
{
    [SerializeField, Header("���̃C�x���g�ɂ���ĕϓ�����l")] private int value = 0;
    [SerializeField, Header("���̃C�x���g�̖���")] private string eventName = "defaultName";

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
