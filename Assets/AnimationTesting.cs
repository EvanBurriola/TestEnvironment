using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTesting : MonoBehaviour
{
    
    
    Animator animator;
    private float speed = 1;
    private bool isUp = true;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed == 0 || speed == 200) isUp = !isUp;

        if (isUp) speed+=.5f;
        else speed-=.5f;


        
        animator.SetFloat("CharSpeed", speed);
        Debug.Log(speed);
    }
}
