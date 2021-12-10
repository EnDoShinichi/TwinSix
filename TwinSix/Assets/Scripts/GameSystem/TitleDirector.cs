using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    // プレイヤー情報
    //PlayerStatus playerStatus;
    // 名前入力欄
    [SerializeField] InputField nameField;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary> 名前入力 </summary>
    public void InputName()
    {
        PlayerStatus playerStatus = new PlayerStatus();
        playerStatus.SetPlayerName(nameField.text);
        GameStatus.lockMenber.statusSeter = playerStatus;
        // シーン変移
    }
}
