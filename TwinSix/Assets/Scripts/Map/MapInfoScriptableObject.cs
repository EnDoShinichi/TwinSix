using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventType
{
    MONEY,
    STAND,
    MOVE,
    UNIQUE,
    NONE
}

[Serializable]
public class MapInfoScriptableObject : ScriptableObject
{
    [Header("�}�b�v�̖���")] [SerializeField] private string mapNameData = "newMapName";

    [Header("�}�b�v�̍��W")] [SerializeField] private Vector3 mapPositionData = Vector3.zero;

    [Header("���̃}�b�v�ɑ��݂���C�x���g")] [SerializeField] public EventBase mapEventData;

    [Header("���̃}�b�v�̎��̃}�b�v�ꗗ")] [SerializeField] public MapInfoScriptableObject[] nextMapData;

    [Header("���̃}�b�v�̑O�̃}�b�v�ꗗ")] [SerializeField] public MapInfoScriptableObject[] beforeMapData;

    [HideInInspector] private string defaultMapName;

    [HideInInspector] private Vector3 defaultMapPosition;

    [HideInInspector] private EventBase defaultEventData;

    [HideInInspector] private MapInfoScriptableObject[] defaultNextMapData;

    [HideInInspector] private MapInfoScriptableObject[] defaultBeforeMapData;

    private bool lockFlg = false;

    // �X�N���v�^�u���I�u�W�F�N�g�̒l��ݒ肷��ۂɌĂԊ֐�
    public void ActivateScriptableObject(string name, Vector3 position, EventBase eventData, MapInfoScriptableObject[] nexts, MapInfoScriptableObject[] befores)
    {
        if (lockFlg) return;
        mapName = name;
        defaultMapName = name;
        mapPositionData = position;
        defaultMapPosition = position;
        mapEventData = eventData;
        defaultEventData = eventData;
        nextMapData = nexts;
        defaultNextMapData = nexts;
        beforeMapData = befores;
        defaultBeforeMapData = befores;

        lockFlg = true;
    }

    // �R���p�C�����̃f�[�^�󋵂ɖ߂��֐�
    public void ResetData()
    {
        mapNameData = defaultMapName;
        mapPositionData = defaultMapPosition;
        mapEventData = defaultEventData;
        nextMapData = defaultNextMapData;
        beforeMapData = defaultBeforeMapData;
    }

    // �R���p�C������̒l��ۊǗp�̒l�ɐݒ�
    public void Initialize()
    {
        defaultMapName = mapNameData;
        defaultMapPosition = mapPositionData;
        defaultEventData = mapEventData;
        defaultNextMapData = nextMapData;
        defaultBeforeMapData = beforeMapData;
    }

    // mapName�̃v���p�e�B
    public string mapName
    {
        get => mapNameData;
        private set => mapNameData = value;
    }

    // mapPosition�̃v���p�e�B
    public Vector3 mapPosition
    {
        get => mapPositionData;
        private set => mapPositionData = value;
    }

    public void OnValidate()
    {
        // �C���X�y�N�^�[����̕ϐ��ϓ����ɌĂ΂��֐�
    }
}