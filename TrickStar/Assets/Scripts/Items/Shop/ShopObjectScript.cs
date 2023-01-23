using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopObjectScript : MonoBehaviour
{
    [Tooltip("���̓X�ŕK�������Ă���A�C�e����ID")]
    [SerializeField]
    private int SetItem;    //���̓X�ŕK�������Ă���A�C�e��ID
    private int[] itemId = new int[Const.CARD_NUMBER];     //���̓X�Ŕ����Ă���A�C�e����ID��List

    private Vector2 thisPos;    //���̃V���b�v�̈ʒu

    private int GetRandomId(int max) { return Random.Range(0, max); }

    //��������
    public void StartShopObject()
    {
        thisPos = this.transform.position;  //�V���b�v�̈ʒu�����߂�

        while (true)
        {
            for (int i = 0; i < Const.CARD_NUMBER; i++)   //�J�[�h�̐������J��Ԃ�
            {
                itemId[i] = GetRandomId(Const.TYPE_ITEMS);  //�A�C�e����ID�������_���Ɍ��߂�
            }
            //SetItem�̃A�C�e��ID�����邩��T��
            var result = itemId.Any(e => e == SetItem); 

            if (result) { break; }  //�������烋�[�v���I��/����������܂�ID�����߂�
        }
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
