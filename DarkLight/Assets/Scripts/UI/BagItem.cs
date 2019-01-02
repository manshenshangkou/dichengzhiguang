using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
/// <summary>
/// 挂载在预设物上  
/// </summary>
public class BagItem : MonoBehaviour
{
    //当前物品的图片
    private Sprite sprite;
    //当前物品
    public GoodsModel CurrentGoods;

    public static event Action<GoodsModel> OnShowInfoPanel;
    
    void Start()
    {
        sprite = GetComponent<Image>().sprite;
        string ImageName = sprite.name;
        
        GetComponent<Button>().onClick.AddListener(Show);
    }
    public void Show()
    {
        if (OnShowInfoPanel!=null)
        {
            OnShowInfoPanel(CurrentGoods);
        }
    }

    public void Init(GoodsModel gm)
    {
        CurrentGoods = gm;
    }
}
