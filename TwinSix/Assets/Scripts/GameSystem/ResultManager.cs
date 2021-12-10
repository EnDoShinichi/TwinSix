using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private List<PlayerStatus> rankList = new List<PlayerStatus>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<PlayerStatus> setRank
    {
        set=>rankList = value;
    }

    public List<PlayerStatus> getRunk
    {
        get => rankList;
    }
}
