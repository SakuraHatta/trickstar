using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    private RectTransform rectTransform;

    private Vector2 BASE_POSITION;  //元の位置

    [Header("Parameta")]
    //[SerializeField]
    private Vector2 SCALE;  //画像の大きさ
    [SerializeField]
    private Vector2 moveSpeed;    //移動する速さ

    private Vector2 movelarge;    //移動した距離

    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        BASE_POSITION = rectTransform.anchoredPosition;
        SCALE.x = Screen.width;
        SCALE.y = Screen.height;
    }

    private void FixedUpdate()
    {
        //移動速度に合わせて移動していく
        rectTransform.Translate(moveSpeed.x, moveSpeed.y, 0.0f);
        //xyそれぞれの移動した距離を測る
        movelarge.x += moveSpeed.x;
        movelarge.y += moveSpeed.y;

        //xの移動距離がサイズxより大きいとき
        if (movelarge.x * movelarge.x > SCALE.x * SCALE.x)
        {
            rectTransform.anchoredPosition = new Vector2(BASE_POSITION.x, rectTransform.anchoredPosition.y);
            movelarge.x = 0.0f;
        }
        //yの移動距離がサイズyより大きいとき
        else if (movelarge.y * movelarge.y > SCALE.y * SCALE.y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, BASE_POSITION.y);
            movelarge.y = 0.0f;
        }
    }
}
