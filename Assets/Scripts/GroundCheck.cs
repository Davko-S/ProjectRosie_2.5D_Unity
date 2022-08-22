using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float distanceToCheck = 0.5f;
    public bool isGrounded;
    
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToCheck))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
