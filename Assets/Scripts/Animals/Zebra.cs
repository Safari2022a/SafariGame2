using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zebra : AnimalBase
{
    override protected void Start() {
        base.Start();
        _nickName = "シマウマ";
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
