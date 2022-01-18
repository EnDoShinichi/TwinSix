using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : MonoBehaviour,IPhase
{
    public event Action NextPhase;
    public PlayerStatus playerStatus;

    [SerializeField] private float waitTime;
    [SerializeField] private MapInfoScriptableObject playerMap;

    private bool choiceFlg;

    /*private void Start()
    {
        //システムのランダムとUnityが競合した
        int random = UnityEngine.Random.Range(1, 6);
        int random1 = UnityEngine.Random.Range(1, 6);
        playerStatus.SetDice(3);
        playerStatus.SetDoubtDice(4);
        playerStatus.SetDoubt(false);

        playerStatus.SetMap(playerMap);

        Debug.Log($"真({playerStatus.dice})");
        Debug.Log($"偽({playerStatus.doubtDice})");
        //Debug.Log(playerStatus.doubt);
        //Debug.Log(playerStatus.myMapPosition.nextMapData.Length);

        PhaseStart(playerStatus);
    }*/

    public void PhaseEnd()
    {
        
    }

    public void PhaseStart(PlayerStatus turnObject)
    {
        playerStatus = turnObject;

        if (playerStatus.doubt) StartCoroutine(PlayerSetMapDoubt());
        else StartCoroutine(PlayerSetMapTruth());

        
    }

    public void PhaseUpdate()
    {
        
    }

    /// <summary>
    /// サイコロの目が嘘
    /// </summary>
    IEnumerator PlayerSetMapDoubt()
    {
        // 嘘の数値分、進む
        for (int i = 0; i < playerStatus.doubtDice; i++)
        {
            //playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);

            ChoiceRoad();

            if (choiceFlg == true) break;

            if (playerStatus.myMapPosition.mapEventData.EventTypeGet() == EventType.STAND) break;

            yield return new WaitForSeconds(waitTime);

            //Debug.Log("asd");
        }
        playerStatus.AddDoubtCount(1);
        choiceFlg = false;
        NextPhase();
    }

    /// <summary>
    /// サイコロの目が正しい
    /// </summary>
    IEnumerator PlayerSetMapTruth()
    {
        // サイコロの出た目通りに進む
        for (int i = 0; i < playerStatus.dice; i++)
        {
            // Vector3 nextPos = playerStatus.myMapPosition.nextMapData[0];
            // 今の場所からnextPosに進む処理
            //playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);

            ChoiceRoad();

            if (choiceFlg == true) break;
            
            if (playerStatus.myMapPosition.mapEventData.EventTypeGet() == EventType.STAND) break;

            yield return new WaitForSeconds(waitTime);

            //Debug.Log("fgh");
        }
        choiceFlg = false;
        NextPhase();
    }

    /// <summary>
    /// 分かれ道があるかどうか
    /// </summary>
    void ChoiceRoad()
    {
        if (playerStatus.myMapPosition.nextMapData.Length >= 2)
        {
            choiceFlg = true;

            // 進行方向を決める処理を追加しないと行けない
        }
        else playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);
    }
}
