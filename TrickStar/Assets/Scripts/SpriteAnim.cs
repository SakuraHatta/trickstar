using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Spriteを使用するため記述

public class SpriteAnim : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] AnimSprites;   //アニメーションする画像
    private SpriteRenderer spriteR;

    [Header("Parameta")]
    [SerializeField]
    private float changeRate;     //絵を入れ替える速度
    private int maxSprites;        //絵の枚数
    private int index;             //表示する絵のindex

    private float time;

    //初期処理
    private void Start()
    {
        spriteR = this.GetComponent<SpriteRenderer>();
        maxSprites = AnimSprites.Length;    //配列の要素数を絵の枚数にセットする
        time = 0.0f;

        ChangeSprite();
    }

    //スプライトを変更するスクリプト
    private void ChangeSprite()
    {
        //もしindexが要素数以上なら0に戻す
        if (index >= maxSprites)
        {
            index = 0;
        }
        spriteR.sprite = AnimSprites[index]; 
    }

    //基本処理
    private void Update()
    {
        time += Time.deltaTime;
        if (time > changeRate)
        {
            time = 0.0f;
            index++;
            ChangeSprite();
        }
    }
}
