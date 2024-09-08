using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private void Awake()
    {
        Brick[] brcks = transform.GetComponentsInChildren<Brick>();

        foreach (Brick brck in brcks)
        {
            brck.Setting();
        }
    }
}
