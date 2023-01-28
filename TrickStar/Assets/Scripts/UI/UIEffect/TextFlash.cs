using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour
{
    [SerializeField] private Text text;
    
    private float speed = 4.0f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.color = getAlphaColor(text.color);
    }

    private Color getAlphaColor(Color color){
        time += Time.deltaTime * speed;
        color.a = Mathf.Sin(time);

        return color;
    }
}
