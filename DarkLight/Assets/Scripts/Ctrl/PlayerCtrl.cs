using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    RaycastHit hit;
    GameObject go;
    public float speed = 0;
    bool isWalk = false;
    CharacterController m_characterController;
    // Use this for initialization
    void Start () {
        m_characterController = gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            speed = 10;
            //把我鼠标点击的位置用一个射线接收到
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction);

            //射线检测的方法 （射线，检测到的物体，射线的长度，射线只能碰到那些层（返回值是一个Int索引））
            if (Physics.Raycast(ray, out hit,1000,LayerMask.GetMask("Ground")))
            {
                Instantiate(GameSetting.Instance.mousefxNormal, hit.point, Quaternion.identity);
            }
            Vector3 TargetPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(TargetPoint);
            isWalk = true;
        }
       
        if (isWalk)
        {
            m_characterController.SimpleMove(transform.forward * speed);
            if (Vector3.Distance(transform.position, hit.point) < 0.5f)
            {
                isWalk = false;
            }
        }

    }
}
