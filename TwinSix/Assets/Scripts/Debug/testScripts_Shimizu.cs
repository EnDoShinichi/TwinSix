using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScripts_Shimizu : MonoBehaviour
{
    [SerializeField] private MapInfoScriptableObject debugPos;
    [SerializeField] private EventBase baseData;
    [SerializeField] private GameObject playerData;
    PlayerStatus status;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(playerData);
        status = obj.GetComponent<PlayerStatus>();
        status.StatusActiveate(1000,1,3,"player",debugPos);
        GameStatus.lockMenber.TargetListBind_Player(status);
        Debug.Log($"更新前({status.money})");
        baseData.MapEvent();
        Debug.Log($"更新後({status.money})");
        Debug.Log($"このイベントの名前は{baseData.EventNameGet()}です");
        Debug.Log($"このマップの名前は{debugPos.name}で、次のマップ名は{debugPos.nextMapData[0].name}です");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
