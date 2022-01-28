using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MoneyEvent_ : EventBase
{
    [SerializeField, Header("このイベントによって変動する値")] private int value = 0;
    [SerializeField, Header("このイベントの名称")] private string eventName = "defaultName";
    public override void MapEvent()
    {
        List<PlayerStatus> statuses = GameStatus.lockMenber.GetTargetList_Player();

        for (int i = 0; i < statuses.Count; i++)
        {
            GameStatus.lockMenber.DrawMessage($"{statuses[i].playerName}は{eventName}によって{value}円失った...");
            statuses[i].AddMoney(value);
        }

        GameStatus.lockMenber.TargetPlayerClear();
    }

    public override string EventNameGet()
    {
        return eventName;
    }

    public override EventType EventTypeGet()
    {
        return EventType.MONEY;
    }

    public override void EventSynchronize()
    {

    }
}