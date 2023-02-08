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
    [SerializeField] AudioClip clickSound;

    AnimalState state = AnimalState.Idle;

    string happyStr = "qweasdzxc";
    string hateStr = "yuihjknm";

    GameObject _paneCon;
    Rigidbody _rb;
    Animator _anim;
    protected float walkPower = 650f;
    protected float runPower = 720;

    GameObject touchableObj;
    GameObject walkableObj;

    SerialHandler _serialHandler;

    virtual protected void Start() {
        audioSource = GetComponent<AudioSource>();
        _paneCon = GameObject.FindWithTag("Panel/Controller");
        _rb = GetComponent<Rigidbody>();

        transform.Find("../Room").name += GetInstanceID();
        

        walkableObj = transform.Find("Walkable").gameObject;
        touchableObj = transform.Find("Touchable").gameObject;

        toWalkable();
        startRun();

        _serialHandler = GameObject.FindWithTag("SerialHandler").GetComponent<SerialHandler>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PanelController pc = GameObject.FindWithTag("Panel/Controller").GetComponent<PanelController>();
        if (pc.CurrentPanel == Panel.AnimalSelect) {
            pc.OnAnimalClick(gameObject);
            startIdle();
            toTouchable();
            audioSource.PlayOneShot(clickSound);
        }
    }

    protected virtual void Update() {
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

        _anim.SetTrigger("Happy");

        StartCoroutine(Utility.DelayCoroutine(1, () => {
            EndHappy(happyEffect);
        }));

        _serialHandler.Write("Happy");
    }

    void EndHappy(GameObject happyEffect) {
        Destroy(happyEffect);
        // if (state == AnimalState.Happy) state = AnimalState.Idle;
        if (state == AnimalState.Happy) startIdle();
    }
    
    void StartHate() {
        if (state == AnimalState.Hate) return;

        GameObject hateEffect = Instantiate(hateEffectPrefab);
        hateEffect.transform.SetParent(transform.Find("Effects"));
        hateEffect.transform.localPosition = Vector3.zero;
        
        state = AnimalState.Hate;
        audioSource.PlayOneShot(happySound);

        _anim.SetTrigger("Hate");

        StartCoroutine(Utility.DelayCoroutine(1, () => {
            EndHate(hateEffect);
        }));

        _serialHandler.Write("Hate");
    }
    
    void EndHate(GameObject hateEffect) {
        Destroy(hateEffect);
        // if (state == AnimalState.Hate) state = AnimalState.Idle;
        if (state == AnimalState.Hate) startIdle();
    }

    void OnTriggerExit(Collider c) {
        if (c.gameObject.name.Equals("Room"+GetInstanceID())) {
            startRotate();
        }
    }

    void startIdle() {
        state = AnimalState.Idle;
        _rb.velocity = Vector3.zero;
        // _anim.SetTrigger("Idle");
        _rb.constraints = RigidbodyConstraints.FreezeAll;

        _serialHandler.Write("Idle");
        print("Start Idle");
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
            if (state == AnimalState.Idle) return;

            if (UnityEngine.Random.Range(0f, 1f) < 0.5f) {
                startWalk();
            } else {
                startRun();
            }
        }));
    }

    IEnumerator RotateCoroutine(int frameCount, float angle, Action callBack) {
        for (int i = 0; i < frameCount; i++) {
            if (state == AnimalState.Idle) break;
            Vector3 r = transform.localEulerAngles;
            r.y += angle;
            transform.localEulerAngles = r;
            yield return null;
        }

        callBack();
    }

    void toTouchable() {
        touchableObj.transform.localScale = new Vector3(1, 1, 1);
        walkableObj.transform.localScale = Vector3.zero;
        _anim = touchableObj.GetComponent<Animator>();
    }
    
    void toWalkable() {
        touchableObj.transform.localScale = Vector3.zero;
        walkableObj.transform.localScale = new Vector3(1, 1, 1);
        _anim = walkableObj.GetComponent<Animator>();
    }
}
