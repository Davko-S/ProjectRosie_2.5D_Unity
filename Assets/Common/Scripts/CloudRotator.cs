using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotator : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, turnSpeed * Time.fixedDeltaTime);
    }
}
