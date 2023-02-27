using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLogic : MonoBehaviour
{
    Rigidbody m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidBody.AddForce(new Vector3(0, 1000, 0));
        }
    }
}
