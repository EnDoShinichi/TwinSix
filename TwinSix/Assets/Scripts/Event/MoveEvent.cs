using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : EventBase
{
    [SerializeField,Header("���̃C�x���g�ɂ���ĕϓ�����l")] private int value = 0;
    [SerializeField,Header("���̃C�x���g�̖���")] private string eventName = "defaultName";

    public override void MapEvent()
    {
        List<PlayerStatus> statuses = GameStatus.lockMenber.GetTargetList_Player();

        for (int i = 0;i < statuses.Count;i++)
        {
            for (int j = 0; j < value; j++)
            {
                statuses[i].SetMap(statuses[i].myMapPosition.nextMapData[0]);
            }
        }

    }

    public override string EventNameGet()
    {
        return eventName;
    }

    public override EventType EventTypeGet()
    {
        return EventType.MOVE;
    }
}
