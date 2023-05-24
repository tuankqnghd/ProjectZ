using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Character_Animation_Controller : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Use GetComponentInChildren() to get the Animator component attached to the same GameObject or any of its child GameObjects
    }

    public void switchMoving(bool switchMove) {
        Debug.Log(switchMove);
        animator.SetBool("isWalking", switchMove);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
