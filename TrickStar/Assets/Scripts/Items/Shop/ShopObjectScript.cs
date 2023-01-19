using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObjectScript : MonoBehaviour
{
    [Tooltip("���̓X�ŕK�������Ă���A�C�e����ID")]
    [SerializeField]
    private int SetItem;

    private int[] itemId = new int[Const.CARD_NUMBER];     //���̓X�Ŕ����Ă���A�C�e����ID��List

    private Vector2 thisPos;    //���̃V���b�v�̈ʒu

    private int GetRandomId(int max) { return Random.Range(0, max); }

    //��������
    public void StartShopObject()
    {
        thisPos = this.transform.position;  //�V���b�v�̈ʒu�����߂�

        for (int i = 0; i < Const.CARD_NUMBER; i++)   //�J�[�h�̐������J��Ԃ�
        {
            itemId[i] = GetRandomId(Const.TYPE_ITEMS);  //�A�C�e����ID�������_���Ɍ��߂�
        }

        itemId[GetRandomId(Const.CARD_NUMBER)] = SetItem; //�����_���̃J�[�h��K�������Ă���A�C�e��ID���Z�b�g����
    }

    //player���X�̋߂��ɂ��邩�m�F���郁�]�b�g
    public bool CheckDistance(Vector2 plrpos)
    {
        float distance = (thisPos - plrpos).magnitude;   //���̓X�̈ʒu�ƃv���C���[�̈ʒu
        //�������X�̑傫����菬�����Ƃ�
        if (distance < Const.ITEM_SCALE)
        {
            return true;
        }   
        return false;
    }

    //�����Ă���A�C�e����Id�̔z���Ԃ�
    public int[] GetIDArray() { return itemId; }
}
