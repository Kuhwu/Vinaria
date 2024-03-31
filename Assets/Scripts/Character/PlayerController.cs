using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _input;
    private bool _forwardPressed;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;

    void Update()
    {
        GatherInput();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _forwardPressed = Input.GetKey(KeyCode.LeftShift);
    }

    void Look()
    {
        var relative = (transform.position + _input) - transform.position;
        var rot = Quaternion.LookRotation(relative, Vector3.up);
        transform.rotation = rot;
    }

    void Move()
    {
        if (_forwardPressed)
        {
            _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime); //LSHIFT move forward
        }
    }   
}
