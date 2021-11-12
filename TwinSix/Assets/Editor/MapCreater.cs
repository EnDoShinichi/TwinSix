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

        assetName = EditorGUILayout.TextField("作成するデータ名", assetName);

        mapName = EditorGUILayout.TextField("マップの名称", mapName);
        mapPosition = EditorGUILayout.Vector3Field("作成対象のtransform.position", mapPosition);
        eventType = EditorGUILayout.ObjectField("作成マップのイベント", eventType, typeof(EventBase), true) as EventBase;

        nextMapCount = EditorGUILayout.IntField("次回移動先のマップデータ数", nextMapCount);

        if (nextMapCount > MAX_MAPCOUNT)
        {
            Debug.Log("次回移動先データ数を" + MAX_MAPCOUNT + "を超える値に設定することはできません(過剰な値の設定防止のため)");
            nextMapCount = MAX_MAPCOUNT;
        }

        if (nextMapCount < 0)
        {
            Debug.Log("次回移動先データ数に0を下回る値が設定されました　0に置き換えられます");
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
            next[i] = EditorGUILayout.ObjectField("次のマップ情報", next[i], typeof(MapInfoScriptableObject), true) as MapInfoScriptableObject;
        }

        beforeMapCount = EditorGUILayout.IntField("前回移動先のマップデータ数",beforeMapCount);

        if (beforeMapCount > MAX_MAPCOUNT)
        {
            Debug.Log("前回移動先データ数を" + MAX_MAPCOUNT + "を超える値に設定することはできません(過剰な値の設定防止のため)");
            beforeMapCount = MAX_MAPCOUNT;
        }

        if (nextMapCount < 0)
        {
            Debug.Log("前回移動先データ数に0を下回る値が設定されました　0に置き換えられます");
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

        if (GUILayout.Button("データ保存"))
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
            // 創れない場合の処理
        }

        EditorUtility.SetDirty(scriptableObject);
        AssetDatabase.SaveAssets();
    }
}
