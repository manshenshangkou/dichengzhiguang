using UnityEngine;
using System.Collections;
/// <summary>
/// 挂载在预设物上  
/// </summary>
public class ItemButton : MonoBehaviour {
    //当前物品的图片
    public UISprite Sprite;
    //当前物品
    public GoodsModel CurrentGoods;
    //选中物品的Id
    public static int CurrentGoodsId;
    //物品信息显示框
    public Transform GoodsInfo;
    // Use this for initialization
    void Start () {
        Sprite = GetComponent<UISprite>();
        EventDelegate dl = new EventDelegate(this, "Show");
        GetComponent<UIButton>().onClick.Add(dl);
        GoodsInfo = transform.parent.parent.parent.parent.Find("ShowInfo");
      
        //获取预设物图片的名字
        //for (int i = 0; i < Save.SaveGoods.GoodsList.Count; i++)
        //{
        //    if (Sprite.spriteName== Save.SaveGoods.GoodsList[i].Nature)
        //    {
        //        CurrentGoods = Save.SaveGoods.GoodsList[i];
        //        break;
        //    }
        //}
	}
    public  void  Show()
    {
        GoodsInfo.GetComponent<TweenScale>().PlayForward();
        GoodsInfo.GetChild(0).GetComponent<UISprite>().spriteName = CurrentGoods.Nature;
        GoodsInfo.GetChild(1).GetComponent<UILabel>().text = CurrentGoods.Name;
        GoodsInfo.GetChild(2).GetComponent<UILabel>().text = CurrentGoods.Function;

        CurrentGoodsId = CurrentGoods.Id;
    }
}
