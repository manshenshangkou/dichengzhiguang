using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class LoadingPanel : TTUIPage {

    public Slider sliderLoading;
    public Text progressText;


    public LoadingPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.Normal)
    {
        uiPath = "UIPrefab/Loading";
    }

    public override void Awake(GameObject go)
    {
        sliderLoading = transform.Find("").GetComponent<Slider>();
        progressText = transform.Find("").GetComponent<Text>();
    }
}
