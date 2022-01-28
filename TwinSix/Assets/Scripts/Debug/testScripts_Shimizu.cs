using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class testScripts_Shimizu : MonoBehaviour
{
    [SerializeField] private MapInfoScriptableObject debugPos;
    [SerializeField] private EventBase baseData;
    [SerializeField] private GameObject playerData;
    [SerializeField] private GameObject result;
    [SerializeField] private MapInfoScriptableObject debug;
    PlayerStatus status;
    PlayerStatus status2;
    ResultManager data;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(playerData);
        status = obj.GetComponent<PlayerStatus>();
        // status.StatusActiveate(2000, 1, 3, "player", debugPos);
        GameObject obj2 = Instantiate(playerData);
        status2 = obj2.GetComponent<PlayerStatus>();
        // status2.StatusActiveate(1000, 1, 3, "player2", debugPos);
        data = result.GetComponent<ResultManager>();
        List<PlayerStatus> state = new List<PlayerStatus>();
        state.Add(status);
        state.Add(status2);

        debug.name = "abcde";

        data.setRank = state;
        GameStatus.lockMenber.TargetListBind_Player(status);
        GameStatus.lockMenber.TargetListBind_Player(status2);
        Debug.Log($"�X�V�O({data.getRunk[0].playerName}),({data.getRunk[1].playerName})");

        state[1].AddMoney(2000);
        IOrderedEnumerable<PlayerStatus> rankList = state.OrderByDescending(state => state.money);
        state = rankList.ToList();
        data.setRank = rankList.ToList();

        Debug.Log($"�X�V��({data.getRunk[0].playerName}({data.getRunk[0].money})),({data.getRunk[1].playerName}({data.getRunk[1].money}))");
        //Debug.Log($"���̃C�x���g�̖��O��{baseData.EventNameGet()}�ł�");
        //Debug.Log($"���̃}�b�v�̖��O��{debugPos.name}�ŁA���̃}�b�v����{debugPos.nextMapData[0].name}�ł�");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
