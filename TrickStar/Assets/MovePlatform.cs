using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePoint;    //移動先の座標
    private int pointNumbers;   //移動する位置の数
    private int pointIndex;     //移動する場所の配列のインデックス

    [SerializeField]
    private float moveSpeed; //移動する速さ

    void Start()
    {
        pointIndex = 0;
        pointNumbers = movePoint.Length;
        this.transform.position = movePoint[0].position;
    }

    //目標地に向かって移動させるメゾット
    private void MoveTween()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, movePoint[pointIndex].position, moveSpeed * Time.deltaTime);
    }
    //プラットフォームが目的地の近くにいるかを確認するメゾット
    private void CheckDone()
    {
        float distance = (this.transform.position - movePoint[pointIndex].position).magnitude;

        if (distance < 0.01f)
        {
            pointIndex++;
            if (pointIndex == pointNumbers)
            {
                pointIndex = 0;
            }
        }
    }

    //プレイヤーが触れたとき
    private void OnCollisionEnter2D(Collision2D hit)
    {
        //当たったタグがプレイヤーなら
        if (hit.gameObject.tag == "Player") {
            if (this.transform.position.y < hit.transform.position.y - Const.PLAYER_HALF_SCALE)
            {
                hit.transform.SetParent(this.transform);    //そのオブジェクトの親をこのオブジェクトにする
            }
        }
    }
    //プレイヤーが離れたとき
    private void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.transform.SetParent(null); //そのオブジェクトの親をなしにする
        }
    }

    void Update()
    {
        CheckDone();
        MoveTween();
    }
}
