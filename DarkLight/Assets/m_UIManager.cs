using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using UnityEditor;
using UnityEngine.UI;

public class m_UIManager : MonoBehaviour
{
    public GameObject MainPanel;//主界面
    public Text Hp, MaxHp, Attack, Speed;
    public GameObject GoodsPrefab;//物品预设物
    public Transform Grid;//背包空格
    public GameObject StatusPanel;
    public GameObject EquipPanel;
    public GameObject SkillPanel;
    GameObject[] GridArray;
    //GameObject content;
    void Start()
    {
        //content = GameObject.Find("Grid");
        //GridArray = new GameObject[content.transform.childCount - 1];
        //for (int i = 0; i < content.transform.childCount-1; i++)
        //{
        //    GridArray[i] = content.transform.GetChild(i).gameObject;
        //}
        RefreshNature();
        Grid.gameObject.SetActive(false);
        StatusPanel.SetActive(false);
        EquipPanel.SetActive(false);
        SkillPanel.SetActive(false);
    }
    /// <summary>
    /// 点击登录按钮
    /// </summary>
    public void LoginBtnClick()
    {
        MainPanel.SetActive(true);//隐藏主界面，覆盖登录按钮
        //刷新属性界面数据
    }
    /// <summary>
    /// 刷新属性界面数据
    /// </summary>
    public void RefreshNature()
    {
        Hp.text = Nature.Instance().Hp + "";
        MaxHp.text = Nature.Instance().MaxHp + "";
        Attack.text = Nature.Instance().Attack + "";
        Speed.text = Nature.Instance().Speed + "";
        // 吃药的方法   使用物品后 属性改变
        Nature.Instance().Eat();
    }
    /// <summary>
    /// 点击背包按钮
    /// </summary>
    int temp = 0;
    public void BagBtnClick()
    {
        if (temp % 2 == 0)
        {
            //显示背包数据
            ShowBag();
            //显示背包动画
            // GoodsAnimation.PlayForward();
        }
        else
        {
            // 清除背包数据
            ClearBag();
            //隐藏背包动画
            // GoodsAnimation.PlayReverse();
            //倒放 提示框动画
            //ShowInfoAnimation.PlayReverse();

        }
        temp++;
    }
    /// <summary>
    /// 显示背包数据
    /// </summary>
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
                //创建物品 NGUITools.AddChild(父物体，预设物);
                //GameObject go = NGUITools.AddChild(GameObject.Find("Content").transform.GetChild(j).gameObject, GoodsPrefab);
                GameObject go = Instantiate(GoodsPrefab);
                //go.transform.SetParent(content.transform.GetChild(j),false);
                ////显示物体的图片及数量
                go.GetComponent<Image>().sprite = Resources.Load(@"Bag/" + item.Nature,typeof(Sprite)) as Sprite;
                go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
                j++;
            }
        }
    }
    /// <summary>
    /// 清除背包数据
    /// </summary>
    public void ClearBag()
    {
        Grid.gameObject.SetActive(false);
        //删除之前创建物品的预设物
        for (int i = 0; i < GridArray.Length; i++)
        {
            if (GridArray[i].transform.childCount != 0)
            {
                Transform tempGo = GridArray[i].transform.GetChild(0);
                tempGo.parent = null;
                Destroy(tempGo.gameObject);
            }
        }
    }
    /// <summary>
    ///提示框的返回按钮
    /// </summary>
    public void ShowInfo_BackBtnClick()
    {
        GameObject.Find("ShowInfo").SetActive(false);
        //Destroy(GameObject.Find("ShowInfo"));
        //倒放 提示框动画
        //ShowInfoAnimation.PlayReverse();
    }
    /// <summary>
    /// 提示框中的使用物品按钮方法
    /// </summary>
    //当前使用的物品
    public GoodsModel CurrentGoods;
    public void ShowInfo_UseGoods(int id)
    {
        //if (Save.SaveGoods.GoodsList.Count>12)
        //{
        //    Debug.LogError("背包已满");
        //    return;
        //}
        //id = m_ItemButton.CurrentGoodsId;
        for (int i = 0; i < Save.goodsModelList.Count; i++)
        {
            if (id == Save.goodsModelList[i].Id)
            {
                CurrentGoods = Save.goodsModelList[i];
            }
        }
        //使用物品  类型
        switch (id)
        {
            case 0:
                Nature.Instance().Hp += CurrentGoods.Value;
                if (Nature.Instance().Hp >= Nature.Instance().MaxHp)
                {
                    Nature.Instance().Hp = Nature.Instance().MaxHp;
                }
                break;
            case 1:
                Nature.Instance().MaxHp += CurrentGoods.Value;
                break;
            case 2:
                Nature.Instance().Attack += CurrentGoods.Value;
                break;
            case 3:
                Nature.Instance().Speed += CurrentGoods.Value;
                break;
            case 4:
                Nature.Instance().Attack += CurrentGoods.Value;
                break;

            default:
                break;
        }
        CurrentGoods.Num--;
        if (CurrentGoods.Num <= 0)
        {
            //ShowInfoAnimation.PlayReverse();
            CurrentGoods.Num = 0;
        }
        //刷新属性界面数据
        RefreshNature();
        //刷新背包界面数据
        ShowBag();

        for (int i = 0; i < Save.goodsModelList.Count; i++)
        {
            if (Save.goodsModelList[i].Id == CurrentGoods.Id)
            {
                Save.goodsModelList[i] = CurrentGoods;
            }
        }
    }
    /// <summary>
    /// 点击保存按钮
    /// </summary>
    public void SaveBtnClick()
    {
        string path = Application.dataPath + @"/Resources/Setting/UserJson.txt";
        FileInfo info = new FileInfo(path);
        StreamWriter sw = info.CreateText();
        //string json = JsonMapper.ToJson(Save.SaveUser);
        //sw.Write(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();

        string path1 = Application.dataPath + @"/Resources/Setting/GoodsList.json";
        //string path1 = Application.dataPath + @"/Resources/Item/Item_Data.json";
        FileInfo info1 = new FileInfo(path1);
        StreamWriter sw1 = info1.CreateText();
        //string json1 = JsonMapper.ToJson(Save.SaveGoods);
        //sw1.Write(json1);
        sw1.Close();
        sw1.Dispose();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 点击状态按钮
    /// </summary>
    int statusInt = 0;
    public void OnStatusBtnClick()
    {
        if (statusInt % 2 == 0)
        {
            StatusPanel.SetActive(true);
        }
        else
        {
            StatusPanel.SetActive(false);
        }
        statusInt++;
    }

    /// <summary>
    /// 点击装备按钮
    /// </summary>
    int euiqpInt = 0;
    public void OnEquipBtnClick()
    {
        if (euiqpInt % 2 == 0)
        {
            EquipPanel.SetActive(true);
        }
        else
        {
            EquipPanel.SetActive(false);
        }
        euiqpInt++;
    }
    /// <summary>
    /// 点击装备按钮
    /// </summary>
    int skillInt = 0;
    public void OnSkillBtnClick()
    {
        if (skillInt % 2 == 0)
        {
            SkillPanel.SetActive(true);
        }
        else
        {
            SkillPanel.SetActive(false);
        }
        skillInt++;
    }

}
