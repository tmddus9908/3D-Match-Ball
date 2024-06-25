using UnityEngine;

public class BallSpawnPoint : Singleton<BallSpawnPoint>
{
    public GameObject ball;
    public Material[] materials;
    public Camera upCamera;
    public float speed;
    
    private float _hAxis;
    private float _vAxis;
    private bool _isFalling;
    
    private GameObject _instantiated;
    
    private Vector3 _priorPosition;
    private Vector3 _planetPosition;
    
    private Rigidbody _planetRigidbody;
    private Rigidbody _rigidbody;
    private void Start()
    {
        InstantiatePlanet();
        _rigidbody = GetComponent<Rigidbody>();
        _priorPosition = transform.position;
    }

    private void Update()
    {
        GetInput();
        DropPlanet();
    }

    private void FixedUpdate()
    {
        if(_isFalling == false)
            MovePlanet();
    }
  
    public void InstantiatePlanet()
    {
        int ran = Random.Range(0, 3);
        _instantiated = Instantiate(ball.gameObject, transform.position, Quaternion.identity);
        _instantiated.GetComponent<Planet>().level = ran;
        _instantiated.GetComponent<Planet>().mesh.material = materials[ran];
        _instantiated.transform.localScale = new Vector3(5 + 2 * ran, 5 + 2 * ran, 5 + 2 * ran);
        
        _planetRigidbody = _instantiated.GetComponent<Rigidbody>();
        _planetRigidbody.useGravity = false;
        _isFalling = false;
    }
    public void ReturnSpawnerPosition()
    {
        transform.position = _priorPosition;
    }

    private void DropPlanet()
    {
        if(Input.GetKey(KeyCode.Space) && _planetRigidbody.useGravity == false)
        {
           _planetRigidbody.useGravity = true;
           _isFalling = true;
        }
    }
    private void GetInput()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");
    }
    private void MovePlanet()
    {
        _planetPosition = new Vector3(_hAxis, 0, _vAxis).normalized;
        Vector3 move = _planetPosition * speed * Time.deltaTime;
        
        transform.position += _planetPosition * speed * Time.deltaTime;
        _planetRigidbody.MovePosition(_planetRigidbody.position + move);
        _rigidbody.MovePosition(_rigidbody.position + move);
    }
}
