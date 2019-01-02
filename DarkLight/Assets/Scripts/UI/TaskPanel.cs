using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;


public class TaskPanel : TTUIPage
{


    Button addTask1;
    Button addTask2;
    Button fight1;
    Button fight2;
    Button taskItem1;
    Button taskItem2;
    private GameObject taskItem;
    Transform content;

    
    public TaskPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/TaskPanel";
    }


    public override void Awake(GameObject go)
    {
        addTask1 = transform.Find("task1").GetComponent<Button>();
        addTask2 = transform.Find("task2").GetComponent<Button>();
        fight1 = transform.Find("fightEnemy1").GetComponent<Button>();
        fight2 = transform.Find("fightEnemy2").GetComponent<Button>();
        taskItem1 = transform.Find("Item1").GetComponent<Button>();
        taskItem2 = transform.Find("Item2").GetComponent<Button>();

        addTask1.onClick.AddListener(delegate() { OnAddTaskBtnClick("T001"); });
        addTask2.onClick.AddListener(delegate () { OnAddTaskBtnClick("T002"); });
        fight1.onClick.AddListener(delegate () { OnFightClick("Enemy1"); });
        fight2.onClick.AddListener(delegate () { OnFightClick("Enemy2"); });
        taskItem1.onClick.AddListener(delegate () { OnFightClick("Item1"); });
        taskItem2.onClick.AddListener(delegate () { OnFightClick("Item2"); });
    }
    public override void Refresh()
    {

    }
    void OnAddTaskBtnClick(string taskID)
    {
        if (Save.currentTaskDic == null)
        {
            Save.currentTaskDic = new Dictionary<string, Task>();
        }
        Task task = Save.TaskDic[taskID];
        if (task==null)
        {
            return;
        }


        if (!Save.currentTaskDic.ContainsKey(taskID))
        {
            content = ShopPanel.FindChildComponent<ContentSizeFitter>(transform).transform;
            taskItem = GameObject.Instantiate(Resources.Load<GameObject>("UIPrefab/TaskItem"), content);
            foreach (var item in task.taskConditions)
            {
                item.nowAmount = 0;
            }
            Task temptk = new Task(task.taskID, task.taskName, task.description, task.taskConditions, task.taskRewards);
            taskItem.GetComponent<TaskItem>().Init(temptk);
            Save.currentTaskDic.Add(temptk.taskID, temptk);
        }
        else
        {
            ShowPage<TipPanel>("这个任务已经接过啦！");
        }
    }

    void OnFightClick(string TaskConditionID)
    {
        foreach (var item in Save.currentTaskDic)
        {
            //item是每一个任务
            for (int i = 0; i < item.Value.taskConditions.Count; i++)
            {
                //for循环遍历任务的完成条件有几个
                if (item.Value.taskConditions[i].id == TaskConditionID)
                {
                    //判断当前打的怪跟对应任务的怪物一样
                    if (item.Value.taskConditions[i].nowAmount < item.Value.taskConditions[i].targetAmount)
                    {
                        item.Value.taskConditions[i].nowAmount++;

                    }
                }
            }
        }
        taskItem.GetComponent<TaskItem>().UpdataTask();
    }
    

   


}
