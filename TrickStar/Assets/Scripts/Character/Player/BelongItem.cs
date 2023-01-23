using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongItem : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private List<BelongItemPanel> belongItemPanelS = new List<BelongItemPanel>();
    [SerializeField]
    private PlayerController playerCS;

    [Header("Data")]
    [SerializeField]
    private ItemListData ItemLD;

    private int Mselected;          //�I�𒆂�item
    private bool Mdelete;           //�폜���[�h�̃t���O

    //�F�֘A
    private readonly Color NormalC = new Color(1.0f, 1.0f, 1.0f, 1.0f);   //�ʏ�̐F
    private readonly Color UseC = new Color(0.0f, 1.0f, 0.0f, 1.0f);      //�g�p���̐F
    private readonly Color CantUseC = new Color(1.0f, 0.0f, 0.0f, 1.0f);  //�g�p�ł��Ȃ����̐F
    private readonly Color NoneC = new Color(1.0f, 1.0f, 1.0f, 0.0f);     //���������ĂȂ����̐F

    public BelongItem()
    {
        Mdelete = false;
    }

    private void KeyController()//�L�[����̃��]�b�g
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveSelect(0);
            UseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveSelect(1);
            UseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MoveSelect(2);
            UseItem();
        }

        //�I������Ƃ�
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    UseItem();
        //}
    }   

    private void MoveSelect(int v)  //�I�������������������]�b�g
    {
        Mselected = v;
    }   

    private void UseItem()   //�I�����Ă�A�C�e�����g�p����
    {
        playerCS.UseItem(Mselected);

        ChangeColor();
    }   

    public void StartBelongItem()   //�ŏ��ɂ��鏈��
    {
        for (int i = 0; i <= Const.MAX_ITEMS - 1; i++)  //�A�C�e���p�l���̐������J��Ԃ�
        {
            belongItemPanelS[i].StartPanel();
            belongItemPanelS[i].ChangeColor(NoneC);
        }
    }

    //�C���x���g���̃A�C�e���̃C���[�W��`�����]�b�g
    public void DrawImages()
    {
        int index = 0;
        foreach(BelongItemData bData in playerCS.GetItems())
        {
            //���������A�C�e�����Ȃ��Ȃ珈���𒆒f����
            if (bData.MItemID == Const.NO_ITEM) { 
                continue;
                index++;
            }

            belongItemPanelS[index].ChangeImage(ItemLD.ItemDataList[bData.MItemID].Image);
            belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
            index++;
        }
    }   
    //�C���x���g���̃A�C�e���̐F�Ƒ傫����ς��郁�]�b�g
    public void ChangeColor()
    {
        int index = 0;  //���[�v��
        foreach (BelongItemData bData in playerCS.GetItems())
        {
            //�A�C�e���������Ă��Ȃ��Ƃ�
            if (bData.MItemID == Const.NO_ITEM)
            {
                belongItemPanelS[index].ChangeColor(NoneC);
                index++;
                continue;
            }
            //�A�C�e���̑ϋv�n���Ȃ��Ƃ�
            else if ((bData.MEndurance == 0))
            {
                belongItemPanelS[index].ChangeColor(CantUseC);
                belongItemPanelS[index].UnChoose();
                belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
                index++;
                continue;
            }
            
            //�g�p���̎�
            if (bData.MActive)
            {
                belongItemPanelS[index].ChangeColor(UseC);
                belongItemPanelS[index].Choose();
                belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
            }
            //�g�p������Ȃ��Ƃ�
            else
            {
                belongItemPanelS[index].ChangeColor(NormalC);
                belongItemPanelS[index].UnChoose();
                belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
            }
            index++;
        }
    }

    public void OpenItems() //�A�C�e�������J�����]�b�g
    {
        DrawImages();
    }
    public void CloseItems() //�A�C�e��������郁�]�b�g
    {
        for(int i = 0; i < Const.MAX_ITEMS; i++)
        {
            //belongItemPanelS[i].UnChoose();
        }
    }

    public void UpdateBelongItem()//�C���x���g�����J���Ă���Ƃ��Ɏ��s���郁�]�b�g
    {
        KeyController();
    }   
}
