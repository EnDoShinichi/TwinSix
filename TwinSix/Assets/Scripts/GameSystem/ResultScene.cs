using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    ResultManager result;
    [SerializeField] private string sceneName;
    [SerializeField] private Text topPlayerText;
    [SerializeField] private Text playersText;
    // Start is called before the first frame update
    void Start()
    {
        result = GameObject.Find("ResultData").GetComponent<ResultManager>();

        PlayerStatus[] statuses = new PlayerStatus[GameStatus.MAX_PLAYER_NUMBER];

        for (int i = 0; i < GameStatus.MAX_PLAYER_NUMBER;i++)
        {
            statuses[i] = GameObject.Find("Avatar" + i.ToString() + "(Clone)").GetComponent<PlayerStatus>();
        }

        var states = statuses.OrderByDescending(statuses=>statuses.money);
        statuses = states.ToArray();

        topPlayerText.text = statuses[0].playerName + ": èäéùã‡" + statuses[0].money;

        for (int i = 1; i < statuses.Length;i++)
        {
            playersText.text += statuses[i].playerName + ": èäéùã‡" + statuses[i].money + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameStatus.lockMenber.GameInit();
            result.ResultDestroy();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(sceneName);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
