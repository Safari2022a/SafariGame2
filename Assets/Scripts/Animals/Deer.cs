using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deer : AnimalBase
{
    override protected void Start() {
        base.Start();
        _nickName = "γγγΈγ«";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
