using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    private float _hAxis;
    private float _vAxis;
    private bool _wDown;
    
    private Vector3 _moveVec;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");
        _wDown = Input.GetButton("Walk");

        _moveVec = new Vector3(_hAxis, 0, _vAxis).normalized;

        transform.position += _moveVec * speed * (_wDown ? 0.3f : 1f) * Time.deltaTime;
        
        _anim.SetBool("isRun", _moveVec != Vector3.zero);
        _anim.SetBool("isWalk", _wDown);
        
        transform.LookAt(transform.position + _moveVec);
    }
}
