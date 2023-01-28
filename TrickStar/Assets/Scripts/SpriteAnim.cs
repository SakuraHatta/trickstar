using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Sprite���g�p���邽�ߋL�q

public class SpriteAnim : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] AnimSprites;   //�A�j���[�V��������摜
    private SpriteRenderer spriteR;

    [Header("Parameta")]
    [SerializeField]
    private float changeRate;     //�G�����ւ��鑬�x
    private int maxSprites;        //�G�̖���
    private int index;             //�\������G��index

    private float time;

    //��������
    private void Start()
    {
        spriteR = this.GetComponent<SpriteRenderer>();
        maxSprites = AnimSprites.Length;    //�z��̗v�f�����G�̖����ɃZ�b�g����
        time = 0.0f;

        ChangeSprite();
    }

    //�X�v���C�g��ύX����X�N���v�g
    private void ChangeSprite()
    {
        //����index���v�f���ȏ�Ȃ�0�ɖ߂�
        if (index >= maxSprites)
        {
            index = 0;
        }
        spriteR.sprite = AnimSprites[index]; 
    }

    //��{����
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
