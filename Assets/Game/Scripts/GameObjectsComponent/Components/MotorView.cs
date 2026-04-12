using Game;
using UnityEngine;

public class MotorView : MonoBehaviour
{
    [SerializeField] private Motor _motor;
    [SerializeField] ShipControllerViewConfig _viewConfig;
    [Header("Visual")]
    [SerializeField]
    private Renderer _renderer;

    private Material _material;
    private Transform _viewTransform;
    private void OnEnable()
    {

        _motor.OnMoved += this.OnMoved;
    }
    private void OnDisable()
    {
        _motor.OnMoved -= this.OnMoved;
    }

    public void VFXIntitiator(ParticleSystem prefab, ShipControllerViewConfig _viewConfig)
    {

        prefab = _viewConfig.DestroyEffectPrefab;

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
