using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterControllerCtrl : MonoBehaviour {

    CharacterController m_characterController;
    private float moveX;
    private float moveZ;
    public float speed = 5;
    public GameObject effect1;
    public GameObject effect2;

    Animator m_animator;

    Collider[] enemy;
    // Use this for initialization
    void Start () {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        moveX = -Input.GetAxis("Horizontal");
        moveZ = -Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        if (moveX!=0||moveZ!=0)
        {
            m_animator.SetBool("isWalk", true);
        }
        else
        {
            m_animator.SetBool("isWalk", false);
        }
        m_characterController.SimpleMove(new Vector3(moveX * speed, 0, moveZ * speed));
        transform.rotation *= Quaternion.Euler(0, -moveX * speed, 0);
        //transform.Rotate(new Vector3(0, moveX * speed, 0));

        Quaternion rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            transform.forward -= rotation * movement * speed;
        }
        //m_characterController.SimpleMove(rotation*movement*speed);
        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_animator.SetTrigger("Skill1");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            m_animator.SetTrigger("Skill2");
        }
    }
    void MySkill1()
    {
        Instantiate(effect1, transform.position, transform.rotation);
        Camera.main.DOShakePosition(0.2f);
    }
    void MySkill2()
    {
        speed = 0;
        Instantiate(effect2,transform.position+ transform.forward*10, Quaternion.identity);
        Camera.main.DOShakePosition(0.2f);
        enemy = Physics.OverlapSphere(transform.position,20,LayerMask.GetMask("Enemy"));
        foreach (var item in enemy)
        {
            Destroy(item.gameObject);
        }
    }

}
