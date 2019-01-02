using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class CompoundPanel : TTUIPage {

    string Item1Name;
    string Item2Name;
    string targetEquipName;
    int item1Num;
    int item2Num;
    Image targetEquipImage;
    Button btnOK;
    string trueName1;
    string trueName2;
    Sprite sp1;
    Sprite sp2;

    bool 匹配 = false;
    bool 合成 = false;
    List<string> currentItem;

    public CompoundPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/CompoundPanel";
    }
    public override void Awake(GameObject go)
    {
        btnOK = ShopPanel.FindChildComponent<Button>(transform);
        btnOK.onClick.AddListener(OnOKClick);
        sp1 = transform.Find("Item1").GetChild(0).GetComponent<Image>().sprite;
        sp2 = transform.Find("Item1").GetChild(0).GetComponent<Image>().sprite;
    }
    public override void Refresh()
    {
        transform.Find("Item1").GetChild(0).GetComponent<Image>().sprite = sp1;
        transform.Find("Item2").GetChild(0).GetComponent<Image>().sprite = sp2;
        base.Refresh();
    }
    void OnOKClick()
    {
        if (Save.CompoundList == null)
        {
            Save.CompoundList = new List<Compound>();
        }
        //材料1 和 材料2 能合成
        Item1Name = transform.Find("Item1").GetChild(0).GetComponent<Image>().sprite.name;
        Item2Name = transform.Find("Item2").GetChild(0).GetComponent<Image>().sprite.name;
        foreach (Compound item in Save.CompoundList)
        {
            if (Item1Name == item.Item1ID)
            {
                if (Item2Name == item.Item2ID)
                {
                    trueName1 = Item1Name;
                    trueName2 = Item2Name;
                    匹配 = true;
                }
                else
                {
                    匹配 = false;
                }
            }
            else if (Item2Name == item.Item1ID)
            {
                if (Item1Name == item.Item2ID)
                {
                    trueName1 = Item2Name;
                    trueName2 = Item1Name;
                    匹配 = true;
                }
                else
                {
                    匹配 = false;
                }
            }
            else
            {
                匹配 = false;
            }
            if (匹配)
            {
                //并且数量够，判断背包里的数量是不是够
                GoodsModel tempGM1 = Save.goodsModelList.Find(x => x.Id == int.Parse(trueName1));
                GoodsModel tempGM2 = Save.goodsModelList.Find(x => x.Id == int.Parse(trueName2));
                if (tempGM1 == tempGM2)
                {
                    if (tempGM1.Num >= item.Item1Amount + item.Item2Amount)
                        {
                            item1Num = item.Item1Amount; item2Num = item.Item2Amount; targetEquipName = item.ResID;
                            合成 = true;
                        }
                }
                if (tempGM1.Num >= item.Item1Amount && tempGM2.Num >= item.Item2Amount)
                {
                    item1Num = item.Item1Amount; item2Num = item.Item2Amount; targetEquipName = item.ResID;
                    合成 = true;
                }
            }
        }
        if (合成)
        {
            GoodsModel gm1 = Save.goodsModelList.Find(x => x.Id == int.Parse(trueName1));
            GoodsModel gm2 = Save.goodsModelList.Find(x => x.Id == int.Parse(trueName2));
            Item targetItem = DataMgr.GetInstance().GetItemByID(int.Parse(targetEquipName));
            GoodsModel targetGm = Save.goodsModelList.Find(x => x.Id == int.Parse(targetEquipName));
            gm1.Num -= item1Num;
            gm2.Num -= item2Num;
            if (gm1.Num <= 0)
            {
                gm1 = null;
            }
            if (gm2.Num <= 0)
            {
                gm2 = null;
            }
            if (targetGm != null)
            {
                targetGm.Num++;
            }
            else
            {
                Save.goodsModelList.Add(new GoodsModel() { Id = targetItem.item_ID, Num = 1, Function = targetItem.equipment_Type.ToString() });
            }
        }
        if (合成)
        {
            ShowPage<TipPanel>("合成成功");
            Save.SaveGoods();
            return;
        }

        if (匹配 == false||合成==false)
        {
            ShowPage<TipPanel>("合成失败");
        }

    }
}
