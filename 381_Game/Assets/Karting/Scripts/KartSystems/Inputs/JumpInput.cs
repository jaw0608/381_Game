using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInput : MonoBehaviour
{
    public bool isJumping;
    public Vector3 jump;
    public float jumpForce = 10.0f;
    public bool isGrounded;
    public float distGround = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 10.0f, 0.0f);
    }
    void GroundCheck(){
        if(Physics.Raycast(transform.position, Vector3.down, distGround))
        {
            //Debug.Log("grounded");
            isGrounded = true;
        }
        else
        {
            Debug.Log("not grounded");
            isGrounded = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isJumping)
        {
            Debug.Log("Space key was pressed.");
            rb.velocity += (Vector3.up * jumpForce);
            isJumping = false;
        }
        GroundCheck();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }  
    }
}
