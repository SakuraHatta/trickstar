using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //パラメーター
    protected uint textState;   //テキストの状態
    protected float timeValue;  //時間変数
    private float phase;        //上下移動の法則

    [Header("Parameta")]
    [SerializeField]
    protected float movelarge;    //移動する大きさ
    [SerializeField]
    protected float movetime;     //移動スピード

    private Vector2 endPos;     //移動のゴール地点

    //Component
    private RectTransform thisTransform;    //このテキストのrextTransform

    private Text thisText;  //自分自身のテキスト
    private Image thisImage;

    //元のデータ
    private Vector2 basePos;    //テキストの原点位置
    private Color baseColor;

    public virtual void UpdateText() {;}
    public virtual void Selected() {;}

    //コンストラクターの代行
    public void StartText()
    {
        textState = Const.DEFAULT_TEXT;
        movetime = 1.0f / movetime;

        thisTransform = this.GetComponent<RectTransform>();
        thisText = this.GetComponent<Text>();

        basePos = thisTransform.anchoredPosition;
    }
    //テキストを上下に移動させるメゾット
    public void UpDownText()
    {
        phase = Mathf.Sin(Mathf.PI * 2 * timeValue * movetime);
        thisTransform.anchoredPosition = new Vector2(basePos.x, basePos.y + (phase * movelarge));

        if (phase > 1.0f)
        {
            timeValue = 0.0f;
        }
    }
    //テキストを選択した時のメゾット
    public void SelectText()
    {

    }
    //移動のそれぞれの位置を決めて移動状態にする
    public void StartTween(Vector2 goal) 
    {
        timeValue = 0.0f;
        endPos = goal;
        textState |= Const.TWEEN_TEXT;
    }
    //テキストを移動させるメゾット
    public void TweenText()
    {
        thisTransform.anchoredPosition = Vector2.Lerp(basePos, endPos, timeValue);
        if (timeValue > Const.MAX_TWEEN) 
        {
            textState &= Const.TWEEN_TEXT;  //移動フラグを消す
            basePos = endPos;   //原点位置を移動先の位置にする
        }
    }
    //このテキストの位置を所得する
    public Vector2 GetPosition()
    {
        return basePos;
    } 
}
