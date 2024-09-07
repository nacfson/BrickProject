using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _standardTrm;
    public BoxCollider2D DeadZoneCollider { get; private set; }
    
    private void Start()
    {
        Vector3 standardPos = _standardTrm.position;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Brick brick = PoolManager.SInstance.Pop("Brick") as Brick;
                brick.transform.position = standardPos + new Vector3(i,j,0);
                brick.gameObject.SetActive(true);
            }
        }
        DeadZoneCollider = transform.Find("DeadZone").GetComponent<BoxCollider2D>();
    }
}
