using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapCreater : EditorWindow
{
    const int MAX_NEXTCOUNT = 10;

    [MenuItem("Window/MapCreater")]
    static void WindowOpen()
    {
        EditorWindow.GetWindow<MapCreater>("MapCreater");
    }

    MapInfoScriptableObject scriptableObject;
    bool instance = true;

    string assetName;

    string mapName = "new mapName...";
    Vector3 mapPosition = Vector3.zero;
    EventType eventType = EventType.NONE;
    List<MapInfoScriptableObject> next = new List<MapInfoScriptableObject>();
    MapInfoScriptableObject before = null;

    int nextMapCount = 1;
    private void OnGUI()
    {
        if (instance)
        {
            newScritable();
        }

        assetName = EditorGUILayout.TextField("�쐬����f�[�^��", assetName);

        mapName = EditorGUILayout.TextField("�}�b�v�̖���", mapName);
        mapPosition = EditorGUILayout.Vector3Field("�쐬�Ώۂ�transform.position", mapPosition);
        eventType = (EventType)EditorGUILayout.EnumPopup("�쐬�}�X�̃C�x���g", eventType);

        nextMapCount = EditorGUILayout.IntField("����ړ���ɐݒ肳���}�b�v�f�[�^��", nextMapCount);

        if (nextMapCount > MAX_NEXTCOUNT)
        {
            Debug.Log("����ړ���f�[�^����" + MAX_NEXTCOUNT + "�𒴂���l�ɐݒ肷�邱�Ƃ͂ł��܂���(�ߏ�Ȓl�̐ݒ�h�~�̂���)");
            nextMapCount = MAX_NEXTCOUNT;
        }

        if (nextMapCount < 0)
        {
            Debug.Log("����ړ���f�[�^����0�������l���ݒ肳��܂����@0�ɒu���������܂�");
            nextMapCount = 0;
        }

        if (nextMapCount >= next.Count && nextMapCount <= MAX_NEXTCOUNT)
        {
            for (int i = next.Count; i < nextMapCount; i++)
            {
                next.Add(null);
            }
        }
        else if (nextMapCount < next.Count && nextMapCount > 0)
        {
            for (int i = next.Count - 1; i > nextMapCount; i--)
            {
                next.RemoveAt(i);
            }
        }

        for (int i = 0; i < nextMapCount; i++)
        {
            next[i] = EditorGUILayout.ObjectField("���̃}�b�v���", next[i], typeof(MapInfoScriptableObject), true) as MapInfoScriptableObject;
        }

        before = EditorGUILayout.ObjectField("��O�̃}�b�v���", before, typeof(MapInfoScriptableObject), true) as MapInfoScriptableObject;

        scriptableObject.mapName = mapName;
        scriptableObject.mapPosition = mapPosition;
        scriptableObject.type = eventType;
        scriptableObject.nextMap = next.ToArray();
        scriptableObject.beforeMap = before;

        if (GUILayout.Button("�f�[�^�ۑ�"))
        {
            SaveData();
            instance = true;
        }
    }

    void MapCounter()
    {

    }

    void newScritable()
    {
        scriptableObject = ScriptableObject.CreateInstance<MapInfoScriptableObject>();

        mapName = "new mapName...";
        mapPosition = Vector3.zero;
        eventType = EventType.NONE;
        before = null;

        for (int i = 0; i < next.Count; i++)
        {
            next[i] = null;
        }

        instance = false;
    }

    void SaveData()
    {
        const string PATH = "Assets/CreateData/";
        string name = assetName + ".asset";
        var asset = AssetDatabase.LoadAssetAtPath(PATH + name, typeof(MapInfoScriptableObject));

        if (asset == null)
        {
            AssetDatabase.CreateAsset(scriptableObject, PATH + name);
        }
        else
        {
            // �n��Ȃ��ꍇ�̏���
        }

        EditorUtility.SetDirty(scriptableObject);
        AssetDatabase.SaveAssets();
    }
}
