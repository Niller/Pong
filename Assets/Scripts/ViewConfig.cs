using UnityEngine;

[CreateAssetMenu]
public class ViewConfig : ScriptableObject
{
    public Vector2 PitchSize;
    public float BottomBatPosition;
    public float TopBatPosition;

    public GameObject BatView;
    public GameObject BallView;
}