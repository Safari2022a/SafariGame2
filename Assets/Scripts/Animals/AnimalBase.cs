using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalBase : MonoBehaviour, IPointerClickHandler
{
    string _nickName = "ダマジカ";
    public string NickName { get { return _nickName; } }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        PanelController pc = GameObject.FindWithTag("Panel/Controller").GetComponent<PanelController>();
        if (pc.CurrentPanel == Panel.AnimalSelect) {
            pc.OnAnimalClick(gameObject);
        }
    }
}
