using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
/// <summary>
/// 解析数据
/// </summary>
public class Analysis : MonoBehaviour {

	void Awake () {
        // 用户数据解析
        UserAnalysis();
        // 物品数据解析
        GoodsAnalysis();
        //装备数据解析
        EquipAnalysis();
        //合成数据解析
        CompoundAnalysis();
        //任务数据解析
        TaskAnalysis();
        //当前任务数据解析
        CurrentTaskAnalysis();
    }
    /// <summary>
    /// 用户数据解析
    /// </summary>
	void UserAnalysis() 
    {
        TextAsset userTA = Resources.Load("Setting/UserJson") as TextAsset;
        if (!userTA)
        {
            return;
        }
        //Save.userModelList = JsonConvert.DeserializeObject<List<UserModel>>(userTA.text);
        //print(userTA.text);
    }

    /// <summary>
    /// 物品数据解析
    /// </summary>
    void GoodsAnalysis()
    {
        TextAsset goodsTA = Resources.Load("Setting/GoodsList") as TextAsset;
        if (!goodsTA)
        {
            Debug.Log("GoodsList文件不存在！");
            return;
        }
        Save.goodsModelList = JsonConvert.DeserializeObject<List<GoodsModel>>(goodsTA.text);
        //print(goodsTA.text);
    }
    void EquipAnalysis()
    {
        TextAsset equipTA = Resources.Load("Setting/EquipList") as TextAsset;
        if (!equipTA)
        {
            Debug.Log("EquipList文件不存在！");
            return;
        }
        Save.EquipList = JsonConvert.DeserializeObject<List<Item>>(equipTA.text);
        //print(equipTA.text);
    }
    void CompoundAnalysis()
    {
        TextAsset comTA = Resources.Load("Setting/CompoundList") as TextAsset;
        if (!comTA)
        {
            Debug.Log("CompoundList文件不存在！");
            return;
        }
        Save.CompoundList = JsonConvert.DeserializeObject<List<Compound>>(comTA.text);
        //print(comTA.text);
    }
    void TaskAnalysis()
    {
        TextAsset taskTA = Resources.Load("Setting/TaskList") as TextAsset;
        if (!taskTA)
        {
            Debug.Log("TaskList文件不存在！");
            return;
        }
        Save.TaskDic = JsonConvert.DeserializeObject<Dictionary<string, Task>>(taskTA.text);
        //print(taskTA.text);
    }
    void CurrentTaskAnalysis()
    {
        TextAsset currentTaskTA = Resources.Load("Setting/CurrentTaskList") as TextAsset;
        if (!currentTaskTA)
        {
            Debug.Log("TaskList文件不存在！");
            return;
        }
        Save.currentTaskDic = JsonConvert.DeserializeObject<Dictionary<string, Task>>(currentTaskTA.text);
        //print(taskTA.text);
    }
}
