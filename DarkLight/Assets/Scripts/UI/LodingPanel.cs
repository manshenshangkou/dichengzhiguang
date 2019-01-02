using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;



public class LodingPanel : TTUIPage {

    public LodingPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/LodingPanel";
    }
}
