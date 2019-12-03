using UnityEngine;

public class Ball
{
    public Vector2 Position;
    private Vector2 _direction;
    private float _speed = 0.7f;

    public float Size;

    public Ball(Vector2 startPosition)
    {
        Position = startPosition;
        //_direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        Size = 0.03f;
        _direction = Random.insideUnitCircle.normalized;
    }

    public void Update(float deltaTime)
    {
        Position += _direction * deltaTime * _speed;


        //TODO refactor

        if (Position.y >= 1 - Size || Position.y <= -1 + Size)
        {
            _direction = new Vector2(_direction.x, -_direction.y);
        }

        if (Position.x >= 1 - Size || Position.x <= -1 + Size)
        {
            _direction = new Vector2(-_direction.x, _direction.y);
        }
    }
}