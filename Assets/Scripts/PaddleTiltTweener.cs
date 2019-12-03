using System.Collections;
using UnityEngine;

public class PaddleTiltTweener : MonoBehaviour
{
    [SerializeField]
    private float _maxAngle;
    [SerializeField]
    private float _time;
    [SerializeField]
    private AnimationCurve _curve;

    private Coroutine _currentCoroutine;
    private Transform _transform;
    private float _currentTime;
    private void Awake()
    {
        _transform = transform;
    }

    public void Run(float force)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentTime = 0;
        _currentCoroutine = StartCoroutine(TweenCoroutine(force));
    }

    private IEnumerator TweenCoroutine(float force)
    {
        while (_currentTime < _time)
        {
            _transform.localRotation = Quaternion.Euler(
                _transform.eulerAngles.x,
                _transform.eulerAngles.y,
                -_curve.Evaluate(_currentTime/_time) * _maxAngle * force);

            _currentTime += Time.deltaTime;
            yield return null;
        }

        _transform.localRotation = Quaternion.Euler(
            _transform.eulerAngles.x,
            _transform.eulerAngles.y,
            0);

        _currentCoroutine = null;

    }
}