using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public void Push();
    public void Pop();
    public void Setting();
}
