using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using System;

public class EquipPanel : TTUIPage
{
    public EquipPanel() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/EquipPanel";
    }
    //获取各个部位
    Transform Head;
    Transform Armor;
    Transform LeftHand;
    Transform RightHand;
    Transform Shoes;
    Transform Accessory;

    Transform EquipInfo;
    private Text nameText;
    private Text natureText;
    private GameObject equipItem;
    Button UseButton;


    List<GameObject> tempEquipList = new List<GameObject>();
    public override void Awake(GameObject go)
    {
        Head = transform.Find("HeadImage");
        Armor = transform.Find("ArmorImage");
        LeftHand = transform.Find("LeftHandImage");
        RightHand = transform.Find("RightHandImage");
        Shoes = transform.Find("ShoeImage");
        Accessory = transform.Find("AccessoryImage");

        EquipInfo = transform.Find("EquipInfo");
        nameText = EquipInfo.Find("nameText").GetComponent<Text>();
        natureText = EquipInfo.Find("natureText").GetComponent<Text>();
        EquipInfo.Find("CancelButton").GetComponent<Button>().onClick.AddListener(() => { EquipInfo.gameObject.SetActive(false); ShowInfoActive++; });

        equipItem = Resources.Load<GameObject>("UIPrefab/EquipItem");
        EquipItem.OnShowInfoPanel += ShowInfo;
        //脱掉按钮————和装备交互
        UseButton = EquipInfo.Find("takeoffButton").GetComponent<Button>();
        UseButton.onClick.AddListener(OnTake_OffBtnClick);
    }

    public override void Refresh()
    {
        base.Refresh();
        EquipInfo.gameObject.SetActive(false);
        ShowEquip();

    }
    public void ShowEquip()
    {

        ClearBag();
        tempEquipList.Clear();
        if (Save.EquipList == null)
        {
            return;
        }
        foreach (Item item in Save.EquipList)
        {
            switch (item.equipment_Type)
            {
                case Equipment_Type.Null:
                    break;
                case Equipment_Type.Head_Gear:
                    youhua(item,Head);
                    break;
                case Equipment_Type.Armor:
                    youhua(item, Armor);
                    break;
                case Equipment_Type.Shoes:
                    youhua(item, Shoes);
                    break;
                case Equipment_Type.Accessory:
                    youhua(item, Accessory);
                    break;
                case Equipment_Type.Left_Hand:
                    youhua(item, LeftHand);
                    break;
                case Equipment_Type.Right_Hand:
                    youhua(item, RightHand);
                    break;
                case Equipment_Type.Two_Hand:
                    youhua(item, RightHand);
                    break;
                default:
                    break;
            }
        }
    }
    public void ClearBag()
    {
        //删除之前创建物品的预设物
        for (int i = 0; i < tempEquipList.Count; i++)
        {
            if (tempEquipList.Count != 0)
            {
                Transform tempGo = tempEquipList[i].transform;
                tempGo.parent = null;
                GameObject.Destroy(tempGo.gameObject);
            }
        }
    }
    int ShowInfoActive = 0;

    public void ShowInfo(Item _item)
    {
        if (ShowInfoActive % 2 == 0)
        {
            EquipInfo.gameObject.SetActive(true);
        }
        else
        {
            EquipInfo.gameObject.SetActive(false);
        }
        Item item = DataMgr.GetInstance().GetItemByID(_item.item_ID);
        nameText.text = item.item_Name;
        natureText.text = item.description;
        Vector3 EquipInfoWorldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(TTUIRoot.Instance.root as RectTransform, Input.mousePosition, TTUIRoot.Instance.uiCamera, out EquipInfoWorldPos))
        {
            EquipInfo.position = EquipInfoWorldPos;
        }
        ShowInfoActive++;
        currentItem = _item;
    }

    Item currentItem;
    private void OnTake_OffBtnClick()
    {
        GoodsModel tempGoods = Save.goodsModelList.Find((x) => x.Id == currentItem.item_ID);
        tempGoods.Num++;
        Save.EquipList.Remove(currentItem);
        Save.SaveEquip();
        Save.SaveGoods();
        EquipInfo.gameObject.SetActive(false);
    }
    void youhua(Item item,Transform fatherTransform)
    {
        GameObject tempItem = GameObject.Instantiate(equipItem, fatherTransform);
        tempItem.GetComponent<EquipItem>().Init(item);
        tempItem.GetComponent<Image>().sprite = Resources.Load("ShopImage/" + item.item_ID, typeof(Sprite)) as Sprite;
        tempEquipList.Add(tempItem);
    }
}
