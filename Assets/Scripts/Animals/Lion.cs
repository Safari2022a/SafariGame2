using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lion : AnimalBase
{
    override protected void Start() {
        base.Start();
        _nickName = "ライオンの赤ちゃん";
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
