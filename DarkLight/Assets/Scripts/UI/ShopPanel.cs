using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopPanel : TTUIPage
{
    private GameObject itemPrefab;
    private List<int> shopIntList;
    Transform content;
    GameObject Prefab;
    GameObject goodDescribe;
    ToggleGroup group;

    public ShopPanel() : base(UIType.Normal, UIMode.NeedBack, UICollider.Normal)
    {
        uiPath = "UIPrefab/ShopPanel";
    }
    public override void Awake(GameObject go)
    {
        group = transform.Find("Group").GetComponent<ToggleGroup>();
        Prefab = Resources.Load<GameObject>("UIPrefab/ShopItem");
        //通过事件传来的ShopItemlist.Itemid数据
        shopIntList = (List<int>)data;
        //找到每个物品的父物体
        content = GameObject.Find("ShopContent").transform;
        foreach (var imageID in shopIntList)
        {
            //找到物品的预制体
            itemPrefab = GameObject.Instantiate(Prefab, content, false);
            //手动设置物品的Scale————上一行的false是不采用世界坐标，同样也能实现固定Scale
            //itemPrefab.transform.localScale = Vector3.one;
            //替换图片
            itemPrefab.transform.GetChild(2).name = imageID.ToString();
            itemPrefab.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load(@"ShopImage/" + imageID, typeof(Sprite)) as Sprite;
            itemPrefab.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { OnItemBtnClick(imageID, imageID.ToString()); });
            itemPrefab.transform.GetChild(2).GetChild(0).GetComponent<Toggle>().group = group;
            Item info = DataMgr.GetInstance().GetItemByID(imageID);
            itemPrefab.transform.GetChild(3).GetComponent<Text>().text = info.item_Name;
            itemPrefab.transform.GetChild(4).GetComponent<Text>().text = info.item_Type;
            itemPrefab.transform.GetChild(5).GetComponent<Text>().text = info.price.ToString();
            itemPrefab.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => {Save.BuyItem(info);Debug.Log("购买"); });
        }
        goodDescribe = GameObject.Find("Describe");
    }


    void OnItemBtnClick(int _itemId,string _btname)
    {
        GameObject.Find(_btname).transform.GetChild(0).GetComponent<Toggle>().isOn = true;
        Item describeItem = DataMgr.GetInstance().GetItemByID(_itemId);
        goodDescribe.transform.GetChild(1).GetComponent<Text>().text = describeItem.item_Name;
        goodDescribe.transform.GetChild(2).GetComponent<Text>().text = describeItem.description;
        goodDescribe.transform.GetChild(3).GetComponent<Text>().text = describeItem.item_Type;
        //bt.GetChild(4).GetComponent<Text>().text = describeItem.item_Type + describeItem.item_Name;

    }
    public override void Hide()
    {
        base.Hide();
        GameObject.Destroy(gameObject);
        //goodDescribe.transform.GetChild(1).GetComponent<Text>().text = "这里显示物品名";
        //goodDescribe.transform.GetChild(2).GetComponent<Text>().text = "这里是物品描述";
        //goodDescribe.transform.GetChild(3).GetComponent<Text>().text = "这里是装备类型";
    }
    static public T FindChildComponent<T>(Transform trans) where T : Component
    {
        if (trans == null) return null;
        var comp = trans.GetComponentInChildren<T>();
        if (comp)
        {
            return comp;
        }
        else
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                return trans.GetChild(i).GetComponentInChildren<T>();
            }
        }
        return comp;
    }

}
