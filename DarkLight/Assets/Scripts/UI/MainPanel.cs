using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MainPanel : TTUIPage {

    Button StatusBtn, BagBtn, EquipBtn, SkillBtn, TishiBtn, CloseBtn, SetBtn, TaskBtn;

    public MainPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.Normal)
    {
        uiPath = "UIPrefab/MainPanel";
    }

    public override void Awake(GameObject go)
    {
        StatusBtn = transform.Find("StatusButton").GetComponent<Button>();
        BagBtn = transform.Find("BagButton").GetComponent<Button>();
        EquipBtn = transform.Find("EquipButton").GetComponent<Button>();
        SkillBtn = transform.Find("SkillButton").GetComponent<Button>();
        TishiBtn = transform.Find("ShopShowButton").GetComponent<Button>();
        CloseBtn = transform.Find("CloseButton").GetComponent<Button>();
        SetBtn = transform.Find("SetButton").GetComponent<Button>();
        TaskBtn = transform.Find("TaskButton").GetComponent<Button>();

        BagBtn.onClick.AddListener(OnBagBtn);
        EquipBtn.onClick.AddListener(OnEquipBtn);
        StatusBtn.onClick.AddListener(OnStatusBtn);
        TaskBtn.onClick.AddListener(OnTaskBtn);
        TishiBtn.gameObject.SetActive(false);//刚开始的时候把提示按钮设为false，人物进去，按钮显示，人物出来，按钮消失
        ShopItemlist.OnNPCTrriger += ShowTishi;
        //Save.SaveGoods();
    }



    /// <summary>
    /// 显示提示按钮
    /// </summary>
    /// <param name="isOpen">按钮是否显示</param>
    /// <param name="_itemList">NPC传来的物品列表</param>
    public void ShowTishi(bool isOpen,List<int> _itemList)
    {
        TishiBtn.gameObject.SetActive(isOpen);
        TishiBtn.image.DOFade(0.5f, 1).SetLoops(-1);
        if (isOpen)
        {
            if (_itemList.Count == 0)
            {
                TishiBtn.onClick.AddListener(() => { TTUIPage.ShowPage<CompoundPanel>(); });

            }
            else
            {
                TishiBtn.onClick.AddListener(() => { TTUIPage.ShowPage<ShopPanel>(_itemList); });
            }
        }
        if (!isOpen)
        {
            if (TTUIPage.allPages.ContainsKey("ShopPanel"))
            {
                TTUIPage.ClosePage<ShopPanel>();
                TishiBtn.onClick.RemoveAllListeners();
            }
            if (TTUIPage.allPages.ContainsKey("CompoundPanel"))
            {
                TTUIPage.ClosePage<CompoundPanel>();
                TishiBtn.onClick.RemoveAllListeners();
            }
        }
    }
    #region 背包界面的显示与关闭
    int bagTemp = 0;
    private void OnBagBtn()
    {
        if (bagTemp % 2 == 0)
        {
            ShowPage<BagPanel>();
        }
        else
        {
            TTUIPage.ClosePage<BagPanel>();
        }
        bagTemp++;
    }
    #endregion
    #region 装备界面的显示与关闭
    int equipTemp = 0;
    private void OnEquipBtn()
    {
        if (equipTemp % 2 == 0)
        {
            ShowPage<EquipPanel>();
        }
        else
        {
            TTUIPage.ClosePage<EquipPanel>();
        }
        equipTemp++;
    }
    #endregion
    #region 状态界面的显示与关闭
    int statusTemp = 0;
    private void OnStatusBtn()
    {
        if (statusTemp % 2 == 0)
        {
            ShowPage<StatusPanel>();
        }
        else
        {
            TTUIPage.ClosePage<StatusPanel>();
        }
        statusTemp++;
    }
    #endregion
    #region 任务界面的显示与关闭
    int taskTemp = 0;
    private void OnTaskBtn()
    {
        if (taskTemp % 2 == 0)
        {
            ShowPage<TaskPanel>();
        }
        else
        {
            TTUIPage.ClosePage<TaskPanel>();
        }
        taskTemp++;
    }
    #endregion


}
