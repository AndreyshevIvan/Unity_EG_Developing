using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelController : MonoBehaviour
{

    public void SetSpawnLevel(int level)
    {
        PlayerPrefs.SetInt("SpawnLevel", level);
        PlayerPrefs.Save();
    }
}
