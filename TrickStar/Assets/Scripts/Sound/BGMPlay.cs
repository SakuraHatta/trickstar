using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour
{
    AudioSource audio;//音楽を再生させるためにAudioSourceコンポーネントのPlayメソッドを使うために定義
    
    void Start()
    {
        audio = GetComponent<AudioSource>();    //AudioSourceコンポーネントを認識
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    // ここに耳栓のbool変数を使う
        audio.mute = !audio.mute;
    }
}
