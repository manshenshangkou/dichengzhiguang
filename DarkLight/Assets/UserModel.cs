using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using TinyTeam.UI;

public class UserModel {
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Speed;
}
//public  class UserModelList
//{
//    public List<UserModel> UserList = new List<UserModel>();
//}

public class GoodsModel //商品信息
{
    public int Id;
    public string Name;
    public string Nature;//图片种类(图片名)
    public string Function;
    public int Value;//值
    public int Num;//数量
}
public class Compound
{
    public string ResID;//目标合成装备
    public string Item1ID;//材料1
    public int Item1Amount;//需要材料1的数量
    public string Item2ID;//材料2
    public int Item2Amount;//需要材料2的数量

}

public  class Save
{
    //public static UserModelList SaveUser;
    //public static GoodsModelList SaveGoods;

    public static List<GoodsModel> goodsModelList;
    public static List<UserModel> userModelList;
    public static List<Item> EquipList;
    public static List<Compound> CompoundList;
    public static Dictionary<string,Task> TaskDic;
    public static Dictionary<string, Task> currentTaskDic;


    public static void SaveGoods()
    {
        string path = Application.dataPath + @"/Resources/Setting/GoodsList.json";

        using (StreamWriter sw = new StreamWriter(path))
        {
            string json1 = JsonConvert.SerializeObject(goodsModelList);
            sw.Write(json1);
        }
        AssetDatabase.Refresh();
    }
    public static void SaveEquip()
    {
        //装备数据更新
        string path = Application.dataPath + @"/Resources/Setting/EquipList.json";
        using (StreamWriter sw = new StreamWriter(path))
        {
            string json1 = JsonConvert.SerializeObject(EquipList);
            sw.Write(json1);
        }
    }
    public static void SaveTask()
    {
        //任务数据更新
        string path = Application.dataPath + @"/Resources/Setting/CurrentTaskList.json";
        using (StreamWriter sw = new StreamWriter(path))
        {
            string json1 = JsonConvert.SerializeObject(currentTaskDic);
            sw.Write(json1);
        }
    }


    public static void BuyItem(Item _item)
    {
        //点击Buy按钮之后，判断钱够不够
        //钱够了，弹出提示框
        TTUIPage.ShowPage<ShowInfoPanel>("是否要购买此物品");
        //钱不够，给提示
        ShowInfoPanel.Init(_item);
    }
}



