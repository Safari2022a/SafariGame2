using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deer : AnimalBase
{
    override protected void Start() {
        base.Start();
        _nickName = "ダマジカ";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
