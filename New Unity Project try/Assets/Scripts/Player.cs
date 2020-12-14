﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private  Transform groundCheckTransform;
    [SerializeField]  private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody ridibodyComponent;
    private int superJumpsRemaining;


    // Start is called before the first frame update
    void Start()
    {
        ridibodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    //fixedupdated its ccalled once every physics update 
     private void FixedUpdate()
    {
        ridibodyComponent.velocity = new Vector3(horizontalInput, ridibodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
      
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            ridibodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }

          private  void OnTriggerEnter(Collider other)
    {
           if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    

}
