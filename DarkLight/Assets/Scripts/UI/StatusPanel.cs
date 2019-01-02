using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanel : TTUIPage
{

    float atkNum;//攻击力
    float defNum;//防御力
    float spdNum;//敏捷
    float hitNum;//暴击伤害
    float critNum;//暴击几率
    float atkSpdNum;//攻速
    float atkRangeNum;//攻击范围
    float moveSpdNum;//移动速度


    Text atkText;
    Text defText;
    Text spdText;
    Text hitText;
    Text critText;
    Text atkSpdText;
    Text atkRangeText;
    Text moveSpdText;

    public StatusPanel() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/StatusPanel";
    }
    public override void Awake(GameObject go)
    {
        atkText = transform.Find("攻击力").GetComponent<Text>();
        defText = transform.Find("防御力").GetComponent<Text>();
        spdText = transform.Find("敏捷").GetComponent<Text>();
        hitText = transform.Find("暴击").GetComponent<Text>();
        critText = transform.Find("暴击率").GetComponent<Text>();
        atkSpdText = transform.Find("攻击速度").GetComponent<Text>();
        atkRangeText = transform.Find("攻击范围").GetComponent<Text>();
        moveSpdText = transform.Find("移动速度").GetComponent<Text>();
    }
    public override void Refresh()
    {
        foreach (Item nullItem in Save.EquipList)
        {
            Item item = DataMgr.GetInstance().GetItemByID(nullItem.item_ID);
            atkNum += item.atk;
            defNum += item.def;
            spdNum += item.spd;
            hitNum += item.hit;
            critNum += item.criPercent;
            atkSpdNum += item.atkSpd;
            atkRangeNum += item.atkRange;
            moveSpdNum += item.moveSpd;
        }

        atkText.text = atkText.name + ":" + atkNum.ToString();
        defText.text = defText.name + ":" + defNum.ToString();
        spdText.text = spdText.name + ":" + spdNum.ToString();
        hitText.text = hitText.name + ":" + hitNum.ToString();
        critText.text = critText.name + ":" + critNum.ToString();
        atkSpdText.text = atkSpdText.name + ":" + atkSpdNum.ToString();
        atkRangeText.text = atkRangeText.name + ":" + atkRangeNum.ToString();
        moveSpdText.text = moveSpdText.name + ":" + moveSpdNum.ToString();
    }
}
