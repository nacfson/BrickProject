using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(PoolManager))]
public class PoolManagerEditor : Editor
{
    public PoolManager manager;


    private void OnEnable()
    {
        manager = target as PoolManager;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reload"))
        {
            Debug.Log("Reload");
            LoadPoolingList();
        }
    }

    private void LoadPoolingList()
    {
        PoolingList poolingList = ScriptableObject.CreateInstance<PoolingList>();
        
        string assetPath = manager.FolderPath + "PoolingList.asset";
        if (!AssetDatabase.IsValidFolder(manager.FolderPath))
        {
            PoolingList originList = AssetDatabase.LoadAssetAtPath<PoolingList>(assetPath);
            if(originList != null)
            {
                poolingList = originList;
            }
        }


        AssetDatabase.CreateAsset(poolingList, assetPath);

        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                if(prefab.TryGetComponent(out PoolableMono poolableMono))
                {
                    if (poolableMono.poolingCnt <= 0) continue; 
                    PoolingItem poolingItem = new PoolingItem { prefab = poolableMono, cnt =  poolableMono.poolingCnt};
                    poolingList.poolingItems.Add(poolingItem);
                }
            }
        }

        manager.PoolingList = poolingList;
        EditorUtility.SetDirty(poolingList);
    }
}
#endif