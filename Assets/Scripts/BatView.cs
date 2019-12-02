using UnityEngine;

public class BatView : MonoBehaviour
{
    private Bat _bat;
    private Transform _transform;

    public void Initialize(Bat bat)
    {
        _bat = bat;
        _transform = transform;
    }

    private void Update()
    {
        _transform.localPosition = new Vector3(_bat.Position, _transform.localPosition.y, _transform.localPosition.z);
    }
}