using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;

public class Ball : PoolableMono
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius;
    private Shooter _shooter;
    [field:SerializeField] public float Speed {get; set;}
    public int BounceCount { get; private set; } = 0;
    public bool Enabled { get; set; } = true;
    public Shooter Parent {get; set;}
    private Vector3 _progressDirection;


    public override void Setting()
    {

    }

    public override void Push()
    {
    }

    public void SetDirection(Vector3 direction)
    {
        _progressDirection = direction.normalized;
    }

    private void Update()
    {
        if (!Enabled) return;
        transform.position += _progressDirection * (Speed * Time.deltaTime);

        RaycastHit2D wallHit = CheckBallHit();
        if (wallHit.collider != null)
        {
            if (wallHit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damaged();
            }
            var reflectValue = Vector3.Reflect(_progressDirection,wallHit.normal);
            SetDirection(reflectValue);
            BounceCount++;
        }

        if(CheckIsDie())
        {
            Enabled = false;
            Parent.BallOnTheFloor(this);
        }
    }

    public void MoveToDestination(Vector3 pos)
    {
        transform.DOMove(pos,1f).SetEase(Ease.InCubic);
    }

    private bool CheckIsDie()
    {
        float yOffset = 5f;
        return transform.position.y <= -yOffset && BounceCount > 0;
    }

    private RaycastHit2D CheckBallHit()
    {
        return Physics2D.Raycast(transform.position,_progressDirection ,_radius,_layerMask);
    }
    public override void Pop()
    {
        BounceCount = 0;
    }


}
