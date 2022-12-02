using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelStart : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        PanelController pc = GameObject.FindWithTag("Panel/Controller").GetComponent<PanelController>();
        pc.OnStartButtonClick();
    }
}
