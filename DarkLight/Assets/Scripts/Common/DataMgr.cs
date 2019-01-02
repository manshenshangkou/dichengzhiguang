using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataMgr
{
    private static DataMgr dm = null;

    private DataMgr()
    {
        TextAsset ta = Resources.Load("Item/Item_Data") as TextAsset;
        itemList = JsonConvert.DeserializeObject<List<Item>>(ta.text);
    }
    public static DataMgr GetInstance()
    {
        if (dm==null)
        {
            dm = new DataMgr();
            return dm;
        }
        return dm;
    }



    /// <summary>
    /// 存放所有的物品
    /// </summary>
    private List<Item> itemList = new List<Item>();

    /// <summary>
    /// 根据ID来获取物品
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public Item GetItemByID(int _ID)
    {
        return itemList.Find( i => { return i.item_ID == _ID; });
    }


    // Use this for initialization

}
/// <summary>
/// 物品类
/// </summary>
[System.Serializable]
public class Item
{
    public string item_Name = "Item Name";
    public string item_Type = "Item Type";
    [Multiline]
    public string description = "Description Here";
    public int item_ID;//物品ID
    public string item_Img;//图片名字
    public string item_Effect;//特效的名字
    public string item_Sfx;//音效的名字

    public bool gold;
    public Equipment_Type equipment_Type;

    public int price;
    public int hp, mp, atk, def, spd, hit;
    public float criPercent, atkSpd, atkRange, moveSpd;
}
/// <summary>
/// 穿戴类型
/// </summary>
public enum Equipment_Type
{
    Null = 0, Head_Gear = 1, Armor = 2, Shoes = 3, Accessory = 4, Left_Hand = 5, Right_Hand = 6, Two_Hand = 7
}
