using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    private RaycastHit hitRight;
    private RaycastHit hitLeft;
    private RaycastHit hitForward;
    private RaycastHit hitBack;
    

    public Transform PlayerTransfrorm;

    public bool CanMoveRight;
    public bool CanMoveLeft;
    public bool CanMoveForward;
    public bool CanMoveBack;

    private void FixedUpdate()
    {
        transform.position = new Vector3(PlayerTransfrorm.position.x, PlayerTransfrorm.position.y + 0.5f, PlayerTransfrorm.position.z);
        RayCasterSensor();
    }

    private void RayCasterSensor()
    {

        CanMoveRight = true;
        CanMoveLeft = true;
        CanMoveForward = true;
        CanMoveBack = true;


        if (Physics.Raycast(transform.position, Vector3.right, out hitRight, 0.6f))
        {
            Debug.DrawRay(transform.position, transform.right, Color.green, 0.6f);
            //print("Found an object: " + hitRight.collider.name);

            if (hitRight.collider.gameObject.layer == 6)
            {
                CanMoveRight = false;
            }


        }

        if (Physics.Raycast(transform.position, Vector3.left, out hitLeft, 0.6f))
        {

            Debug.DrawRay(transform.position, -transform.right, Color.green, 0.6f);
            //print("Found an object: " + hitLeft.collider.name);

            if (hitLeft.collider.gameObject.layer == 6)
            {
                CanMoveLeft = false;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out hitForward, 0.6f))
        {

            Debug.DrawRay(transform.position, transform.forward, Color.red, 0.6f);
            //print("Found an object: " + hitForward.collider.name);

            if (hitForward.collider.gameObject.layer == 6)
            {
                CanMoveForward = false;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.back, out hitBack, 0.6f))
        {

            Debug.DrawRay(transform.position, -transform.forward, Color.red, 0.6f);
            //print("Found an object: " + hitBack.collider.name);

            if (hitBack.collider.gameObject.layer == 6)
            {
                CanMoveBack = false;
            }
        }
    }

}