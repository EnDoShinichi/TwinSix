using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    // �v���C���[���
    //PlayerStatus playerStatus;
    // ���O���͗�
    [SerializeField] InputField nameField;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary> ���O���� </summary>
    public void InputName()
    {
        PlayerStatus playerStatus = new PlayerStatus();
        playerStatus.SetPlayerName(nameField.text);
        GameStatus.lockMenber.statusSeter = playerStatus;
        // �V�[���ψ�
    }
}
