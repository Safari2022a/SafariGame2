using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalBase : MonoBehaviour, IPointerClickHandler
{
    string _nickName = "ダマジカ";
    public string NickName { get { return _nickName; } }

    bool isHeart = false;
    bool isHate = false;

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

        if (isHeart) {
            GameObject heart = transform.Find("Effects/heart/Particle System").gameObject;
            heart.transform.localScale = new Vector3(0.11613f, 0.11613f, 0.11613f);
        } else if (isHate) {
            GameObject hate = transform.Find("Effects/hate/Particle System").gameObject;
            hate.transform.localScale = new Vector3(0.11613f, 0.11613f, 0.11613f);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            isHeart = true;
            isHate = false;
        } else if (Input.GetKeyDown(KeyCode.P)) {
            isHeart = false;
            isHate = true;
        } else if (Input.GetKeyDown(KeyCode.L)) {
            isHeart = false;
            isHate = false;
            GameObject heart = transform.Find("Effects/heart/Particle System").gameObject;
            heart.transform.localScale = Vector3.zero;
            GameObject hate = transform.Find("Effects/hate/Particle System").gameObject;
            hate.transform.localScale = Vector3.zero;
        }
    }
}
