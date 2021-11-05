using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventType
{
    MANEY,
    STAND,
    MOVE,
    NONE
}

[Serializable]
public class MapInfoScriptableObject : ScriptableObject
{
    [Header("�}�b�v�̖���")][SerializeField] public string mapName = "newMapName";

    [Header("�}�b�v�̍��W")][SerializeField] public Vector3 mapPosition = Vector3.zero;

    [Header("�}�b�v�ɐݒ肷��C�x���g")][SerializeField] public EventType type = EventType.NONE;

    [Header("���̃}�b�v�̎��̃}�b�v�ꗗ")][SerializeField] public MapInfoScriptableObject[] nextMap;

    [Header("���̃}�b�v�̑O�̃}�b�v")][SerializeField] public MapInfoScriptableObject beforeMap;

    public void OnValidate()
    {
        // �C���X�y�N�^�[����̕ϐ��ϓ����ɌĂ΂��֐�
    }
}
