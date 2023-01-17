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

    [SerializeField]
    private ItemListData ItemLD;

    private readonly int MAX_ITEMS; //���Ă�A�C�e���̐�
    private int Mitems;              //�����Ă���A�C�e����

    private int Mselected;          //�I�𒆂�item
    private bool Mdelete;           //�폜���[�h�̃t���O

    public BelongItem()
    {
        MAX_ITEMS = 3 - 1;  //���Ă�A�C�e���̍ő吔�����߂�
        Mselected = 0;
        Mdelete = false;
    }

    private void KeyController()//�L�[����̃��]�b�g
    {
        //���ɑI�����ړ�����Ƃ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelect(-1);
        }
        //�E�ɑI�����ړ�����Ƃ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelect(1);
        }
        //�I������Ƃ�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UseItem();
        }
    }   

    private void MoveSelect(int v)  //�I�������������������]�b�g
    {
        belongItemPanelS[Mselected].UnChoose();

        Mselected += v;

        if (Mselected > MAX_ITEMS)
            Mselected = 0;
        else if (Mselected < 0)
            Mselected = MAX_ITEMS;

        belongItemPanelS[Mselected].Choose();

        Debug.Log("�I���A�C�e�� : " + Mselected);
    }   

    public void StartBelongItem()   //�ŏ��ɂ��鏈��
    {
        for (int i = 0; i <= MAX_ITEMS; i++)  //�A�C�e���p�l���̐������J��Ԃ�
        {
            belongItemPanelS[i].StartPanel();
        }
    }

    public void DrawImages()//�C���x���g���̃A�C�e���̃C���[�W��`�����]�b�g
    {
        int index = 0;
        foreach(int id in playerCS.GetItems())
        {
            belongItemPanelS[index].ChangeImage(ItemLD.ItemDataList[id].image);
            index++;
        }
    }   

    public void UseItem()
    {
        playerCS.UseItem(Mselected);
    }   //�I�����Ă�A�C�e�����g�p����

    public void OpenItems() //�A�C�e�������J�����]�b�g
    {
        Mselected = 0;
        belongItemPanelS[Mselected].Choose();
        DrawImages();
    }
    public void CloseItems() //�A�C�e��������郁�]�b�g
    {
        for(int i = 0; i < MAX_ITEMS; i++)
        {
            belongItemPanelS[i].UnChoose();
        }
    }

    public void UpdateBelongItem()//�C���x���g�����J���Ă���Ƃ��Ɏ��s���郁�]�b�g
    {
        KeyController();
    }   
}
