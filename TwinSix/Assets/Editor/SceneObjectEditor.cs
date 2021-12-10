using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneObject))]
public class SceneObjectEditor : PropertyDrawer
{
    // プロパティ名
    private string sceneName = "sceneName";
    protected SceneAsset GetSceneObject(string name)
    {
        // 名前が無いならNullを返す
        if (string.IsNullOrEmpty(name)) return null;
        //  Buildされるシーンの数だけ処理
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            // 確認するシーン
            EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
            // シーン番号が負の値じゃ無いなら
            if (scene.path.IndexOf(name) != -1)
                return AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
        }
        // 全てのシーンを確認しても見つからないならNullを返す
        return null;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // シーン名プロパティ
        SerializedProperty sceneProp = property.FindPropertyRelative(sceneName);
        SceneAsset sceneObj = GetSceneObject(sceneProp.stringValue);
        // シーンのオブジェクト   
        var newScene = EditorGUI.ObjectField(position, label, sceneObj, typeof(SceneAsset), false);
        // オブジェクトが無いならシーン名を空に
        if (newScene == null) sceneProp.stringValue = "";
        else
        {
            if (newScene.name != sceneProp.stringValue)
            {
                // シーンをAsset内から検索
                SceneAsset scnObj = GetSceneObject(newScene.name);
                // 検索して見つからなかったら警告
                if (scnObj == null) Debug.LogWarning
                        ($"The scene {newScene.name} can't be used. add it build setting for the project.");
                else sceneProp.stringValue = newScene.name;
            }
        }
    }
}
#endif
