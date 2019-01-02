using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 磕头 : MonoBehaviour {
    bool isTaitou;
    Vector3 taitoudian;
    Vector3 ditoudian;
	// Use this for initialization
	void Start () {
        taitoudian = new Vector3(0,0.19f,0);
        taitoudian = new Vector3(0, 0.41f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position,ditoudian)<0.1)
        {
            Taitou();
        }
        if (Vector3.Distance(transform.position, taitoudian) < 0.1)
        {
            Ditou();
        }
    }
    void Taitou()
    {
        transform.position += new Vector3(0, 0.01f, 0); 
    }
    void Ditou()
    {
        transform.position -= new Vector3(0, 0.01f, 0); 
    }
  
}
