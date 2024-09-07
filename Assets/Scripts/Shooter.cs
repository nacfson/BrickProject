using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Shooter : PoolableMono
{
    public GameController Controller {get; set;}

    public bool IsShot {get; private set;}

    [SerializeField] private float _yOffset;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _ballCreationInterval = 0.1f;

    private WaitForSeconds _ballWfs;
    private Coroutine _creationCoroutine;

    private int _ballCnt;
    private int _checkBallCnt;

    //땅에 먼저 닿는 공이 큐에 첫 번째에 위치하게 됨
    private Queue<Ball> _ballQueue;
    private Ball _firstQueueBall;

    private GuideLine _guideLine;

    private void Awake()
    {
        Setting();
    }    

    public override void Setting()
    {
        _ballQueue = new Queue<Ball>();
        _firstQueueBall = null;
        _checkBallCnt = 0;
        _ballCnt = 1;

        _ballWfs = new WaitForSeconds(_ballCreationInterval);

        _guideLine = GetComponentInChildren<GuideLine>();
    }

    public void BallOnTheFloor(Ball ball)
    {
        if(_firstQueueBall == null)
        {
            _firstQueueBall = ball;
        }

        _ballQueue.Enqueue(ball);
        _checkBallCnt++;

        if(CheckRoundEnd())
        {
            Debug.Log("RoundEnd");
            foreach(Ball queueBall in _ballQueue)
            {
                queueBall.MoveToDestination(_firstQueueBall.transform.position);
            }
            _ballCnt++;

            _checkBallCnt = 0;
            IsShot = false;
        }
    }

    private bool CheckRoundEnd()
    {
        return _checkBallCnt == _ballCnt;
    }

    private void Update()
    {
        if(!IsShot)
        {
            //_guideLine.CalculateLine(GetShooterPos(), GetMouseDirection());
            
            if (Input.GetMouseButtonDown(0))
            {
                //_guideLine.StopCalculateLine();

                if(_creationCoroutine != null)
                {
                    StopCoroutine(_creationCoroutine);
                }
                _creationCoroutine = StartCoroutine(BallCreateCoroutine(_ballCnt));
                IsShot = true;
            }
        }
    }

    private IEnumerator BallCreateCoroutine(int ballCnt = 1)
    {
        Ball ball;
        Vector3 spawnPos;
        
        if(_firstQueueBall != null)
        {
            spawnPos = _firstQueueBall.transform.position;
        }
        else
        {
            spawnPos = transform.position;
        }


        Vector3 direction = GetMouseDirection();

        for (int i = 0; i < ballCnt; i++)
        {
            bool tryDequeue = _ballQueue.TryDequeue(out ball);
            
            if(!tryDequeue)
                ball = PoolManager.SInstance.Pop("Ball") as Ball;


            ball.Parent = this;
            ball.Speed = _ballSpeed;
            ball.SetDirection(direction);
            ball.Enabled = true;
            ball.transform.position = spawnPos;
            yield return _ballWfs;
        }
    }

    private Vector3 GetMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float radian = GetDirectionRadian(GetShooterPos()   ,mousePos);
        return new Vector3(Mathf.Cos(radian),Mathf.Sin(radian),0);
    }
    private Vector3 GetShooterPos()
    {
        if(_firstQueueBall != null)
            return _firstQueueBall.transform.position;
        return transform.position;
    }

    private float GetDirectionRadian(Vector3 originPos, Vector3 mousePos)
    {
        return Mathf.Atan2(mousePos.y - originPos.y,
                            mousePos.x - originPos.x);
    }

    public override void Pop()
    {
    }

    public override void Push()
    {

    }
}
