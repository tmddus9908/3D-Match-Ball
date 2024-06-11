using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    
    private float _hAxis;
    private float _vAxis;
    private bool _wDown;
    private bool _jDown;
    
    private bool _isJump;
    private bool _isDodge;
    
    private Vector3 _moveVec;
    private Vector3 _dodgeVec;
    
    private Rigidbody _rigid;
    private Animator _anim;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
       GetInput();
       Move();
       Turn();
       Jump();
       Dodge();
    }

    private void GetInput()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");
        _wDown = Input.GetButton("Walk");
        _jDown = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        _moveVec = new Vector3(_hAxis, 0, _vAxis).normalized;

        if (_isDodge)
            _moveVec = _dodgeVec;
        
        transform.position += _moveVec * speed * (_wDown ? 0.3f : 1f) * Time.deltaTime;
        
        _anim.SetBool("isRun", _moveVec != Vector3.zero);
        _anim.SetBool("isWalk", _wDown);
    }

    private void Turn()
    {
        transform.LookAt(transform.position + _moveVec);
    }

    private void Jump()
    {
        if (_jDown && _moveVec == Vector3.zero && !_isJump && !_isDodge)
        {
            _rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _anim.SetBool("isJump", true);
            _anim.SetTrigger("doJump");
            _isJump = true;
        }
    }
    private void Dodge()
    {
        if (_jDown && _moveVec != Vector3.zero && !_isJump && !_isDodge)
        {
            _dodgeVec = _moveVec;
            
            speed *= 2;
            _anim.SetTrigger("doDodge");
            _isDodge = true;
            
            Invoke("DodgeOut", 0.4f);
        }
    }

    private void DodgeOut()
    {
        speed *= 0.5f;
        _isDodge = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            _anim.SetBool("isJump", false);
            _isJump = false;
        }
    }
}
