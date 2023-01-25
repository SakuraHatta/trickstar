using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    private Vector2 MstartPos;  //�n�܂�ꏊ
    private Vector2 MendPos;    //�ړ��̏I���ꏊ

    private bool Mactive;    //�N�����Ă��邩�̃t���O
    private bool Mtween;     //�i�s�����t���O
    private float Mtime;
    private float Mspeed;    //�ړ������鎞��

    //�p�����[�^�[�����߂�
    public void SetParameta(bool act, bool t, float s)
    {
        Mactive = act;
        Mtween = t;
        Mspeed = s;
    }
    //���߂̈ʒu�ƏI���̈ʒu�����߂�
    public void SetPosition(Vector2 sp, Vector2 ep)
    {
        MstartPos = sp;
        MendPos = ep;
    }

    //�ړ����J�n������
    public void DoTween()
    {
        //�����܂��N�����Ȃ�
        if (Mactive) { 
            Mtween = !Mtween;   //�i�s�������t�ɂ��ď����𒆒f
            return;
        }

        Mactive = true;     
    }

    //�ړ����I����������m�F����
    public bool CheckDoneTween()
    {
        //���Ԃ��ŏ��l���ő�l�̎�
        if (Mtime == 0.0f || Mtime == Const.MAX_TWEEN)
        {
            return true;
        }

        return false;
    }

    //���Ԃ�i�߂�֐�
    public void Update()
    {
        if (!Mactive) { return; }    //�N�����ĂȂ��Ȃ珈���𒆒f
        
        //�i�s����Ƃ�
        if (Mtween)
        {
            //�������Ԃ��ő厞�Ԃ𒴂��Ă�����
            if (Mtime < Const.MAX_TWEEN)
            {
                Mactive = false;    //�N����off�ɂ���
                Mtween = !Mtween;   //�i�s�������t�ɂ���
                Mtime = Const.MAX_TWEEN;    //���Ԃ��ő厞�Ԃɍ��킹��
                return;
            }

            Mtime += Time.deltaTime;    //���Ԃ𑫂��Ă���
        }
        //�߂�Ƃ�
        else
        {
            if (Mtime > 0.0f)
            {
                Mactive = false;
                Mtween = !Mtween;
                Mtime = 0.0f;
                return;
            }

            Mtime -= Time.deltaTime;
        }
    }
}
