using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    public bool Enabled
    {
        get => _enabled;
        set
        {
            _lineRenderer.enabled = value;
            _enabled = value;
        }
    }

    private bool _enabled;
    private LineRenderer _lineRenderer;
    private const int _reflectionCount = 2;
    [SerializeField] private float _findDistance = 50f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void StopCalculateLine()
    {
        _lineRenderer.positionCount = 0;

        Enabled = false;
    }

    public void CalculateLine(Vector3 startPos, Vector2 direction)
    {        
        Enabled = true;

        Debug.Log("CalculateLine");
        
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0,startPos);

        for(int i = 0; i < _reflectionCount; i++)
        {
            RaycastHit2D hit = GetReflectionRayCast(startPos,direction);

            AddVertexLine(hit.point);

            startPos = hit.point - direction * 0.01f;
            direction = Vector2.Reflect(direction, hit.normal).normalized;
        }
    }

    private bool AddVertexLine(Vector3 newPos)
    {
        int positionCount  = _lineRenderer.positionCount;
        _lineRenderer.positionCount = _lineRenderer.positionCount + 1;
        _lineRenderer.SetPosition(positionCount,newPos);

        return true;
    }

    private RaycastHit2D GetReflectionRayCast(Vector3 startPos,Vector3 direction)
    {
        return Physics2D.Raycast(startPos,direction,_findDistance);
    }
}
