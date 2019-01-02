using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour {


    string s = "http://pic1a.nipic.com/2009-01-07/20091713417344_2.jpg";
    // Use this for initialization
    void Start () {
        StartCoroutine(download());
	}
	
	// Update is called once per frame
	void Update () {
		 
	}

    IEnumerator download()
    {
        WWW w = new WWW(s);
        yield return w;
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.GetComponent<MeshRenderer>().material.mainTexture = w.texture;


    }
}
