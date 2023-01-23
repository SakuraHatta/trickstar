using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    //Referense
    [Header("Scripts")]
    [SerializeField]
    private BelongItem belongItemS; //belongitem��Script
    [SerializeField]
    private PlayerController playerCS;  //PlayerController��Script

    [Space(10)]
    [Header("Lists")]
    [SerializeField]
    private List<ItemCard> cardSList = new List<ItemCard>();    //�A�C�e���J�[�h�̃��X�g
    [SerializeField]
    private List<ShopObjectScript> ShopObjectList = new List<ShopObjectScript>();   //�V���b�v�I�u�W�F�N�g�̃��X�g

    private int[] SellItemID = new int[Const.CARD_NUMBER];

    [Space(10)]
    [Header("Data")]
    [SerializeField]
    private ItemListData ItemLD;    //ItemListData��DataList

    private int Mselected;  //�I�𒆂̃J�[�h��index

    public ShopScript() //ShopScript�̃R���X�g���N�^
    {
        Mselected = 0;
    }   

    //�A�C�e���𔃂�����
    private void BuyItem()  
    {
        //����ȏ�A�C�e�������ĂȂ��Ƃ��͏����𒆒f����
        //(������ : �A�C�e��ID, ������ : �A�C�e���̑ϋv�n)
        if (!(playerCS.AddItem(SellItemID[Mselected],
            ItemLD.ItemDataList[SellItemID[Mselected]].Endurance))) {
        #if CHECK
            Debug.Log("����ȏ�A�C�e���͎��ĂȂ�!");
        #endif 
            return;
        }

        belongItemS.OpenItems();    //�A�C�e���̃C���[�W���X�V����
        belongItemS.ChangeColor();  //�A�C�e�����̐F��ς���

        #if CHECK
                Debug.Log(ItemLD.ItemDataList[SellItemID[Mselected]].Name + "���w������");
        #endif
    }
    //Mselected�������̒l�������炷����
    private void ChangeSelect(int change) 
    {
        cardSList[Mselected].UnChooseCard();    //���ɑI������Ă���J�[�h���ɖ߂�

        Mselected += change;
        if (Mselected > Const.CARD_NUMBER - 1)   //Mselected���J�[�h�̖����𒴂��Ă�Ƃ�
        {
            Mselected = 0;  //�I���J�[�h��0�ɂ���
        }
        else if (Mselected < 0) //Eselected��0��艺�̎�
        {
            Mselected = Const.CARD_NUMBER - 1;   //Eselected���ő�l�ɂ���;
        }

        cardSList[Mselected].ChooseCard();  //�V�����I�����ꂽ�J�[�h�𓮂���
    }
    //�J�[�h��I�ԏ���
    private void KeyController()   
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

    //�ŏ��̏���
    public void StartShop()
    {
        //�}�b�v�ɂ���V���b�v�̐������J��Ԃ�
        foreach(ShopObjectScript shop in ShopObjectList)
        {
            shop.StartShopObject(); //shopObject�̍ŏ��̏��������s����
        }
    }

    //�߂��ɓX�����邩
    public bool CheckNearShop()
    {
        foreach(ShopObjectScript shop in ShopObjectList)
        {
            if (shop.CheckDistance(playerCS.GetPosition()))
            {
                SetSellItemID(shop);
                return true;
            }
        }
        return false;
    }
    //����A�C�e����ID��ύX����
    public void SetSellItemID(ShopObjectScript shopObj)
    {
        int index = 0;
        foreach(int sellId in shopObj.GetIDArray())
        {
            SellItemID[index] = sellId;
            index++;
        }
    }
    //�X���J�����]�b�g
    public void OpenShop()
    {
        int index = 0;
        Mselected = 0;
        cardSList[Mselected].ChooseCard();

        foreach (ItemCard cardS in cardSList)   //cardSList�̗v�f�������J��Ԃ�
        {
            cardS.DrawCard(ItemLD.ItemDataList[SellItemID[index]]); //�J�[�h�Ƀf�[�^���X�g�̏�����������
            index++;
        }

    }
    //�X��߂郁�]�b�g
    public void ExitShop()
    {
        cardSList[Mselected].UnChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSList�̗v�f�������J��Ԃ�
        {
            cardS.HideCard(); //�J�[�h���B��
        }
    }
    //�V���b�v���Ȃ珈��������֐�
    public void UpdateShop()
    {
        KeyController();
    }
}
