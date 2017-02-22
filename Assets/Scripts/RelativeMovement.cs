using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

    private float _vertSpeed;
    private CharacterController _characterController;

    private ControllerColliderHit _contact;

    private Animator _animator;

    void Start()
    {
        _characterController = this.GetComponent<CharacterController>();

        _vertSpeed = minFall;

        _animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (0 != horInput || 0 != vertInput)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        bool hitGround = false;
        RaycastHit hit;
        bool isHit = Physics.Raycast(this.transform.position, Vector3.down, out hit);

        if (_vertSpeed < 0 && isHit)
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = (hit.distance <= check);
        }

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = -0.1f;
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            _vertSpeed = _vertSpeed + gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            if (null != _contact)
            {
                _animator.SetBool("Jumping", true);
            }

            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement = movement + _contact.normal * moveSpeed;
                }
            }
        }
        movement.y = _vertSpeed;

        movement = movement * Time.deltaTime;
        _characterController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (null != body && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
