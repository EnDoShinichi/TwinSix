using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary> �V�[���I�u�W�F�N�g�N���X </summary>
public class SceneObject
{
    // �V�[����
    [SerializeField] private string sceneName;
    /// <summary> �V�[���� </summary>
    /// <param name="obj">�V�[���I�u�W�F�N�g</param>
    public static implicit operator string(SceneObject obj) { return obj.sceneName; }
    /// <summary> �V�[�� </summary>
    /// <param name="name">�V�[����</param>
    public static implicit operator SceneObject(string name)
    {
        return new SceneObject() { sceneName = name };
    }
}
