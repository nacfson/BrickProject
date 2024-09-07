using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : Singleton<PoolManager>
{
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
    [field:SerializeField] public string FolderPath { get; private set; }= "Assets/ScriptableObjects/PoolingList/";

    [field:SerializeField] [HideInInspector] public PoolingList PoolingList { get; set; }


    private void Awake()
    {
        Setting();
    }
    public void Setting()
    {
        Debug.Log($"Setting: {PoolingList.poolingItems.Count}");
        foreach (var pair in PoolingList.poolingItems)
        {
            CreatePool(pair.prefab, transform, pair.cnt);
        }
    }
    
    private void CreatePool(PoolableMono prefab, Transform parent, int cnt)
    {
        if (_pools.ContainsKey(prefab.name))
        {
            return;
        }
            
        _pools.Add(prefab.name, new Pool(prefab, parent, cnt));
    }
    
    public PoolableMono Pop(string key)
    {
        if (_pools.TryGetValue(key, out var pool))
        {
            return pool.Pop();
        }
            
        Debug.LogError($"[PoolManager] Doesn't exist key on pools : [{key}]");
        return null;
    }
    
    public void Push(PoolableMono obj)
    {
        if (obj == null)
        {
            return;
        }
            
        _pools[obj.name].Push(obj);
    }
}