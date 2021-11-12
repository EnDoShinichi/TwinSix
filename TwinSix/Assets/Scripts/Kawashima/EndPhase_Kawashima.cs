using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase_Kawashima : MonoBehaviour, IPhase
{
    public event Action NextPhase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 初期化
    private void ResetProperty(ref int dice)
    {
        dice = 0;
    }
    // 次のターンへ移行
    private void TurnEnd(int playerNo, int playerSum, ref int turn)
    {
        // 現在のプレイヤーが最後のメンバーなら次のターンへ
        if (playerNo == playerSum) turn++;

    }
    void IPhase.PhaseStart(PlayerStatus turnObject)
    {

    }
    void IPhase.PhaseUpdate()
    {

    }
    void IPhase.PhaseEnd()
    {
        if (GameStatus.lockMenber.playingNumber >= GameStatus.MAX_PLAYER_NUMBER)
        {

        }
    }
}
