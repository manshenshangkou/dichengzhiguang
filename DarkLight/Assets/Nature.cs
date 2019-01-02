using UnityEngine;
using System.Collections;
/// <summary>
/// 属性
/// </summary>
public class Nature : MonoBehaviour {
    private static Nature nature;
    public static Nature Instance()
    {
        if (nature == null)
        {
            nature = new Nature();
            return nature;
        }
        return nature;
    }
    public int Hp, MaxHp, Attack, Speed;
    private void Awake()
    {
        nature = this;
    }
    // Use this for initialization
    void Start () {
        AssigNature();// 给属性赋值
    }
    /// <summary>
    /// 给属性赋值  assig分配，任务，作业，功课
    /// </summary>
    void AssigNature()
    {    
      //  for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        {
            //Hp = Save.userModelList[0].Hp;
            //MaxHp = Save.userModelList[0].MaxHp;
            //Attack = Save.userModelList[0].Attack;
            //Speed = Save.userModelList[0].Speed;
        }
    }
    /// <summary>
    /// 吃药的方法
    /// 使用物品后 属性改变
    /// </summary>
	public  void Eat()
    {
       // for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        {
            Save.userModelList[0].Hp = Hp ;
            Save.userModelList[0].MaxHp= MaxHp;
            Save.userModelList[0].Attack = Attack;
            Save.userModelList[0].Speed= Speed;
        }
    }
}
