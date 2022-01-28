using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


interface IPhase // フェーズの基本的な関数
{
    /// <summary>フェーズ終了時に呼ぶイベント</summary>
    event Action NextPhase; // フェーズ終了時に呼ぶイベント
    /// <summary>フェーズの開始時に呼び出される処理</summary>
    /// <param name="turnObject">このターンのプレイヤーステータス</param>
    void PhaseStart(PlayerStatus turnObject); // フェーズの開始時に呼び出される処理
    /// <summary>フェーズ中常に呼び出される処理</summary>
    void PhaseUpdate(); // フェーズ中常に呼び出される処理
    /// <summary>フェーズの終了時に呼び出される処理</summary>
    void PhaseEnd(); // フェーズの終了時に呼び出される処理
    /// <summary> </summary>
    void PhaseCompleatesynchronize(int number);
}