using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //�p�����[�^�[
    protected uint textState;   //�e�L�X�g�̏��
    protected float timeValue;  //���ԕϐ�
    private float phase;        //�㉺�ړ��̖@��

    [Header("Parameta")]
    [SerializeField]
    protected float movelarge;    //�ړ�����傫��
    [SerializeField]
    protected float movetime;     //�ړ��X�s�[�h

    private Vector2 endPos;     //�ړ��̃S�[���n�_

    //Component
    private RectTransform thisTransform;    //���̃e�L�X�g��rextTransform

    private Text thisText;  //�������g�̃e�L�X�g
    private Image thisImage;

    //���̃f�[�^
    private Vector2 basePos;    //�e�L�X�g�̌��_�ʒu
    private Color baseColor;

    public virtual void UpdateText() {;}
    public virtual void Selected() {;}

    //�R���X�g���N�^�[�̑�s
    public void StartText()
    {
        textState = Const.DEFAULT_TEXT;
        movetime = 1.0f / movetime;

        thisTransform = this.GetComponent<RectTransform>();
        thisText = this.GetComponent<Text>();

        basePos = thisTransform.anchoredPosition;
    }
    //�e�L�X�g���㉺�Ɉړ������郁�]�b�g
    public void UpDownText()
    {
        phase = Mathf.Sin(Mathf.PI * 2 * timeValue * movetime);
        thisTransform.anchoredPosition = new Vector2(basePos.x, basePos.y + (phase * movelarge));

        if (phase > 1.0f)
        {
            timeValue = 0.0f;
        }
    }
    //�e�L�X�g��I���������̃��]�b�g
    public void SelectText()
    {

    }
    //�ړ��̂��ꂼ��̈ʒu�����߂Ĉړ���Ԃɂ���
    public void StartTween(Vector2 goal) 
    {
        timeValue = 0.0f;
        endPos = goal;
        textState |= Const.TWEEN_TEXT;
    }
    //�e�L�X�g���ړ������郁�]�b�g
    public void TweenText()
    {
        thisTransform.anchoredPosition = Vector2.Lerp(basePos, endPos, timeValue);
        if (timeValue > Const.MAX_TWEEN) 
        {
            textState &= Const.TWEEN_TEXT;  //�ړ��t���O������
            basePos = endPos;   //���_�ʒu���ړ���̈ʒu�ɂ���
        }
    }
    //���̃e�L�X�g�̈ʒu����������
    public Vector2 GetPosition()
    {
        return basePos;
    } 
}
