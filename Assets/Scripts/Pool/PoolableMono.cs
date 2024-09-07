using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour,IPoolable
{
    public int poolingCnt;
    public abstract void Setting();
    public abstract void Push();
    public abstract void Pop();
}
