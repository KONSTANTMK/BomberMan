using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject BomberMan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = BomberMan.transform.position;
        transform.position = new Vector3(BomberMan.transform.position.x, BomberMan.transform.position.y+6f, BomberMan.transform.position.z-6f);
        
    }
}
