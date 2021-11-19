using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : MonoBehaviour,IPhase
{
    public event Action NextPhase;
    private PlayerStatus playerStatus;

    public void PhaseEnd()
    {
        
    }

    public void PhaseStart(PlayerStatus turnObject)
    {

    }

    public void PhaseUpdate()
    {
        //if(playerStatus.SetDoubtDice())
        {

        }
    }
}
