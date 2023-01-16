using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    //Referense
    [Header("Scripts")]
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private PlayerController playerCS;

    [Space(10)]
    [Header("Data")]
    [SerializeField]
    private List<ItemCard> cardSList = new List<ItemCard>();
    [SerializeField]
    private ItemListData ItemLD;

    private readonly int CARD_NUMBER;  //�V���b�v�̃J�[�h�̐�
    private int Mselected;  //�I�𒆂̃J�[�h��index

    public ShopScript() //ShopScript�̃R���X�g���N�^
    {
        CARD_NUMBER = 3 - 1;
        Mselected = 0;
    }   

    //private void Test1(int index)    //�����̃A�C�e���̃X�e�[�^�X��\������
    //{
    //    Debug.Log("�A�C�e��ID[" + ItemLD.ItemDataList[index].id + "]�̖��O��["
    //        + ItemLD.ItemDataList[index].name + "]�ł��B"
    //        );

    //    Debug.Log("�l�i��[" + ItemLD.ItemDataList[index].price + "]�ł��B");
    //    Debug.Log("������ : " + ItemLD.ItemDataList[index].info);
    //}

    private void BuyItem()  //�A�C�e���𔃂�����
    {
        playerCS.AddItem(0);
    }

    private void ChangeSelect(int change) //Mselected�������̒l�������炷����
    {
        cardSList[Mselected].UnChooseCard();    //���ɑI������Ă���J�[�h���ɖ߂�

        Mselected += change;
        if (Mselected > CARD_NUMBER)   //Mselected���J�[�h�̖����𒴂��Ă�Ƃ�
        {
            Mselected = 0;  //�I���J�[�h��0�ɂ���
        }
        else if (Mselected < 0) //Eselected��0��艺�̎�
        {
            Mselected = CARD_NUMBER;   //Eselected���ő�l�ɂ���;
        }

        cardSList[Mselected].ChooseCard();  //�V�����I�����ꂽ�J�[�h�𓮂���
    }
    private void KeyController()   //�J�[�h��I�ԏ���
    {
        //���ɑI�����ړ�����Ƃ�
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSelect(-1);
        }
        //�E�ɑI�����ړ�����Ƃ�
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeSelect(1);
        }
        //�I������Ƃ�
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuyItem();
        }
    }

    public void OpenShop()//�X���J�����]�b�g
    {
        Mselected = 0;
        cardSList[Mselected].ChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSList�̗v�f�������J��Ԃ�
        {
            cardS.DrawCard(ItemLD.ItemDataList[0]); //�J�[�h�Ƀf�[�^���X�g�̏�����������
        }
    }
    public void ExitShop()//�X��߂郁�]�b�g
    {
        cardSList[Mselected].UnChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSList�̗v�f�������J��Ԃ�
        {
            cardS.HideCard(); //�J�[�h���B��
        }
    }

    public void UpdateShop()//�V���b�v���Ȃ珈��������֐�
    {
        KeyController();
    }
}
