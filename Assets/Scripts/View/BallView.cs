using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class BallView : PongObjectView
{
    private Ball _ball;


#pragma warning disable 649
    [SerializeField]
    private GameObject _view;
#pragma warning restore 649

    private Vector3 _rotationSpeed;
    private Transform _viewTransform;

    private readonly int _shaderColor1PropertyId = Shader.PropertyToID("_Color1");
    private readonly int _shaderColor2PropertyId = Shader.PropertyToID("_Color2");

    public void Initialize(Ball ball, Vector2 pitchSize)
    {
        base.Initialize(pitchSize, ball);
        _ball = ball;

        _viewTransform = _view.transform;
        var radius = _ball.Size * pitchSize.y;
        _viewTransform.localScale = new Vector3(radius, radius, radius);

        var settingsManager = ServiceLocator.Get<SettingsManager>();

        var material = _view.GetComponent<Renderer>().sharedMaterial;
        var ballColors = settingsManager.LoadBallColor();
        material.SetColor(_shaderColor1PropertyId, settingsManager.Config.BallColors[ballColors.Item1].Color);
        material.SetColor(_shaderColor2PropertyId, settingsManager.Config.BallColors[ballColors.Item2].Color);

        SignalBus.Subscribe<BallHitSignal>(OnBallHit);
        SignalBus.Subscribe<BallPositionChangedSignal>(OnPositionChanged);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<BallHitSignal>(OnBallHit);
        SignalBus.Unsubscribe<BallPositionChangedSignal>(OnPositionChanged);
    }

    private void OnPositionChanged(BallPositionChangedSignal data)
    {
        NextPosition = new Vector2(
            _ball.Position.x * (PitchSize.x / 2f),
            _ball.Position.y * (PitchSize.y / 2f));
    }

    private void OnBallHit(BallHitSignal data)
    {
        _rotationSpeed = new Vector3(Random.value, Random.value, Random.value) * Random.Range(2, 5);
    }

    protected override void Update()
    {
        base.Update();
        _viewTransform.Rotate(_rotationSpeed);
    }
}