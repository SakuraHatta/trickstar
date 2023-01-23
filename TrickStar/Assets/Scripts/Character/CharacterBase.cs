using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基底クラス
public class CharacterBase : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    protected ItemBox itemsBoxS;  //すべてのアイテムが管理されているスクリプト
    [SerializeField]
    protected RelationTilemapScript tilemapS;    //タイルマップ関連のスクリプト

    //装備しているアイテムIDのList
    [SerializeField]
    protected List<BelongItemData> EquipmentItem = new List<BelongItemData>();
    protected int Mitems; //持っているアイテム数

    [Header("Parametas")]
    //パラメーター
    protected int maxhp;          //最大体力
    protected int hp;             //体力
    protected int power;         //攻撃力
    protected float jumppower;    //ジャンプ力
    protected float speed;        //移動速度

    public int Maxhp { get { return maxhp; } set { maxhp = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Power { get { return power; } set { power = value; } }
    public float Jumppower { get { return jumppower; } set { jumppower = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    protected int airjump;        //空中でジャンプできる回数
    protected int limitairjump;   //空中でジャンプできる回数

    public int Airjump { get { return airjump; } set { airjump = value; } }
    public int Limitairjump { get { return limitairjump; } set { limitairjump = value; } }

    protected Vector2 rigid;      //動く力

    //状態管理
    protected uint state;            //プレイヤーのステータス
    protected uint itemstate;        //アイテムのステータス

    public uint Itemstate { get { return itemstate; } set { itemstate = value; } }

    public CharacterBase()
    {
        state = 0b1001;
        itemstate = 0b0000;
        for (int i = 0; i < Const.MAX_ITEMS; i++)
        {
            EquipmentItem.Add(new BelongItemData());
        }
    }

    //メゾット
    public void SetParameta(CharacterData charData)   //CharacterDataに合わせてパラメーターを変更する
    {
        maxhp = charData.Maxhp;
        power = charData.Power;
        jumppower = charData.Jumppower;
        speed = charData.Speed;
    } 

    //アイテムを追加できるか確認してから追加するメゾット
    public bool AddItem(int id, int endurance)
    {
        //アイテムの数だけ繰り返す
        foreach(BelongItemData bData in EquipmentItem)
        {
            if (bData.MItemID == Const.NO_ITEM)
            {
                bData.MItemID = id;
                bData.MEndurance = endurance;
                return true;
            }
        }

        return false;
    }
    //持っているアイテムのidを確認するメゾット
    public List<BelongItemData> GetItems() { return EquipmentItem; }
    //持っているアイテムを全て使用するメゾット
    public void ActiveItems()
    {
        foreach(BelongItemData bData in EquipmentItem)
        {
            itemsBoxS.UseItems(this, bData);
        }
    }

    public virtual void CharacterUpdate() {;}

    public virtual void KeyController() {;}      //キーごとのアクション
    public virtual void Jump() {;}               //ジャンプするメゾット
    public virtual void SkyJump() {;}            //空中でジャンプする処理(ダブルジャンプ)
    public virtual void Walk(int direction) {;}  //移動するメゾット
    public virtual void Attack() {;}             //攻撃するメゾット
    public virtual void HitWall() {;}            //壁に当たった時の処理
    
    public Vector2 GetPosition() { return this.transform.position; }
    public Vector2 GetRigid() { return rigid; }  //rigidを所得するメゾット

    public uint GetItemState() { return itemstate; }    //キャラクターのアイテムステータスを所得する

    //キャラクターのACTIVEフラグを所得する
    public bool GetActive() {

        if (Const.ACTIVE == (state & Const.ACTIVE))
            return true;

        return false;    
    }
    //キャラクターのACTIVEフラグを変更する
    public void SetActive(bool b)
    {
        if (b && Const.ACTIVE != (state & Const.ACTIVE))
            state |= Const.ACTIVE;
        else if (Const.ACTIVE == (state & Const.ACTIVE))
            state &= ~Const.ACTIVE;
    }
}