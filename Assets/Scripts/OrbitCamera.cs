using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;

    void Start()
    {
        _rotY = this.transform.eulerAngles.y;
        _offset = target.position - this.transform.position;
    }

    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");

        if (0 != horInput)
        {
            _rotY = _rotY + horInput * rotSpeed;
        }
        else
        {
            _rotY = _rotY + Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        this.transform.position = target.position - (rotation * _offset);
        this.transform.LookAt(target);
    }
}
