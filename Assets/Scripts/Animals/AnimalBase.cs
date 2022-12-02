using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AnimalState {
    Normal,
    Happy,
    Hate,
}

public class AnimalBase : MonoBehaviour, IPointerClickHandler
{
    string _nickName = "ダマジカ";
    public string NickName { get { return _nickName; } }

    [SerializeField] GameObject happyEffectPrefab;
    [SerializeField] GameObject hateEffectPrefab;

    GameObject happyEffect;
    GameObject hateEffect;

    AnimalState state = AnimalState.Normal;

    string happyStr = "qweasdzxc";
    string hateStr = "yuihjknm";

    // bool isHeart = false;
    // bool isHate = false;

    void Start() {
        GameObject heart = transform.Find("Effects/heart/Particle System").gameObject;
        heart.transform.localScale = Vector3.zero;
        GameObject hate = transform.Find("Effects/hate/Particle System").gameObject;
        hate.transform.localScale = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PanelController pc = GameObject.FindWithTag("Panel/Controller").GetComponent<PanelController>();
        if (pc.CurrentPanel == Panel.AnimalSelect) {
            pc.OnAnimalClick(gameObject);
        }
    }

    void Update() {
        for (int i = 0; i < happyStr.Length; i++) {
            if (Input.GetKeyDown(happyStr[i].ToString())) {
                StartHappy();
                StartCoroutine(Utility.DelayCoroutine(3, EndHappy));
            }
        }
        
        for (int i = 0; i < hateStr.Length; i++) {
            if (Input.GetKeyDown(hateStr[i].ToString())) {
                StartHate();
                StartCoroutine(Utility.DelayCoroutine(3, EndHate));
            }
        }
    }

    void StartHappy() {
        if (state == AnimalState.Happy) return;
        if (state == AnimalState.Hate) {
            EndHate();
        }

        print("Happy");
        happyEffect = Instantiate(happyEffectPrefab);
        happyEffect.transform.SetParent(transform.Find("Effects"));
        happyEffect.transform.localPosition = Vector3.zero;

        state = AnimalState.Happy;
    }

    void EndHappy() {
        if (state != AnimalState.Happy) return;

        Destroy(happyEffect);
        state = AnimalState.Normal;
    }
    
    void StartHate() {
        if (state == AnimalState.Hate) return;
        if (state == AnimalState.Happy) {
            EndHappy();
        }

        hateEffect = Instantiate(hateEffectPrefab);
        hateEffect.transform.SetParent(transform.Find("Effects"));
        hateEffect.transform.localPosition = Vector3.zero;
        
        state = AnimalState.Hate;
    }
    
    void EndHate() {
        if (state != AnimalState.Hate) return;

        Destroy(hateEffect);
        state = AnimalState.Normal;
    }
}
