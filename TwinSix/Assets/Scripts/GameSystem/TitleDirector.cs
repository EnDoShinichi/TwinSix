using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    bool inputFlg = false; // ���̓t���O
    // �v���C���[���
    //PlayerStatus playerStatus;
    // ���O���͗�
    [SerializeField] InputField nameField;
    // �ڍs��
    [SerializeField] SceneObject nextScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary> ���O���� </summary>
    public void Matching()
    {
        if (inputFlg || nameField.text == null) return; // 1�񂾂�����
        PlayerStatus playerStatus = new PlayerStatus();
        // ���O����ݒ�
        playerStatus.SetPlayerName(nameField.text);
        GameStatus.lockMenber.statusSeter = playerStatus;
        inputFlg = true;
        // �V�[���ψ�
        //SceneManager
    }
}
