using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _standardTrm;
    public BoxCollider2D DeadZoneCollider { get; private set; }
    private int _mapIndex = 0;
    private Map _currentMap;

    public void RestartGame()
    {
        PoolManager.SInstance.Push(_currentMap);

        CreateMap(_mapIndex);
    }
    
    private void Start()
    {
        CreateMap(_mapIndex);
        DeadZoneCollider = transform.Find("DeadZone").GetComponent<BoxCollider2D>();
    }

    private void CreateMap(int mapIndex)
    {
        _currentMap = PoolManager.SInstance.Pop($"Map{mapIndex}") as Map;
        _currentMap.transform.position = _standardTrm.position;
        _currentMap.Setting();
    }
}
