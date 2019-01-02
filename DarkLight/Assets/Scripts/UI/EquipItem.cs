using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour
{
    //当前物品
    public Item CurrentEquip;

    public static event Action<Item> OnShowInfoPanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Show);
    }
    public void Show()
    {
        if (OnShowInfoPanel != null)
        {
            OnShowInfoPanel(CurrentEquip);
        }
    }

    public void Init(Item im)
    {
        CurrentEquip = im;
    }
}
