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
    // ������
    public static void ResetProperty(ref int dice)
    {
        dice = 0;
    }
    // ���̃^�[���ֈڍs
    public static void TurnEnd(int playerNo, int playerSum, ref int turn)
    {
        // ���݂̃v���C���[���Ō�̃����o�[�Ȃ玟�̃^�[����
        if (playerNo == playerSum) turn++;

    }
}
