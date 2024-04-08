using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Camera _mainCamera;
    private bool _isMoving;
    private Vector3 _targetPosition;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button is clicked
        {
            SetTargetPosition();
        }

        if (_isMoving)
        {
            MoveToTargetPosition();
        }
    }

    void SetTargetPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _targetPosition = hit.point;
            _targetPosition.y = transform.position.y; // Keep the same height as the player
            _isMoving = true;
        }
    }

    void MoveToTargetPosition()
    {
        // Calculate the direction to move towards the target position
        Vector3 direction = (_targetPosition - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;

        // Rotate towards the target point (optional)
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);

        // If the player is close enough to the target position, stop moving
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _isMoving = false;
        }
    }
}
