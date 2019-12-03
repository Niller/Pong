using UnityEngine;

public class Ball
{
    public Vector2 Position
    {
        get;
        private set;
    }

    public Vector2 Direction
    {
        get;
        private set;
    }

    private float _speed = 0.7f;
    private readonly PongManager _pongManager;

    private float _postDeathFlyLength = 0.5f;
    private bool _postDeath;

    public float Size;

    public Ball(Vector2 startPosition)
    {
        Position = startPosition;
        //_direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        Size = 0.03f;
        Direction = Random.insideUnitCircle.normalized;
        _pongManager = ServiceLocator.Get<PongManager>();
    }

    public void Update(float deltaTime)
    {
        var deltaMove = Direction * deltaTime * _speed;
        Position += deltaMove;

        if (_postDeath)
        {
            _postDeathFlyLength -= deltaMove.magnitude;

            if (_postDeathFlyLength < 0)
            {
                _pongManager.DespawnBall();
            }
            return;
        }

        //Wall collision
        if (Position.x >= 1 - Size || Position.x <= -1 + Size)
        {
            Direction = new Vector2(-Direction.x, Direction.y);
        }

        //Paddle collision
        if (Position.y >= 1 - Size || Position.y <= -1 + Size)
        {
            if (!_pongManager.CheckCollision(this, out var newDirection))
            {
                _postDeath = true;
            }

            Direction = newDirection;
        }

        
    }
}