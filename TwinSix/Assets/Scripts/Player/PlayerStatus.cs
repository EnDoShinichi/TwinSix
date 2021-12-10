using System;
using UnityEngine;

[Serializable]
public class PlayerStatus : MonoBehaviour
{
    // �O���PlayerStatus�ƈقȂ���̂�����PlayerStatus �V���A���C�Y������Ă���

    [SerializeField, NonSerialized] private int moneyData = 10000;

    [SerializeField, NonSerialized] private int ID = -1;

    [SerializeField, NonSerialized] private int diceData = 0;

    [SerializeField, NonSerialized] private int doubtDiceData = 0;

    [SerializeField, NonSerialized] private int doubtCountData = 0;

    [SerializeField, NonSerialized] private string nameData = "nonPlayer";

    [SerializeField, NonSerialized] private bool doubtData = false;

    [SerializeField, NonSerialized] private bool actionData = true;

    [SerializeField, NonSerialized] private MapInfoScriptableObject myMapPositionData;

    private bool activeateFlg = false; // �������t���O


    // ���������s���N���X(���̂ݎ��s�\)
    public void StatusActiveate(int money, int ID, int doubtCount, string name, MapInfoScriptableObject defaultPosition)
    {
        if (activeateFlg)
        {
            Debug.Log("���̃X�e�[�^�X�͊��ɏ���������Ă��܂�");
            return;
        }
        moneyData = money;
        this.ID = ID;
        doubtCountData = doubtCount;
        nameData = name;
        actionData = true;
        activeateFlg = true;
        myMapPosition = defaultPosition;
    }

    #region get�v���p�e�B

    // get�v���p�e�B�͂��̊֐����Ăяo�����ƂőΉ������ϐ����擾���邱�Ƃ��ł��܂�
    // �������Aget�݂̂̋L�q�ƂȂ�̂ŕϐ��̏��������͉��L�Z�b�^�[���g�p���Ă�������

    public int money
    {
        get => moneyData;
        private set => moneyData = value;
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

    public MapInfoScriptableObject myMapPosition
    {
        get => myMapPositionData;
        private set => myMapPositionData = value;
    }
    #endregion

    #region �Z�b�^�[

    // �Z�b�^�[�͑Ή������ϐ��ɑ΂��Ă̏����������s�ł��܂�
    // �������A�ꕔ�ϐ��͒ǉ�������̂����̕������݂���̂ŁA���ӂ��Ă�������
    // �ǉ����邾���̂��̂� Add ���擪�ɂ��A���S�ɕύX�ł�����̂� Set ���擪�ɂ��Ă��܂�

    public void AddMoney(int addAmount)
    {
        moneyData += addAmount;
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

    public void SetMap(MapInfoScriptableObject newMap)
    {
        myMapPositionData = newMap;
    }
    #endregion
}
