using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    private Vector2 MstartPos;  //始まる場所
    private Vector2 MendPos;    //移動の終わる場所

    private bool Mactive;    //起動しているかのフラグ
    private bool Mtween;     //進行方向フラグ
    private float Mtime;
    private float Mspeed;    //移動しきる時間

    //パラメーターを決める
    public void SetParameta(bool act, bool t, float s)
    {
        Mactive = act;
        Mtween = t;
        Mspeed = s;
    }
    //初めの位置と終わるの位置を決める
    public void SetPosition(Vector2 sp, Vector2 ep)
    {
        MstartPos = sp;
        MendPos = ep;
    }

    //移動を開始させる
    public void DoTween()
    {
        //もしまだ起動中なら
        if (Mactive) { 
            Mtween = !Mtween;   //進行方向を逆にして処理を中断
            return;
        }

        Mactive = true;     
    }

    //移動し終わったかを確認する
    public bool CheckDoneTween()
    {
        //時間が最小値か最大値の時
        if (Mtime == 0.0f || Mtime == Const.MAX_TWEEN)
        {
            return true;
        }

        return false;
    }

    //時間を進める関数
    public void Update()
    {
        if (!Mactive) { return; }    //起動してないなら処理を中断
        
        //進行するとき
        if (Mtween)
        {
            //もし時間が最大時間を超えていたら
            if (Mtime < Const.MAX_TWEEN)
            {
                Mactive = false;    //起動をoffにする
                Mtween = !Mtween;   //進行方向を逆にする
                Mtime = Const.MAX_TWEEN;    //時間を最大時間に合わせる
                return;
            }

            Mtime += Time.deltaTime;    //時間を足していく
        }
        //戻るとき
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
