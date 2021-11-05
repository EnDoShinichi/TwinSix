using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStatus : MonoBehaviour
{
    // �O���PlayerStatus�ƈقȂ���̂�����PlayerStatus �V���A���C�Y������Ă���

    [SerializeField, NonSerialized] private int maneyData = 10000;

    [SerializeField, NonSerialized] private int ID = -1;

    [SerializeField, NonSerialized] private int diceData = 0;

    [SerializeField, NonSerialized] private int doubtDiceData = 0;

    [SerializeField, NonSerialized] private int doubtCountData = 0;

    [SerializeField, NonSerialized] private string nameData = "nonPlayer";

    [SerializeField, NonSerialized] private bool doubtData = false;

    [SerializeField, NonSerialized] private bool actionData = true;

    [SerializeField, NonSerialized] private int debugPosition = 0;

    private bool activeateFlg = false; // �������t���O


    // ���������s���N���X(���̂ݎ��s�\)
    public void StatusActiveate(int maney, int ID, int doubtCount, string name)
    {
        if (activeateFlg)
        {
            Debug.Log("���̃X�e�[�^�X�͊��ɏ���������Ă��܂�");
            return;
        }
        maneyData = maney;
        this.ID = ID;
        doubtCountData = doubtCount;
        nameData = name;
        actionData = true;
        debugPosition = 0;
        activeateFlg = true;
    }

    #region get�v���p�e�B

    // get�v���p�e�B�͂��̊֐����Ăяo�����ƂőΉ������ϐ����擾���邱�Ƃ��ł��܂�
    // �������Aget�݂̂̋L�q�ƂȂ�̂ŕϐ��̏��������͉��L�Z�b�^�[���g�p���Ă�������

    public int maney
    {
        get => maneyData;
        private set => maney = value;
    }
    public int id
    {
        get => ID;
        private set => ID = value;
    }
    public int dice
    {
        get => diceData;
        private set => diceData = value;
    }
    public int doubtDice
    {
        get => doubtDiceData;
        private set => doubtDiceData = value;
    }
    public int doubtCount
    {
        get => doubtCountData;
        private set => doubtCountData = value;
    }
    public string playerName
    {
        get => nameData;
        private set => nameData = value;
    }
    public bool doubt
    {
        get => doubtData;
        private set => doubtData = value;
    }
    public int position
    {
        get => debugPosition;
        private set => debugPosition = value;
    }
    #endregion

    #region �Z�b�^�[

    // �Z�b�^�[�͑Ή������ϐ��ɑ΂��Ă̏����������s�ł��܂�
    // �������A�ꕔ�ϐ��͒ǉ�������̂����̕������݂���̂ŁA���ӂ��Ă�������
    // �ǉ����邾���̂��̂� Add ���擪�ɂ��A���S�ɕύX�ł�����̂� Set ���擪�ɂ��Ă��܂�

    public void AddManey(int addAmount)
    {
        maneyData += addAmount;
    }

    public void SetID(int newID)
    {
        if (ID == -1) ID = newID;
        else Debug.Log("���Ƀ��[�U�[ID���ݒ肳��Ă��܂�");
    }

    public void SetDice(int newDice)
    {
        dice = newDice;
    }

    public void AddDoubtCount(int addAmount)
    {
        doubtCountData += addAmount;
    }

    public void SetPlayerName(string newName)
    {
        if (playerName == "nonPlayer") playerName = newName;
        else Debug.Log("���Ƀ��[�U�[�����ݒ肳��Ă��܂�");
    }

    public void SetDoubt(bool newFlg)
    {
        doubt = newFlg;
    }

    public void SetDoubtDice(int newDoubtDice)
    {
        doubtDiceData = newDoubtDice;
    }

    public void SetAction(bool newAction)
    {
        actionData = newAction;
    }

    public void AddPosition(int addAmount)
    {
        debugPosition += addAmount;
    }
    #endregion
}
