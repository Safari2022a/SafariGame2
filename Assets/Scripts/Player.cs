using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 prevMousePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    
    void Update()
    {
        //視点回転
        Vector3 mouseDelta = Input.mousePosition - prevMousePos;
        Vector3 angles = transform.localEulerAngles;
        //print(mouseDelta);
        angles.y += mouseDelta.x;
        transform.localEulerAngles = angles;
        prevMousePos = Input.mousePosition;

        //移動
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("ok");
        }

        
    }

    private void FixedUpdate()
    {
        //Vector3 force = new Vector3(0, 0, 6f);
        //rb.AddForce(force);

        
    }

    public void MouseMoveEvent()
    {
        print("move");
    }
}
