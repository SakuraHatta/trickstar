using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject shopSetObject;  //�V���b�v��UI�������Ă���I�u�W�F�N�g

    [SerializeField]
    private RectTransform[] rectTransforms;   //�g�p����RectTransfrom�͔̂z��
    private Vector2[] basePositions;        //���̈ʒu
    private Vector2[] goalPositions;        //�ړ���

    private enum transformE
    {
        InfoTransform,  //���p�l����transform
        ItemsetTransform    //�A�C�e���Z�b�g��transform
    }   //Rtransform�z��ɂ���A�v�f�̂��ꂼ��̖��O

    [SerializeField]
    private Text[] Texts;  //�g�p����e�L�X�g�̔z��
    private enum textE
    {
        NameT,  //�A�C�e���̖��O�������e�L�X�g
        InfoT   //�A�C�e���̏���`���e�L�X�g
    }   //�e�L�X�g�z��ɂ���A�v�f�̂��ꂼ�ꖼ�O

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

    private int[] SellItemID = new int[Const.CARD_NUMBER];  //�����Ă���A�C�e����id�z��

    [Space(10)]
    [Header("Data")]
    [SerializeField]
    private ItemListData ItemLD;    //ItemListData��DataList

    [Space(10)]
    [Header("Animation")]
    [SerializeField]
    private AnimationCurve curve;

    private int Mselected;  //�I�𒆂̃J�[�h��index
    private bool animation; //�A�j���[�V���������Ă��邩�ǂ���
    private float animTime; //�A�j���[�V�����̎��Ԍo��

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

        ChangeText(textE.NameT, ItemLD.ItemDataList[SellItemID[Mselected]].Name);
        ChangeText(textE.InfoT, ItemLD.ItemDataList[SellItemID[Mselected]].Info);
    }
    //�J�[�h��I�ԏ���
    private void KeyController()   
    {
        //���ɑI�����ړ�����Ƃ�
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelect(-1);
        }
        //�E�ɑI�����ړ�����Ƃ�
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelect(1);
        }
        //�I������Ƃ�
        else if (Input.GetKeyDown(KeyCode.E))
        {
            BuyItem();
        }
    }

    //�ŏ��̏���
    public void StartShop()
    {
        basePositions = new Vector2[rectTransforms.Length];
        goalPositions = new Vector2[rectTransforms.Length];

        //rectTransform�z��̗v�f���̕������J��Ԃ�
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            //���ꂼ��̌��̈ʒu���J�n�n�_�ɐݒ肷��
            basePositions[i] = rectTransforms[i].anchoredPosition;
        }
        //���ꂼ��̈ړ���̍��W�����߂�
        goalPositions[(int)transformE.InfoTransform] =
            basePositions[(int)transformE.InfoTransform] + new Vector2(-280.0f, 0.0f);
        goalPositions[(int)transformE.ItemsetTransform] = 
            basePositions[(int)transformE.ItemsetTransform] + new Vector2(-140.0f, 0.0f);

        //�}�b�v�ɂ���V���b�v�̐������J��Ԃ�
        foreach (ShopObjectScript shop in ShopObjectList)
        {
            shop.StartShopObject(); //shopObject�̍ŏ��̏��������s����
        }
        //�V���b�v�ɔ����Ă��镪�̐������J��Ԃ�
        foreach(ItemCard items in cardSList)
        {
            items.StartCard();
        }
    }

    //�e�L�X�g��ύX����X�N���v�g
    private void ChangeText(textE type, string s)
    {
        Texts[(int)type].text = s;
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
        shopSetObject.SetActive(true);

        animation = true;
        animTime = 0.0f;

        int index = 0;
        foreach (ItemCard cardS in cardSList)   //cardSList�̗v�f�������J��Ԃ�
        {
            cardS.DrawCard(ItemLD.ItemDataList[SellItemID[index]]); //�J�[�h�Ƀf�[�^���X�g�̏�����������
            index++;
        }
    }
    //�X��߂郁�]�b�g
    public void ExitShop()
    {
        shopSetObject.SetActive(false);

        cardSList[Mselected].UnChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSList�̗v�f�������J��Ԃ�
        {
            cardS.HideCard(); //�J�[�h���B��
        }

        //�e�L�X�g�̒��g����ɂ���
        ChangeText(textE.NameT, null);
        ChangeText(textE.InfoT, null);
    }

    //���ۂɃA�j���[�V�����o�ߎ��Ԃɍ��킹�Ĉړ������郁�]�b�g
    private void MoveObjects()
    {
        for (int i = 0; i < 2; i++)
        {
            rectTransforms[i].anchoredPosition = Vector2.Lerp(basePositions[i], goalPositions[i], curve.Evaluate(animTime));
        }
    }
    //�A�j���[�V���������Ă��邩���m�F���郁�]�b�g
    private bool PlayAnim()
    {
        animTime += Time.deltaTime;
        MoveObjects();

        if (animTime > 1.0f || animTime < 0.0f)
        {
            return false;
        }
        return true;
    }

    //�V���b�v���Ȃ珈��������֐�
    public void UpdateShop()
    {
        if (animation)
        {
            if (!PlayAnim())
            {
                Mselected = 0;
                ChangeSelect(0);
                cardSList[Mselected].ChooseCard();

                animation = false;
            }
            return;
        }

        KeyController();
    }
}
