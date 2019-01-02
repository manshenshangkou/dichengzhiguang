
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    /// <summary>
    /// 任务ID
    /// </summary>
    public string taskID;
    /// <summary>
    /// 任务名字
    /// </summary>
    public string taskName;
    /// <summary>
    /// 任务描述 
    /// </summary>
    public string description;
    /// <summary>
    /// 任务目标
    /// </summary>
    public List<TaskCondition> taskConditions = new List<TaskCondition>();
    /// <summary>
    /// 任务奖励
    /// </summary>
    public List<TaskReward> taskRewards = new List<TaskReward>();

    public Task(string _id, string _name, string _des, List<TaskCondition> _con, List<TaskReward> _rew)
    {
        this.taskID = _id; taskName = _name; description = _des; taskConditions = _con; taskRewards = _rew;
    }
}

public class TaskReward
{
    public string id;//奖励id
    public int amount = 0;//奖励数量

    public TaskReward(string id, int amount)
    {
        this.id = id;
        this.amount = amount;
    }
}

public class TaskCondition
{
    public string id;//条件id
    public int nowAmount;//条件id的当前进度
    public int targetAmount;//条件id的目标进度
    public int isFinish = 0;//记录是否满足条件

    public TaskCondition(string id, int nowAmount, int targetAmount)
    {
        this.id = id;
        this.nowAmount = nowAmount;
        this.targetAmount = targetAmount;
    }

}