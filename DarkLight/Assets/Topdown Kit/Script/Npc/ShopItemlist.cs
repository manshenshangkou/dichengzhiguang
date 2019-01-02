/// <summary>
/// Npc shop.
/// This script use to create a shop to sell item
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ShopItemlist : MonoBehaviour {

    public static event Action<bool,List<int>> OnNPCTrriger; 

	public List<int> itemID = new List<int>();

    void Start()
	{
		if(this.gameObject.tag == "Untagged")
			this.gameObject.tag = "Npc_Shop";
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //ShowShopBtn.gameObject.SetActive(true);
            if (OnNPCTrriger != null)
            {
                OnNPCTrriger(true, itemID);
            }
            //ShowShopBtn.onClick.AddListener();
            //ÉÌµê´ò¿ª
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ShowShopBtn.gameObject.SetActive(false);
            if (OnNPCTrriger != null)
            {
                OnNPCTrriger(false, null);
            }
        }
    }
}


