using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary> シーンオブジェクトクラス </summary>
public class SceneObject
{
    // シーン名
    [SerializeField] private string sceneName;
    /// <summary> シーン名 </summary>
    /// <param name="obj">シーンオブジェクト</param>
    public static implicit operator string(SceneObject obj) { return obj.sceneName; }
    /// <summary> シーン </summary>
    /// <param name="name">シーン名</param>
    public static implicit operator SceneObject(string name)
    {
        return new SceneObject() { sceneName = name };
    }
}
