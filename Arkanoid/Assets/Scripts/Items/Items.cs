using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{

    public virtual void SetActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}