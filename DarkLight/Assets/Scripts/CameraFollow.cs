using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float distance;
    Vector3 offset;
    Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.forward * distance;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, player.position - offset,0.5f);
	}
}
