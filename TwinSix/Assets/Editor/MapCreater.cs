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

        assetName = EditorGUILayout.TextField("作成するデータ名", assetName);

        mapName = EditorGUILayout.TextField("マップの名称", mapName);
        mapPosition = EditorGUILayout.Vector3Field("作成対象のtransform.position", mapPosition);
        eventType = (EventType)EditorGUILayout.EnumPopup("作成マスのイベント", eventType);

        nextMapCount = EditorGUILayout.IntField("次回移動先に設定されるマップデータ数", nextMapCount);

        if (nextMapCount > MAX_NEXTCOUNT)
        {
            Debug.Log("次回移動先データ数を" + MAX_NEXTCOUNT + "を超える値に設定することはできません(過剰な値の設定防止のため)");
            nextMapCount = MAX_NEXTCOUNT;
        }

        if (nextMapCount < 0)
        {
            Debug.Log("次回移動先データ数に0を下回る値が設定されました　0に置き換えられます");
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
            next[i] = EditorGUILayout.ObjectField("次のマップ情報", next[i], typeof(MapInfoScriptableObject), true) as MapInfoScriptableObject;
        }

        before = EditorGUILayout.ObjectField("一個前のマップ情報", before, typeof(MapInfoScriptableObject), true) as MapInfoScriptableObject;

        scriptableObject.mapName = mapName;
        scriptableObject.mapPosition = mapPosition;
        scriptableObject.type = eventType;
        scriptableObject.nextMap = next.ToArray();
        scriptableObject.beforeMap = before;

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
            // 創れない場合の処理
        }

        EditorUtility.SetDirty(scriptableObject);
        AssetDatabase.SaveAssets();
    }
}
