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
        Debug.Log($"�X�V�O({status.money})");
        baseData.MapEvent();
        Debug.Log($"�X�V��({status.money})");
        Debug.Log($"���̃C�x���g�̖��O��{baseData.EventNameGet()}�ł�");
        Debug.Log($"���̃}�b�v�̖��O��{debugPos.name}�ŁA���̃}�b�v����{debugPos.nextMapData[0].name}�ł�");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
