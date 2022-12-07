using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AnimalState {
    Idle,
    Happy,
    Hate,
    Walking,
    Running,
    Rotating,
}

public class AnimalBase : MonoBehaviour, IPointerClickHandler
{
    protected string _nickName = "ダマジカ";
    public string NickName { get { return _nickName; } }

    [SerializeField] GameObject happyEffectPrefab;
    [SerializeField] GameObject hateEffectPrefab;
    
    AudioSource audioSource;
    [SerializeField] AudioClip happySound;
    [SerializeField] AudioClip hateSound;

    AnimalState state = AnimalState.Idle;

    string happyStr = "qweasdzxc";
    string hateStr = "yuihjknm";

    GameObject _paneCon;
    Rigidbody _rb;
    Animator _anim;
    protected float walkPower = 650f;
    protected float runPower = 720;

    // bool isHeart = false;
    // bool isHate = false;

    virtual protected void Start() {
        // GameObject heart = transform.Find("Effects/heart/Particle System").gameObject;
        // heart.transform.localScale = Vector3.zero;
        // GameObject hate = transform.Find("Effects/hate/Particle System").gameObject;
        // hate.transform.localScale = Vector3.zero;

        audioSource = GetComponent<AudioSource>();
        _paneCon = GameObject.FindWithTag("Panel/Controller");
        _rb = GetComponent<Rigidbody>();
        _anim = transform.Find("Walkable").GetComponent<Animator>();

        transform.Find("../Room").name += GetInstanceID();
        
        startRun();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PanelController pc = GameObject.FindWithTag("Panel/Controller").GetComponent<PanelController>();
        if (pc.CurrentPanel == Panel.AnimalSelect) {
            pc.OnAnimalClick(gameObject);
            startIdle();
        }
    }

    void Update() {
        if (_paneCon.GetComponent<PanelController>().CurrentAnimal == gameObject) {
            for (int i = 0; i < happyStr.Length; i++) {
                if (Input.GetKeyDown(happyStr[i].ToString())) {
                    StartHappy();
                }
            }
            
            for (int i = 0; i < hateStr.Length; i++) {
                if (Input.GetKeyDown(hateStr[i].ToString())) {
                    StartHate();
                }
            }
        }
    }

    void FixedUpdate() {
        if (state == AnimalState.Walking) {
            Vector3 power = transform.forward * Time.deltaTime * walkPower;
            _rb.AddForce(power);
            checkStop();
        } else if (state == AnimalState.Running) {
            Vector3 power = transform.forward * Time.deltaTime * runPower;
            _rb.AddForce(power);
            checkStop();
        }
    }

    int rotateCnt = 0;
    void checkStop() {
        if (_rb.velocity.magnitude < 1f) {
            ++rotateCnt;
            if (rotateCnt > 60) {
                startRotate();
                rotateCnt = 0;
            }
        }
    }

    void StartHappy() {
        if (state == AnimalState.Happy) return;

        GameObject happyEffect = Instantiate(happyEffectPrefab);
        happyEffect.transform.SetParent(transform.Find("Effects"));
        happyEffect.transform.localPosition = Vector3.zero;

        state = AnimalState.Happy;
        audioSource.PlayOneShot(happySound);

        StartCoroutine(Utility.DelayCoroutine(1, () => {
            EndHappy(happyEffect);
        }));
    }

    void EndHappy(GameObject happyEffect) {
        if (state != AnimalState.Happy) return;

        Destroy(happyEffect);
        state = AnimalState.Idle;
    }
    
    void StartHate() {
        if (state == AnimalState.Hate) return;

        GameObject hateEffect = Instantiate(hateEffectPrefab);
        hateEffect.transform.SetParent(transform.Find("Effects"));
        hateEffect.transform.localPosition = Vector3.zero;
        
        state = AnimalState.Hate;

        StartCoroutine(Utility.DelayCoroutine(1, () => {
            EndHate(hateEffect);
        }));
    }
    
    void EndHate(GameObject hateEffect) {
        if (state != AnimalState.Hate) return;

        Destroy(hateEffect);
        state = AnimalState.Idle;
    }

    void OnTriggerExit(Collider c) {
        if (c.gameObject.name.Equals("Room"+GetInstanceID())) {
            startRotate();
        }
    }

    void startIdle() {
        state = AnimalState.Idle;
        _rb.velocity = Vector3.zero;
        _anim.SetTrigger("Idle");
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        print("startIdle");
    }

    void startWalk() {
        state = AnimalState.Walking;
        _anim.SetTrigger("Walk");
    }
    
    void startRun() {
        state = AnimalState.Running;
        _anim.SetTrigger("Run");
    }

    void startRotate() {
        state = AnimalState.Rotating;
        int rotCnt = UnityEngine.Random.Range(140, 220);
        int angle = (int)Math.Pow(-1, UnityEngine.Random.Range(0, 2));
        StartCoroutine(RotateCoroutine(rotCnt, angle, () => {
            if (UnityEngine.Random.Range(0f, 1f) < 0.5f) {
                startWalk();
            } else {
                startRun();
            }
        }));
    }

    IEnumerator RotateCoroutine(int frameCount, float angle, Action callBack) {
        if (state == AnimalState.Idle) yield break;

        for (int i = 0; i < frameCount; i++) {
            Vector3 r = transform.localEulerAngles;
            r.y += angle;
            transform.localEulerAngles = r;
            yield return null;
        }

        callBack();
    }
}
