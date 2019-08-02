using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_StateMachine : MonoBehaviour
{

    public Animator animator;

    void Update()
    {
        animator.SetFloat("PC_Walk", Input.GetAxis("Horizontal"));
    }
}
