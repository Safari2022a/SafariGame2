using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHandler : MonoBehaviour
{
    Rigidbody rb;
    Transform cameraT;

    [SerializeField, Range(0f, 1f)]
    float walkSpeed = 100f;
    float jumpPower = 250f;
    float mouseSensitivity = 1f;
    
    bool isGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraT = GameObject.FindWithTag("FollowingCamera").transform;
    }

    void Update()
    {
        //視点回転
        Vector3 angles = transform.localEulerAngles;
        angles.y += Input.GetAxis("Mouse X") * Time.deltaTime * 1200f * mouseSensitivity;
        transform.localEulerAngles = angles;

        angles = cameraT.localEulerAngles;
        if (angles.x > 180f) { angles.x -= 360f; } //0 - 1 = 359になるからだけど、これでいいのかな？
        angles.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * 1200f * mouseSensitivity;
        if (angles.x > 90f) {
            angles.x = 90f;
        } else if (angles.x < -90f) {
            angles.x = -90f;
        }
        cameraT.localEulerAngles = angles;
        //
    }

    private void FixedUpdate()
    {
        //移動
        Vector3 force = Input.GetAxis("Vertical") * transform.forward * walkSpeed * Time.deltaTime;
        rb.AddForce(force);
        force = Input.GetAxis("Horizontal") * transform.right * walkSpeed * Time.deltaTime;
        rb.AddForce(force);

        //ジャンプ
        if (isGround && Input.GetAxis("Jump") == 1f)
        {
            print("Jump!");
            isGround = false;
            rb.AddForce(new Vector3(0, jumpPower, 0));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.tag == "Ground")
        if (true)
        {
            isGround = true;
        }
    }
}
