using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : MonoBehaviour,IPhase
{
    public event Action NextPhase;
    public PlayerStatus playerStatus;

    private bool doubtFlg;



    private void Start()
    {
        //�V�X�e���̃����_����Unity����������
        int random = UnityEngine.Random.Range(1, 6);
        int random1 = UnityEngine.Random.Range(1, 6);
        playerStatus.SetDice(random);
        playerStatus.SetDoubtDice(random1);
        playerStatus.AddDoubtCount(5);
        playerStatus.SetDoubt(false);

        Debug.Log(playerStatus.dice);
        Debug.Log(playerStatus.doubtDice);
        Debug.Log(playerStatus.doubt);
    }

    private void Update()
    {
        
    }

    public void PhaseEnd()
    {
        
    }

    public void PhaseStart(PlayerStatus turnObject)
    {
        playerStatus = turnObject;
    }

    public void PhaseUpdate()
    {
        if(playerStatus.doubt)
        {
            PlayerSetMapDoubt();
        }
        else
        {
            PlayerSetMapTruth();
        }
        NextPhase();
    }

    /// <summary>
    /// �T�C�R���̖ڂ��R
    /// </summary>
    void PlayerSetMapDoubt()
    {
        // �R�̐��l���A�i��
        for (int i = 0; i < playerStatus.doubtDice; i++)
        {
            playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);
        }
        playerStatus.AddDoubtCount(1);
    }

    /// <summary>
    /// �T�C�R���̖ڂ�������
    /// </summary>
    void PlayerSetMapTruth()
    {
        // �T�C�R���̏o���ڒʂ�ɐi��
        for (int i = 0; i <= playerStatus.dice; i++)
        {
            // Vector3 nextPos = playerStatus.myMapPosition.nextMapData[0];
            // ���̏ꏊ����nextPos�ɐi�ޏ���
            playerStatus.SetMap(playerStatus.myMapPosition.nextMapData[0]);
        }
    }
}
