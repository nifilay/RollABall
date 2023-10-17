using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
            transform.eulerAngles = Vector3.zero;
        }
    }
}
