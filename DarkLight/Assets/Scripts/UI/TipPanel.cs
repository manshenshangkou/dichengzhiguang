using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class TipPanel : TTUIPage {

    public TipPanel() : base(UIType.PopUp, UIMode.NeedBack, UICollider.Normal)
    {
        uiPath = "UIPrefab/TipPanel";
    }
    public override void Awake(GameObject go)
    {
        ShopPanel.FindChildComponent<Text>(transform).text = data.ToString();
        //transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnClickBtn);

        CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
        cg.DOFade(0, 0.5f).SetDelay(0.5f).OnComplete(() => { OnClickBtn(); });

    }

    private void OnClickBtn()
    {
        GameObject.Destroy(gameObject);
    }
}
