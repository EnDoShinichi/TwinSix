using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private List<PlayerStatus> rankList;
    // Start is called before the first frame update
    void Start()
    {
        // rankList = new List<PlayerStatus>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResultDestroy()
    {
        //for (int i = 0; i < rankList.Count;i++)
        //{
        //    Destroy(rankList[i].gameObject);
        //}
        Destroy(gameObject);
    }

    public List<PlayerStatus> setRank
    {
        set
        {
            Debug.Log("setRank");
            if (rankList == null) rankList = new List<PlayerStatus>();
            if (rankList.Count < 0)
            {
                for (int i = 0; i < GameStatus.MAX_PLAYER_NUMBER; i++)
                {
                    rankList.Add(GameStatus.lockMenber.PlayerStatusGeter(i));
                }
            }

            rankList = value;
        }
    }

    public List<PlayerStatus> getRunk
    {
        get => rankList;
    }
}
