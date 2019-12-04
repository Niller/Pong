using UnityEngine;

public class Ball
{
    private readonly float _speedIncrement;

    //TODO Use fsm instead this
    private float _postDeathFlyLength = 0.5f;
    private bool _postDeath;

    private readonly PongManager _pongManager;

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

    public float Speed
    {
        get;
        private set;
    }

    public float Size
    {
        get;
    }

    public Ball(Vector2 startPosition, float size, float speed, float speedIncrement)
    {
        Position = startPosition;
        Size = size;
        Speed = speed;
        _speedIncrement = speedIncrement;
        Direction = Random.insideUnitCircle.normalized;
        _pongManager = ServiceLocator.Get<PongManager>();
    }

    public void Update(float deltaTime)
    {
        var deltaMove = Direction * deltaTime * Speed;
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

            Speed += _speedIncrement;
            Direction = newDirection;
        }

        
    }
}