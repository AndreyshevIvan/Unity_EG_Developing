using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide trigger");
    }
}