using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject movePhase;
    [SerializeField] private GameObject testPlayer;
    private IPhase iPhase;

    void Start()
    {
        iPhase = movePhase.GetComponent<IPhase>();
    }

    void Update()
    {
        
    }
}
