using Game;
using UnityEngine;

public class MotorComponentView : MonoBehaviour
{
    [SerializeField] private MotorComponent _motorComponent;

    [SerializeField] ShipViewConfig _viewConfig;

    [Header("Visual")] [SerializeField] private Renderer _renderer;

    private Material _material;

    private Transform _viewTransform;

    private void OnEnable()
    {
        _motorComponent.OnMoved += this.OnMoved;
    }

    private void OnDisable()
    {
        _motorComponent.OnMoved -= this.OnMoved;
    }


    public void Awake()
    {
        _material = new Material(_viewConfig.MaterialPrefab);
        _renderer.material = _material;
        _viewTransform = this.GetComponent<Transform>();
    }

    public void OnMoved(Vector3 moveDirection)
    {
        Vector3 shipAngles = _viewTransform.localEulerAngles;
        shipAngles.x = _viewConfig.MoveRotationAngle * moveDirection.y;
        shipAngles.y = _viewConfig.MoveRotationAngle / 2 * moveDirection.x * -1f;

        Quaternion shipRotation = Quaternion.Euler(shipAngles);
        float t = _viewConfig.MoveSpeed * Time.fixedDeltaTime;
        _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
    }
}