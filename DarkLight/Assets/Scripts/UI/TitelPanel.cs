using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitelPanel : TTUIPage {
    public Image Title;
    public Image Anykey;
    public Image White;
    public Button LoginButton;
    public Button NewGameButton;
    public static bool AnyKeyDown = false;


    public TitelPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/TitlePanel";
    }
    public override void Awake(GameObject go)
    {
        Title = transform.Find("TitleText").GetComponent<Image>();
        //Anykey = transform.Find("Anykey").GetComponent<Image>();
        White = transform.Find("TitleBG").GetComponent<Image>();
        LoginButton = transform.Find("ButtonLoadGame").GetComponent<Button>();
        NewGameButton = transform.Find("ButtonNewGame").GetComponent<Button>();

        Title.color = new Color(1,1,1,0);
        //Anykey.color = new Color(1, 1, 1, 0);
        White.DOFade(0, 2f);
        Title.DOFade(1, 2f).SetLoops(-1).SetDelay(3.5f);
        LoginButton.gameObject.SetActive(false);
        NewGameButton.gameObject.SetActive(false);
        LoginButton.image.DOFade(1, 2f).SetDelay(4f).OnStart(()=> { LoginButton.gameObject.SetActive(true); });
        NewGameButton.image.DOFade(1, 2f).SetDelay(4f).OnStart(() => { NewGameButton.gameObject.SetActive(true); });


        LoginButton.onClick.AddListener(()=> { Tools.LoadSceneByLoading("My Dreamdev Village"); });
        NewGameButton.onClick.AddListener(() => { Tools.LoadSceneByLoading("My Character Creation"); });
        

        if (!PlayerPrefs.HasKey("SaveData"))
        {
            LoginButton.interactable = false;
        }
        //PlayerPrefs.GetString("SaveData");
    }


}
/// <summary>
/// 先加载Loading场景，然后加载目标场景
/// </summary>
public static class Tools
{
    public static void LoadSceneByLoading(string targetScene)
    {
        SceneManager.LoadScene("Loading");
        GameCtrl.Instance.nextSceneName = targetScene;
    }

}
