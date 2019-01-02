using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CreatPlayerPanel : TTUIPage {

    public Button ButtonNext;//下一个
    public Button ButtonPrev;//上一个
    public Button ButtonOk;//确定按钮
    public Button ButtonRandom;//骰子
    public InputField InputFieldName;//名字输入框



    public GameObject[] hero;  //预制体数组
    public int indexHero = 0;  //index select hero
    private GameObject[] heroInstance; //加载到场景里的数组
    string[] xings = { "赵", "钱", "孙", "李", "周", "吴", "郑", "王" };
    string[] names = { "潇晗", "昭樨", "宇航", "宇浩", "嘉熙", "嘉懿", "晗晗", "熙熙", "然然", "诺诺", "赢赢", "涵涵", "奥然", "浅秋", "思璇", "宇熙", "姿诺", "雨欣" };

    int rotationNum = 1;

    public CreatPlayerPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/CreatPlayerPanel";
    }
    public override void Awake(GameObject go)
    {
        ButtonNext = transform.Find("ButtonNext").GetComponent<Button>();
        ButtonPrev = transform.Find("ButtonPrev").GetComponent<Button>();
        ButtonOk = transform.Find("ButtonOk").GetComponent<Button>();
        ButtonRandom = transform.Find("ButtonRandom").GetComponent<Button>();
        InputFieldName = transform.Find("InputFieldName").GetComponent<InputField>();

        ButtonNext.onClick.AddListener(() =>
        {
            indexHero++;
            if (indexHero>=heroInstance.Length)
            {
                indexHero = 0;
            }
            UpdateHero(indexHero);
        });
        ButtonPrev.onClick.AddListener(() =>
        {
            indexHero--;
            if (indexHero < 0)
            {
                indexHero = heroInstance.Length-1;
            }
            UpdateHero(indexHero);
        });
        ButtonRandom.onClick.AddListener(GetRandomName);
        ButtonOk.onClick.AddListener(OnOkClick);
        hero = Resources.LoadAll<GameObject>("Player/HeroPreview");//加载指定路径下的所有GameObject
        heroInstance = new GameObject[hero.Length]; //add array size equal hero size
        indexHero = 0; //set default selected hero
        SpawnHero(); //spawn hero to display current selected


        //check if hero is less than 1 , button next and prev will disappear
        if (hero.Length <= 1)
        {
            ButtonNext.gameObject.SetActive(false);
            ButtonPrev.gameObject.SetActive(false);
        }


    }
    /// <summary>
    /// 显示指定索引所对应的角色
    /// </summary>
    /// <param name="_indexHero"></param>
    public void UpdateHero(int _indexHero)
    {
        for (int i = 0; i < hero.Length; i++)
        {
            //Show only select character
            if (i == _indexHero)
            {
                heroInstance[i].SetActive(true);
            }
            else
            {
                //Hide Other Character
                heroInstance[i].SetActive(false);
            }
        }
    }

    //加载预制体到场景
    public void SpawnHero()
    {
        for (int i = 0; i < hero.Length; i++)
        {
            heroInstance[i] = (GameObject)GameObject.Instantiate(hero[i], transform.position, Quaternion.Euler(0,180,0));
        }

        UpdateHero(indexHero);
    }
    /// <summary>
    /// 随机姓名
    /// </summary>
    public void GetRandomName()
    {
        switch (rotationNum%2)
        {
            case 0:
                ButtonRandom.transform.DORotate(Vector3.forward * 180 * 2, 0.5f);
                break;
            case 1:
                ButtonRandom.transform.DORotate(Vector3.forward * 180, 0.5f);
                break;
        }
        
        rotationNum++;
        string xing = xings[Random.Range(0, xings.Length - 1)];
        string name = names[Random.Range(0, names.Length - 1)];
        InputFieldName.text = xing + name;
    }
    public void OnOkClick()
    {
        PlayerPrefs.SetString("pName", InputFieldName.text);
        PlayerPrefs.SetInt("pSelect", indexHero);

        Tools.LoadSceneByLoading("My Dreamdev Village");
    }

}
