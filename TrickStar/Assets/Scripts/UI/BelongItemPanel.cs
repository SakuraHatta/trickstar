using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text,Image���g�p���邽�߂ɒǉ�


public class BelongItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;    //�A�C�e���̃C���[�W�摜

    public void ChangeImage(Sprite image)
    {
        itemImage.sprite = image;
    }   //�A�C�e���̃C���[�W��ύX�����郁�]�b�g
}
