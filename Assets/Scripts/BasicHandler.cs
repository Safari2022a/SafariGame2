using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHandler : MonoBehaviour
{
    Rigidbody _rb;
    protected Transform cameraT;
    AudioSource audioSource;
    
    int walkCnt = 0;
    bool walkSoundOK = true;
    [SerializeField] AudioClip walkSound0;
    [SerializeField] AudioClip walkSound1;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip jumpLandSound;

    [SerializeField] float walkPower = 250f;
    [SerializeField] float jumpPower = 250f;
    
    [SerializeField, Range(0f, 1f)]
    float mouseSensitivity = 1f;
    
    bool isGround = true;

    bool rotatable = true;
    bool movable = true;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cameraT = GameObject.FindWithTag("FollowingCamera").transform;
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        //視点回転
        if (rotatable) {
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
        }
        //

        if (Input.GetKeyDown(KeyCode.R)) {
            movable = !movable;
            rotatable = !rotatable;
        }
    }

    private void FixedUpdate()
    {
        //移動
        if (movable) {
            Vector3 force = Input.GetAxis("Vertical") * transform.forward * walkPower * Time.deltaTime;
            _rb.AddForce(force);
            force = Input.GetAxis("Horizontal") * transform.right * walkPower * Time.deltaTime;
            _rb.AddForce(force);
            
            if (Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") > 0 && isGround && walkSoundOK) {
                walkSoundOK = false;
                if (walkCnt == 0) {
                    audioSource.PlayOneShot(walkSound0);
                } else {
                    audioSource.PlayOneShot(walkSound1);
                }
                walkCnt = (walkCnt + 1) % 2;

                StartCoroutine(Utility.DelayCoroutine(0.5f, () => {
                    walkSoundOK = true;
                }));
            }
        }
        //

        //ジャンプ
        if (isGround && Input.GetAxis("Jump") == 1f && movable)
        {
            isGround = false;
            _rb.AddForce(new Vector3(0, jumpPower, 0));
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.tag == "Ground")
        if (true)
        {
            isGround = true;
            audioSource.PlayOneShot(jumpLandSound);
        }
    }

    protected void startIdle() {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
