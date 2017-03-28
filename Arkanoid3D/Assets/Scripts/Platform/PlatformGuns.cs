using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGuns : MonoBehaviour
{
    public void Fire()
    {
        GetComponent<Animation>().Play();
    }
}