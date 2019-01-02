using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCtrl : MonoBehaviour {
    Animator m_Animator;
    bool isActive = true; 
    public Transform lookObj;
    public Transform rightHandObj;
    public Transform leftHandObj;
    public Transform rightFootObj;
    public Transform leftFootObj;
	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        OnAnimatorIK();

    }

    void OnAnimatorIK()
    {
        if (m_Animator)
        {
            if (isActive)
            {
                if (lookObj)
                {
                    m_Animator.SetLookAtWeight(1);
                    m_Animator.SetLookAtPosition(lookObj.position);
                }
                if (rightHandObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                if (leftHandObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }
                if (rightFootObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
                }
                if (leftFootObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
                }


            }
            else
            {
                m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                m_Animator.SetLookAtWeight(0);
            }
        }
    }
}
