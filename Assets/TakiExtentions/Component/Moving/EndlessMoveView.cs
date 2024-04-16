using UnityEngine;


/// <summary>
/// リサージュ曲線を描くように、Update関数で動かす。
/// </summary>
public class EndlessMoveView : MonoBehaviour
{

    [SerializeField] float angleSpeed;
    [SerializeField] float xSeata;
    [SerializeField] float ySeata;
    [SerializeField,Tooltip("振幅")] float amplitude;
    float angle;

    Transform _Transform;
    private void Awake()
    {
        _Transform = transform;
    }

    private void Update()
    {
        angle += Time.deltaTime * angleSpeed * Mathf.Deg2Rad;

        _Transform.position = new Vector3(Mathf.Cos(angle*xSeata), Mathf.Cos(angle * ySeata), 0)* amplitude;

    }


}