using UnityEngine;
using System;

[Serializable]
public struct PoolingItem
{
    public PoolableMono prefab;
    public int cnt;
}