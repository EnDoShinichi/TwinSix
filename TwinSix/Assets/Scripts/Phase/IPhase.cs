using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


interface IPhase // �t�F�[�Y�̊�{�I�Ȋ֐�
{
    /// <summary>�t�F�[�Y�I�����ɌĂԃC�x���g</summary>
    event Action NextPhase; // �t�F�[�Y�I�����ɌĂԃC�x���g
    /// <summary>�t�F�[�Y�̊J�n���ɌĂяo����鏈��</summary>
    /// <param name="turnObject">���̃^�[���̃v���C���[�X�e�[�^�X</param>
    void PhaseStart(PlayerStatus turnObject); // �t�F�[�Y�̊J�n���ɌĂяo����鏈��
    /// <summary>�t�F�[�Y����ɌĂяo����鏈��</summary>
    void PhaseUpdate(); // �t�F�[�Y����ɌĂяo����鏈��
    /// <summary>�t�F�[�Y�̏I�����ɌĂяo����鏈��</summary>
    void PhaseEnd(); // �t�F�[�Y�̏I�����ɌĂяo����鏈��
    /// <summary> </summary>
    void PhaseCompleatesynchronize(int number);
}