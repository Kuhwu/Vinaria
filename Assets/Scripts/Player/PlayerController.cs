using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private LayerMask _groundLayer; // Define which layer represents the ground
    private Vector3 _targetPosition;
    private bool _hasTarget = false;

    void Update()
    {
        HandleClickInput();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleClickInput()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                _targetPosition = hit.point; // Set the target position
                _hasTarget = true;
            }
        }
    }

    void Look()
    {
        if (_hasTarget)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            direction.y = 0; // Ignore vertical difference for rotation

            if (direction != Vector3.zero)
            {
                var rot = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
            }
        }
    }

    void Move()
    {
        if (_hasTarget)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            direction.y = 0; // Ignore vertical movement

            // Move the player towards the target position
            Vector3 newPosition = transform.position + direction * _speed * Time.deltaTime;

            // Stop moving if the player is very close to the target
            if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
            {
                _hasTarget = false;
            }
            else
            {
                _rb.MovePosition(newPosition);
            }
        }
    }
}
