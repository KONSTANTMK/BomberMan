using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BomberManController : MonoBehaviour
{
    public SensorController _SensorController;

    private float horizontal;
    private float vertical;

    private Vector3 moveVector;

    public GameObject bombPrefab;

    private Animator _animator;
    public Rigidbody _rigidbody;
    public CharacterController _characterController;
   
    public float speedMove;
    public float speedJump;
    private float gravityForce;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        GetNewComponents();
        Movement();
        CheckGravity();
        //_SensorController=gameObject.GetComponentInChildren<SensorController>();
        
    }

    private void GetNewComponents()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void Movement()
    {
        moveVector = Vector3.zero;
        moveVector.x = horizontal * speedMove;
        moveVector.z = vertical * speedMove;
        moveVector.y = gravityForce;

        _characterController.Move(moveVector * Time.deltaTime);

        _characterController.transform.Rotate(Vector3.up * horizontal * (100f * Time.deltaTime));
    }

    private void CheckGravity()
    {
        if (!_characterController.isGrounded)
        {
            gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            gravityForce -= 1f;
        }
    }

    //private void Move()
    //{



    //    if (horizontal > 0.0f && vertical == 0.0f && _SensorController.CanMoveRight || horizontal < 0.0f && vertical == 0.0f && _SensorController.CanMoveLeft)
    //    {

    //        _Animator.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), 1).magnitude);
    //        _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), 1) * speed;
    //        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(horizontal, 0, 0)), Time.deltaTime * 10);

    //    }
    //    else if (horizontal == 0.0f && vertical > 0.0f && _SensorController.CanMoveForward || horizontal == 0.0f && vertical < 0.0f && _SensorController.CanMoveBack)
    //    {
    //        _Animator.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(0, 0, vertical), 1).magnitude);
    //        _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(0, 0, vertical), 1) * speed;
    //        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, vertical)), Time.deltaTime * 10);
    //    }

    //    if (horizontal > 0.0f && _SensorController.CanMoveRight == false || horizontal < 0.0f && _SensorController.CanMoveLeft == false)
    //    {

    //        _Animator.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(0, 0, vertical), 1).magnitude);
    //        _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(0, 0, vertical), 1) * speed;

    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(horizontal, 0, vertical)), Time.deltaTime * 10);



    //    }
    //    else if (vertical > 0.0f && _SensorController.CanMoveForward == false || vertical < 0.0f && _SensorController.CanMoveBack == false)
    //    {

    //        _Animator.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), 1).magnitude);
    //        _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(horizontal, 0, 0), 1) * speed;
    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(horizontal, 0, vertical)), Time.deltaTime * 10);

    //    }




    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        DropBomb();
    //    }


    //}

    private void DropBomb()
    {
        if (bombPrefab)
        { 
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



