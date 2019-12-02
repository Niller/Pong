using UnityEngine;

public class Ball
{
    public Vector2 Position;
    private Vector2 _direction;
    private float _speed = 0.7f;

    public Ball(Vector2 startPosition)
    {
        Position = startPosition;
        _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
    }

    public void Update(float deltaTime)
    {
        Position += _direction * deltaTime * _speed;


        //TODO refactor

        if (Position.y >= 1 || Position.y <= -1)
        {
            _direction = new Vector2(-_direction.x, _direction.y);
        }

        if (Position.x >= 1 || Position.x <= -1)
        {
            _direction = new Vector2(_direction.x, -_direction.y);
        }
    }
}