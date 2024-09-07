using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO/PoolingList")]
public class PoolingList : ScriptableObject
{
    public List<PoolingItem> poolingItems = new List<PoolingItem>();
}