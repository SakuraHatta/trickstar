using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePoint;    //�ړ���̍��W
    private int pointNumbers;   //�ړ�����ʒu�̐�
    private int pointIndex;     //�ړ�����ꏊ�̔z��̃C���f�b�N�X

    [SerializeField]
    private float moveSpeed; //�ړ����鑬��

    void Start()
    {
        pointIndex = 0;
        pointNumbers = movePoint.Length;
        this.transform.position = movePoint[0].position;
    }

    //�ڕW�n�Ɍ������Ĉړ������郁�]�b�g
    private void MoveTween()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, movePoint[pointIndex].position, moveSpeed * Time.deltaTime);
    }
    //�v���b�g�t�H�[�����ړI�n�̋߂��ɂ��邩���m�F���郁�]�b�g
    private void CheckDone()
    {
        float distance = (this.transform.position - movePoint[pointIndex].position).magnitude;

        if (distance < 0.01f)
        {
            pointIndex++;
            if (pointIndex == pointNumbers)
            {
                pointIndex = 0;
            }
        }
    }

    //�v���C���[���G�ꂽ�Ƃ�
    private void OnCollisionEnter2D(Collision2D hit)
    {
        //���������^�O���v���C���[�Ȃ�
        if (hit.gameObject.tag == "Player") {
            if (this.transform.position.y < hit.transform.position.y - Const.PLAYER_HALF_SCALE)
            {
                hit.transform.SetParent(this.transform);    //���̃I�u�W�F�N�g�̐e�����̃I�u�W�F�N�g�ɂ���
            }
        }
    }
    //�v���C���[�����ꂽ�Ƃ�
    private void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.transform.SetParent(null); //���̃I�u�W�F�N�g�̐e���Ȃ��ɂ���
        }
    }

    void Update()
    {
        CheckDone();
        MoveTween();
    }
}
