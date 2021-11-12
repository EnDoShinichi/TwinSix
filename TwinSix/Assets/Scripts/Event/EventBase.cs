using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EventBase:MonoBehaviour
{
    /// <summary>
    /// �}�b�v�̃C�x���g���e�����s���܂�
    /// </summary>
    public virtual void MapEvent()
    {
        Debug.LogError("����EventBase�ɃA�N�Z�X����Ă��܂�");
    }

    /// <summary>
    /// �C�x���g�̖��̂�ԋp���܂�
    /// </summary>
    /// <returns>�C�x���g�̖���</returns>
    public virtual string EventNameGet()
    {
        Debug.LogError("����EventBase�ɃA�N�Z�X����Ă��܂�");
        return null;
    }

    /// <summary>
    /// �C�x���g�^�C�v��ԋp���܂�
    /// </summary>
    /// <returns>�C�x���g�^�C�v</returns>
    public virtual EventType EventTypeGet()
    {
        Debug.LogError("����EventBase�ɃA�N�Z�X����Ă��܂�");
        return EventType.NONE;
    }
}
