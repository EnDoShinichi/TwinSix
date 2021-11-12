using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapCreater : EditorWindow
{
    const int MAX_MAPCOUNT = 10;

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
    EventBase eventType = null;
    List<MapInfoScriptableObject> next = new List<MapInfoScriptableObject>();
    List<MapInfoScriptableObject> before = new List<MapInfoScriptableObject>();

    int nextMapCount = 1;
    int beforeMapCount = 1;
    private void OnGUI()
    {
        if (instance)
        {
            newScritable();
        }

        assetName = EditorGUILayout.TextField("�쐬����f�[�^��", assetName);

        mapName = EditorGUILayout.TextField("�}�b�v�̖���", mapName);
        mapPosition = EditorGUILayout.Vector3Field("�쐬�Ώۂ�transform.position", mapPosition);
        eventType = EditorGUILayout.ObjectField("�쐬�}�b�v�̃C�x���g", eventType, typeof(EventBase), true) as EventBase;

        nextMapCount = EditorGUILayout.IntField("����ړ���̃}�b�v�f�[�^��", nextMapCount);

        if (nextMapCount > MAX_MAPCOUNT)
        {
            Debug.Log("����ړ���f�[�^����" + MAX_MAPCOUNT + "�𒴂���l�ɐݒ肷�邱�Ƃ͂ł��܂���(�ߏ�Ȓl�̐ݒ�h�~�̂���)");
            nextMapCount = MAX_MAPCOUNT;
        }

        if (nextMapCount < 0)
        {
            Debug.Log("����ړ���f�[�^����0�������l���ݒ肳��܂����@0�ɒu���������܂�");
            nextMapCount = 0;
        }

        if (nextMapCount >= next.Count && nextMapCount <= MAX_MAPCOUNT)
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

        beforeMapCount = EditorGUILayout.IntField("�O��ړ���̃}�b�v�f�[�^��",beforeMapCount);

        if (beforeMapCount > MAX_MAPCOUNT)
        {
            Debug.Log("�O��ړ���f�[�^����" + MAX_MAPCOUNT + "�𒴂���l�ɐݒ肷�邱�Ƃ͂ł��܂���(�ߏ�Ȓl�̐ݒ�h�~�̂���)");
            beforeMapCount = MAX_MAPCOUNT;
        }

        if (nextMapCount < 0)
        {
            Debug.Log("�O��ړ���f�[�^����0�������l���ݒ肳��܂����@0�ɒu���������܂�");
            beforeMapCount = 0;
        }

        if (beforeMapCount >= before.Count && beforeMapCount <= MAX_MAPCOUNT)
        {
            for (int i = before.Count; i < beforeMapCount; i++)
            {
                before.Add(null);
            }
        }
        else if (beforeMapCount < before.Count && beforeMapCount > 0)
        {
            for (int i = before.Count - 1; i > beforeMapCount; i--)
            {
                before.RemoveAt(i);
            }
        }

        for (int i = 0;i < beforeMapCount;i++)
        {
            before[i] = EditorGUILayout.ObjectField($"{i + 1}:", before[i], typeof(MapInfoScriptableObject), true) as MapInfoScriptableObject;
        }

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
        eventType = null;

        for (int i = 0; i < next.Count; i++)
        {
            next[i] = null;
        }

        for (int i = 0;i < before.Count; i++)
        {
            before[i] = null;
        }

        instance = false;
    }

    void SaveData()
    {
        scriptableObject.ActivateScriptableObject(mapName, mapPosition, eventType, next.ToArray(), before.ToArray());
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
