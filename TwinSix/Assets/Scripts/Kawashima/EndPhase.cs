using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 初期化
    public static void ResetProperty(ref int dice)
    {
        dice = 0;
    }
    // 次のターンへ移行
    public static void TurnEnd(int playerNo, int playerSum, ref int turn)
    {
        // 現在のプレイヤーが最後のメンバーなら次のターンへ
        if (playerNo == playerSum) turn++;

    }
}
