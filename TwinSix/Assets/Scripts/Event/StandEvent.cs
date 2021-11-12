using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandEvent : EventBase
{
    [SerializeField, Header("���̃C�x���g�̖���")] private string eventName = "defaultName";

    public override void MapEvent()
    {
        List<PlayerStatus> statuses = GameStatus.lockMenber.GetTargetList_Player();

        for (int i = 0;i < statuses.Count;i++)
        {
            statuses[i].SetAction(false);
        }
    }

    public override string EventNameGet()
    {
        return eventName;
    }

    public override EventType EventTypeGet()
    {
        return EventType.STAND;
    }
}
