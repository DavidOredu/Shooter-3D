using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public ForceMode forceMode = ForceMode.VelocityChange;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(new Vector3(-moveSpeed, 0, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(new Vector3(moveSpeed, 0, 0));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(0, moveSpeed,0));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(0, -moveSpeed,0));
        }
    }
    private void Move(Vector3 motion)
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = right.normalized * motion.x + forward.normalized * motion.y;
        rb.AddForce(movement * Time.deltaTime, forceMode);
    }
}
