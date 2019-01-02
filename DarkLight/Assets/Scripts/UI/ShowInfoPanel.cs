using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
public class ShowInfoPanel : TTUIPage
{
    private Button closeBtn;
    private Button yesBtn;
    Text text;
    static Item item;

    public ShowInfoPanel() : base(UIType.PopUp, UIMode.NeedBack, UICollider.Normal)
    {
        uiPath = "UIPrefab/ShowInfo";
    }
    public override void Awake(GameObject go)
    {
        text = ShopPanel.FindChildComponent<Text>(transform);
        text.text = data.ToString();
        closeBtn = transform.Find("CloseButton").GetComponent<Button>();
        yesBtn = transform.Find("YesButton").GetComponent<Button>();

        yesBtn.onClick.AddListener(OnYesBtn);
        closeBtn.onClick.AddListener(OnClickBtn);
    }

    public static void Init(Item _item)
    {
        item = _item;
    }

    void OnClickBtn()
    {
        GameObject.Destroy(gameObject);
    }

    void OnYesBtn()
    {
        if (Save.goodsModelList == null)
        {
            Save.goodsModelList = new List<GoodsModel>();
        }
        GoodsModel gm = Save.goodsModelList.Find(x => x.Id == item.item_ID);
        if (gm != null)
        {
            gm.Num++;
        }
        else
        {
            Save.goodsModelList.Add(new GoodsModel() { Id = item.item_ID, Num = 1, Function = item.equipment_Type.ToString() });
        }
        Save.SaveGoods();
        SoundManager.instance.PlayingSound("Level_Up");
        ShowPage<TipPanel>("购买成功");
        GameObject.Destroy(gameObject);
    }
}
