
public class Bat
{
    private readonly Pitch _pitch;

    public float Position;

    private float _sign = 1;

    public Bat(Pitch pitch)
    {
        _pitch = pitch;
        Position = _pitch.BatStartPosition;
    }

    public void Update(float deltaTime)
    {
        Position += deltaTime * _sign;
        if (Position >= _pitch.Size.x || Position <= 0)
        {
            _sign *= -1;
        }
    }
}