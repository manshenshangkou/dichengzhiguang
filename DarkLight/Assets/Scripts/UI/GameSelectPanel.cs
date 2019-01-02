using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;

public class GameSelectPanel : TTUIPage {

    public GameSelectPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/GameSelectPanel";
    }
}
