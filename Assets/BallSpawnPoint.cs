using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawnPoint : Singleton<BallSpawnPoint>
{
    public GameObject[] balls;
    public Camera upCamera;
    public float speed;
    
    
    private GameObject _instantiated;
    private Vector3 _cameraPosition;
    private Vector2 _inputVector2;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _instantiated = Instantiate(balls[0].gameObject, transform.position, Quaternion.identity);
        _instantiated.GetComponent<Rigidbody>().useGravity = false;
        _cameraPosition = upCamera.transform.position;

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DropPlanet();
    }

    private void FixedUpdate()
    {
        MoveSpawnPoint();
    }

    public void SetCameraPosition()
    {
        upCamera.transform.position = _cameraPosition;
    }

    private void DropPlanet()
    {
        if(Input.GetKey(KeyCode.Space) && _instantiated.GetComponent<Rigidbody>().useGravity == false)
        {
            _instantiated.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void MoveSpawnPoint()
    {
        Vector3 move = _inputVector2 * speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + move);
    }

    void OnMove(InputValue value)
    {
        Debug.Log("눌림");
        _inputVector2 = value.Get<Vector2>();
    }
}
