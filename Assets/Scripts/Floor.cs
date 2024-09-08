using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;



    private void Start()
    {
        Ball ball = PoolManager.SInstance.Pop("Ball") as Ball;
        ball.Setting();
        ball.SetDirection(Vector3.up);
        ball.transform.position = transform.position + Vector3.up;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float targetPos = Mathf.Clamp(mousePos.x,_xMin,_xMax);
        transform.position = new Vector3(targetPos , transform.position.y);
    }
}
