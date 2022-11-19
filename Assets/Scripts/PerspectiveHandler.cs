using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveHandler : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField, Range(0f, 1f)]
    float walkSpeed = 100f;
    float mouseSensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //視点回転
        Vector3 angles = transform.localEulerAngles;
        angles.y += Input.GetAxis("Mouse X") * Time.deltaTime * 1200f * mouseSensitivity;
        // if (angles.x > 180f) { angles.x -= 360f; } //0 - 1 = 359になるからだけど、これでいいのかな？
        // angles.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * 1200f * mouseSensitivity;
        // if (angles.x > 90f) {
        //     angles.x = 90f;
        // } else if (angles.x < -90f) {
        //     angles.x = -90f;
        // }
        transform.localEulerAngles = angles;

    }

    private void FixedUpdate()
    {
        // float dx = transform.localEulerAngles.x

        //移動
        Vector3 force = Input.GetAxis("Vertical") * transform.forward * walkSpeed * Time.deltaTime;
        print(force);
        rb.AddForce(force);
        force = Input.GetAxis("Horizontal") * transform.right * walkSpeed * Time.deltaTime;
        rb.AddForce(force);
    }
}
