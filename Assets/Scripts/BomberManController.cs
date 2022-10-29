using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BomberManController : MonoBehaviour
{
    public SensorController _SensorController;

    private float horizontal;
    private float vertical;

    private Animator _Animator;

    public GameObject bombPrefab;
    public Rigidbody _rigidbody;
    public float speed;
    
    public float SensorRange = 2f;

    public static int Direction;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        GetNewComponents();
        Move();
        //_SensorController=gameObject.GetComponentInChildren<SensorController>();
        
    }

    private void GetNewComponents()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        
        

    }

    private void Move()
    {
        
        
        
        if (horizontal > 0.0f && vertical == 0.0f && _SensorController.CanMoveRight || horizontal < 0.0f && vertical == 0.0f && _SensorController.CanMoveLeft)
            {
                 
                _Animator.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), 1).magnitude);
                _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), 1) * speed;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(horizontal, 0, 0)),Time.deltaTime * 10);

            } 
            else if (horizontal == 0.0f && vertical > 0.0f && _SensorController.CanMoveForward || horizontal == 0.0f && vertical < 0.0f && _SensorController.CanMoveBack)
            {
            _Animator.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(0, 0, vertical), 1).magnitude);
               _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(0, 0, vertical), 1) * speed;
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, vertical)), Time.deltaTime * 10);
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }


    }

    private void DropBomb()
    {
        if (bombPrefab)
        { //Check if bomb prefab is assigned first
            // Create new bomb and snap it to a tile
            Instantiate(bombPrefab,
                new Vector3(Mathf.RoundToInt(transform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(transform.position.z)),
                bombPrefab.transform.rotation);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        { //Not dead & hit by explosion
            Debug.Log("Вы погибли");

            //Notify global state manager that this player died
            Destroy(transform.parent.gameObject);
        }
    }


}



