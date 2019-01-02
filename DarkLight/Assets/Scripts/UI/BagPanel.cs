using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Newtonsoft.Json;
using System.IO;

public class BagPanel : TTUIPage
{
    Transform Grid;//背包空格
    List<GoodsModel> itemGoodsList;
    List<GameObject> GridList = new List<GameObject>();
    GameObject bagItem;
    Transform EquipInfo;
    Text nameText;
    Text natureText;
    
    public BagPanel() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/BagPanel";
    }
    

    public override void Awake(GameObject go)
    {
        Grid = ShopPanel.FindChildComponent<GridLayoutGroup>(transform).transform;
        bagItem = Resources.Load<GameObject>("UIPrefab/BagItem");
        BagItem.OnShowInfoPanel += ShowInfo;

        EquipInfo = transform.Find("EquipInfo");
        nameText = EquipInfo.Find("nameText").GetComponent<Text>();
        natureText = EquipInfo.Find("natureText").GetComponent<Text>();
        EquipInfo.Find("CancelButton").GetComponent<Button>().onClick.AddListener(() => { EquipInfo.gameObject.SetActive(false); ShowInfoActive++; });
        //使用按钮————和装备交互
        UseButton = EquipInfo.Find("UseButton").GetComponent<Button>();
        UseButton.onClick.AddListener(OnUseBtnClick);

    }


    public override void Refresh()
    {
        EquipInfo.gameObject.SetActive(false);
        ShowBag();
    }
    public override void Hide()
    {
        base.Hide();
        //GameObject.Destroy(gameObject);
    }
    public void ShowBag()
    {
        //清除背包
        ClearBag();
        Grid.gameObject.SetActive(true);

        //遍历物品信息
        int j = 0;
        foreach (GoodsModel item in Save.goodsModelList)
        {
            // if (Save.SaveGoods.GoodsList[j].Num !=0)
            if (item.Num != 0)//物品数量不等于零时
            {
                GridList.Add(Grid.transform.GetChild(j).gameObject);
                GameObject go = GameObject.Instantiate(bagItem, GridList[j].transform);
                ////显示物体的图片及数量
                go.GetComponent<Image>().sprite = Resources.Load("ShopImage/" + item.Id, typeof(Sprite)) as Sprite;
                go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
                go.GetComponent<BagItem>().Init(item);
                j++;
            }
        }
    }
    public void ClearBag()
    {
        Grid.gameObject.SetActive(false);
        //删除之前创建物品的预设物
        for (int i = 0; i < GridList.Count; i++)
        {
            if (GridList[i].transform.childCount != 0)
            {
                Transform tempGo = GridList[i].transform.GetChild(0);
                //tempGo.parent = null;
                GameObject.Destroy(tempGo.gameObject);
            }
        }
    }
    int ShowInfoActive = 0;
    Button UseButton;

    public void ShowInfo(GoodsModel gm)
    {
        if (ShowInfoActive % 2 == 0)
        {
            EquipInfo.gameObject.SetActive(true);
        }
        else
        {
            EquipInfo.gameObject.SetActive(false); 
        }
        Item item = DataMgr.GetInstance().GetItemByID(gm.Id);
        nameText.text = item.item_Name;
        natureText.text = item.description;
        Vector3 EquipInfoWorldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(TTUIRoot.Instance.root as RectTransform, Input.mousePosition, TTUIRoot.Instance.uiCamera, out EquipInfoWorldPos))
        {
            EquipInfo.position = EquipInfoWorldPos;
        }
        ShowInfoActive++;
        equipGoodsModel = gm;
    }

    private GoodsModel equipGoodsModel;

    private void OnUseBtnClick()
    {
        if (equipGoodsModel == null)
        {
            return;
        }
        Equipment_Type Goodsequipment_Type = (Equipment_Type)Enum.Parse(typeof(Equipment_Type), equipGoodsModel.Function);
        if (Save.EquipList == null)
        {
            Save.EquipList = new List<Item>();
        }
        Item currentEquip = Save.EquipList.Find(x => x.item_ID == equipGoodsModel.Id);
        if (currentEquip != null)
        {
            return;
        }
        else
        {
            currentEquip = new Item() { item_ID = equipGoodsModel.Id, equipment_Type = Goodsequipment_Type };
            for (int i = 0; i < Save.EquipList.Count; i++)
            {
                if (currentEquip.equipment_Type == Save.EquipList[i].equipment_Type)
                {
                    GoodsModel gm = Save.goodsModelList.Find(x => x.Id == Save.EquipList[i].item_ID);
                    gm.Num++;

                    Save.EquipList.Remove(Save.EquipList[i]);
                }
            }
            Save.EquipList.Add(currentEquip);

        }

        Save.SaveEquip();


        //背包界面里的物品少一个
        equipGoodsModel.Num--;
        if (equipGoodsModel.Num <= 0)
        {
            equipGoodsModel = null;
        }
        //背包数据更新
        Save.SaveGoods();


        ShowPage<TipPanel>("穿戴成功");
        EquipInfo.gameObject.SetActive(false);
        ShowInfoActive++;
    }
}
