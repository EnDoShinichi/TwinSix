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
        //�V�X�e���̃����_����Unity����������
        int random = UnityEngine.Random.Range(1, 6);
        int random1 = UnityEngine.Random.Range(1, 6);
        playerStatus.SetDice(3);
        playerStatus.SetDoubtDice(4);
        playerStatus.SetDoubt(false);

        playerStatus.SetMap(playerMap);

        Debug.Log($"�^({playerStatus.dice})");
        Debug.Log($"�U({playerStatus.doubtDice})");
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
    /// �T�C�R���̖ڂ��R
    /// </summary>
    IEnumerator PlayerSetMapDoubt()
    {
        // �R�̐��l���A�i��
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
    /// �T�C�R���̖ڂ�������
    /// </summary>
    IEnumerator PlayerSetMapTruth()
    {
        // �T�C�R���̏o���ڒʂ�ɐi��
        for (int i = 0; i < playerStatus.dice; i++)
        {
            // Vector3 nextPos = playerStatus.myMapPosition.nextMapData[0];
            // ���̏ꏊ����nextPos�ɐi�ޏ���
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
    /// �����ꓹ�����邩�ǂ���
    /// </summary>
    void ChoiceRoad()
    {
        if (playerStatus.myMapPosition.nextMapData.Length >= 2)
        {
            choiceFlg = true;

            // �i�s���������߂鏈����ǉ����Ȃ��ƍs���Ȃ�
        }
        else playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);
    }
}
