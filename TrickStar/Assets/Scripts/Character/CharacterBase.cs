using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //�p�����[�^�[
    protected int maxhp;          //�ő�̗�
    protected int hp;             //�̗�
    protected int attack;         //�U����
    protected float jumppower;    //�W�����v��
    protected float speed;        //�ړ����x

    protected Vector2 rigid;      //������

    //��ԊǗ�
    protected int State = 0b00000; //(����A�U���A�W�����v�A�ړ��A�n��)

    //���]�b�g
    public void SetParameta(CharacterData charData)   //CharacterData�ɍ��킹�ăp�����[�^�[��ύX����
    {
        maxhp = charData.maxhp;
        attack = charData.attack;
        jumppower = charData.jumppower;
        speed = charData.speed;
    }

    public virtual void KeyController() {; }     //�L�[���Ƃ̃A�N�V����
    public virtual void Jump() {; }              //�W�����v���郁�]�b�g
    public virtual void Walk() {; }              //�ړ����郁�]�b�g
    public virtual void Attack() {; }            //�U�����郁�]�b�g
}