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
    // �v���p�e�B��
    private string sceneName = "sceneName";
    protected SceneAsset GetSceneObject(string name)
    {
        // ���O�������Ȃ�Null��Ԃ�
        if (string.IsNullOrEmpty(name)) return null;
        //  Build�����V�[���̐���������
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            // �m�F����V�[��
            EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
            // �V�[���ԍ������̒l���ᖳ���Ȃ�
            if (scene.path.IndexOf(name) != -1)
                return AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
        }
        // �S�ẴV�[�����m�F���Ă�������Ȃ��Ȃ�Null��Ԃ�
        return null;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // �V�[�����v���p�e�B
        SerializedProperty sceneProp = property.FindPropertyRelative(sceneName);
        SceneAsset sceneObj = GetSceneObject(sceneProp.stringValue);
        // �V�[���̃I�u�W�F�N�g   
        var newScene = EditorGUI.ObjectField(position, label, sceneObj, typeof(SceneAsset), false);
        // �I�u�W�F�N�g�������Ȃ�V�[���������
        if (newScene == null) sceneProp.stringValue = "";
        else
        {
            if (newScene.name != sceneProp.stringValue)
            {
                // �V�[����Asset�����猟��
                SceneAsset scnObj = GetSceneObject(newScene.name);
                // �������Č�����Ȃ�������x��
                if (scnObj == null) Debug.LogWarning
                        ($"The scene {newScene.name} can't be used. add it build setting for the project.");
                else sceneProp.stringValue = newScene.name;
            }
        }
    }
}
#endif
