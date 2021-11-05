using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePhase : MonoBehaviour,IPhase
{
    public event Action NextPhase;

    public void PhaseEnd()
    {
        throw new NotImplementedException();
    }

    public void PhaseStart(PlayerStatus turnObject)
    {
        throw new NotImplementedException();
    }

    public void PhaseUpdate()
    {
        throw new NotImplementedException();
    }
}
