using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    bool inputFlg = false; // 入力フラグ
    // プレイヤー情報
    //PlayerStatus playerStatus;
    // 名前入力欄
    [SerializeField] InputField nameField;
    // 移行先
    [SerializeField] SceneObject nextScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary> 名前入力 </summary>
    public void Matching()
    {
        if (inputFlg || nameField.text == null) return; // 1回だけ処理
        PlayerStatus playerStatus = new PlayerStatus();
        // 名前情報を設定
        playerStatus.SetPlayerName(nameField.text);
        GameStatus.lockMenber.statusSeter = playerStatus;
        inputFlg = true;
        // シーン変移
        //SceneManager
    }
}
