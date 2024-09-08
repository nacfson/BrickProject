using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : PoolableMono
{
    public override void Pop()
    {
    }

    public override void Push()
    {
    }

    public override void Setting()
    {
        Brick[] brcks = transform.GetComponentsInChildren<Brick>();

        foreach (Brick brck in brcks)
        {
            brck.Setting();
        }
    }
}
