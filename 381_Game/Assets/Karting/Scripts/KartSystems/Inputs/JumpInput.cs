using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JumpInput : MonoBehaviour
{
    public bool gravitySwitch = false;
    public bool gravityOn = true;
    public bool isJumping;
    public Vector3 jump;
    public float jumpForce = 10.0f;
    public bool isGrounded;
    public float distGround = 1f;
    Rigidbody rb;
    public CinemachineVirtualCamera cam;
    private Vector3 StartPosition;
    private Vector3 FlipPosition;
    private Vector3 destPosition;

    public bool GravitySwitchLevel = false;

    // Start is called before the first frame update
    
    void Start()
    {
        // cam = GetComponent<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 10.0f, 0.0f);
        // StartPosition = cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        // FlipPosition = StartPosition;
        // FlipPosition.y = -1 * FlipPosition.y;
        // destPosition = StartPosition;
    }

    // void SetPosition(Vector3 position)
    // {
    //     cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = position;
    // }

    // public Vector3 GetPosition()
    // {
    //     return cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    // }

    void GroundCheck(){
        if(gravityOn)
        {
            // destPosition = StartPosition;

            if(Physics.Raycast(transform.position, Vector3.down, distGround))
            {
                //Debug.Log("grounded");
                isGrounded = true;
            }
            else
            {
                //Debug.Log("not grounded");
                isGrounded = false;
            }
        }
        else if(GravitySwitchLevel==true)
        {
            //Debug.Log("upside down");
            if (Physics.Raycast(transform.position, Vector3.up, distGround))
            {
                // destPosition = FlipPosition;
                //Debug.Log("grounded");
                isGrounded = true;
            }
            else
            {
                // destPosition = StartPosition;
                //Debug.Log("not grounded");
                isGrounded = false;    
            }
        }
    }
    void reversedGroundCheck(){
        if(Physics.Raycast(transform.position, Vector3.up, distGround))
        {
            // destPosition = FlipPosition;
            //Debug.Log("grounded");
            isGrounded = true;
        }
        else
        {
            //Debug.Log("not grounded");
            isGrounded = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(gravitySwitch && GravitySwitchLevel==true)
        {
            this.gameObject.transform.Rotate(0, 0, 180);
            Physics.gravity *= -1;
            gravitySwitch = false;
            rb.velocity = new Vector3(0, 0, 0);
            //cam.transform.rotation *= Quaternion.Euler(0, 180, 0);
            
        }
        if(isJumping)
        {
            if(gravityOn)
            {
                Debug.Log("regular jump");
                rb.velocity += (Vector3.up * jumpForce);
                isJumping = false;
            }
            else if (GravitySwitchLevel==true)
            {
                Debug.Log("upside down jump");
                rb.velocity += (Vector3.down * jumpForce);
                isJumping = false;
            }
        }
        GroundCheck();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }
        if(Input.GetKeyDown(KeyCode.Return) && GravitySwitchLevel==true)
        {
            gravitySwitch = true;
            gravityOn = !gravityOn;
        }

        // Vector3 pos = Vector3.MoveTowards(destPosition, GetPosition(), Time.deltaTime/100);
        // SetPosition(pos);
    }
}
