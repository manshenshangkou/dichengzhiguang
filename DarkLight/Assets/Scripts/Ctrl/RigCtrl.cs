using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RigCtrl : MonoBehaviour {

    Rigidbody rig;
    float moveX;
    float moveZ;
    public float speed;
    public GameObject camera;
	// Use this for initialization
	void Start () {
        rig = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.forward = new Vector3(camera.transform.forward.y,0, 0);
        moveX = -Input.GetAxis("Horizontal");
        moveZ = -Input.GetAxis("Vertical");
        rig.velocity = new Vector3(moveX * speed, 0, moveZ * speed);
        //if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        //{
        //    transform.rotation *= Quaternion.Euler(0, -moveX * speed, 0);
        //}
        transform.forward += new Vector3(moveX * speed, 0, moveZ * speed);
        //Vector3 movement = transform.forward * moveZ;
        //rig.MovePosition(transform.position + movement * speed * Time.deltaTime);
        //rig.MoveRotation(transform.rotation * Quaternion.Euler(0, moveX * speed, 0));
    }
}
