using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour {

    Text taskName;
    Text taskDescription;
    Text target1Name;
    Text target2Name;
    Text taskProgress1;
    Text taskProgress2;
    Button abandonBtn;
    Button achieveBtn;


    private Task task;
    public bool isAchieve;

    // Use this for initialization
    void Start () {
        taskName = transform.Find("TaskName").GetComponent<Text>();
        taskDescription = transform.Find("TaskDescription").GetComponent<Text>();
        target1Name = transform.Find("TaskCondition/target1").GetComponent<Text>();
        target2Name = transform.Find("TaskCondition/target2").GetComponent<Text>();
        taskProgress1 = transform.Find("TaskCondition/target1Progress").GetComponent<Text>();
        taskProgress2 = transform.Find("TaskCondition/target2Progress").GetComponent<Text>();
        abandonBtn = transform.Find("abandonBtn").GetComponent<Button>();
        achieveBtn = transform.Find("OKBtn").GetComponent<Button>();

        achieveBtn.onClick.AddListener(OnAchieveBtnClick);
        abandonBtn.onClick.AddListener(OnAbondenBtnClick);
        UpdataTask();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Init(Task _task)
    {
        task = _task;
    }

    int lastTask;
    public void UpdataTask()
    {
        taskName.text = task.taskName;
        taskDescription.text = task.description;
        target1Name.text = task.taskConditions[0].id;
        target2Name.text = task.taskConditions[1].id;
        taskProgress1.text = task.taskConditions[0].nowAmount + "/" + task.taskConditions[0].targetAmount;
        taskProgress2.text = task.taskConditions[1].nowAmount + "/" + task.taskConditions[1].targetAmount;
        for (int i = 0; i < task.taskConditions.Count; i++)
        {
            if (task.taskConditions[i].nowAmount >= task.taskConditions[i].targetAmount)
            {
                if (lastTask!=i)
                {
                    task.taskConditions[i].isFinish++;
                }
                if (task.taskConditions[i].isFinish >= task.taskConditions.Count)
                {
                    isAchieve = true;
                }
                lastTask = i;
            }
        }
    }
    void OnAchieveBtnClick()
    {
        if (isAchieve)
        {
            TTUIPage.ShowPage<TipPanel>("任务完成！");
            Destroy(gameObject);
            for (int i = 0; i < task.taskRewards.Count; i++)
            {
                Debug.Log("获得" + task.taskRewards[i].amount + "个" + task.taskRewards[i].id);
            }
            Save.SaveTask();
        }
        else
        {
            return;
        }
    }

    void OnAbondenBtnClick()
    {
        TTUIPage.ShowPage<TipPanel>("放弃任务！");
        Save.currentTaskDic.Remove(task.taskID);
        Destroy(gameObject);
    }
}
