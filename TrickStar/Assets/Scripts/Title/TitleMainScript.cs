using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMainScript : MonoBehaviour
{
    [Header("TextControllers")]
    [SerializeField]
    private List<UIController> TextCArray = new List<UIController>();//タイトルのテキストたち
    //タイトル画面の状態
    private uint titleState = Const.DEFAULT_T;  //通常状態をする
    //色
    private readonly Color NULL_COLOR = new Color(1.0f, 1.0f, 1.0f, 0.0f);  //透明色

    private float timeValue = 0.0f;

    //enterキーをおした時の処理
    private void EnterEvent()
    {
        if (Const.FADE_T == (titleState & Const.FADE_T)) { return; }
        TextCArray[0].StartTween(TextCArray[0].GetPosition() + new Vector2(0.0f, 3000.0f));
        titleState |= Const.FADE_T;
    }
    //キーの状態判定
    private void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterEvent();
        }
    }

    //初期処理
    private void Start()
    {
        foreach(UIController texts in TextCArray)
        {
            texts.StartText();
        }
    }
    //基本処理
    private void Update()
    {
        KeyEvent();

        foreach (UIController texts in TextCArray)
        {
            texts.UpdateText();
        }

        if (Const.FADE_T == (titleState & Const.FADE_T))
        {
            timeValue += Time.deltaTime;
            if (timeValue > 2.0f)
            {
                SceneManager.LoadScene("MapScene");
            }
        }
    }
}
