using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicHandler
{
    public void MoveFrontOfAnimal(GameObject animal) {
        Vector3 p = animal.transform.position;
        p.y += 0.5f;
        p += animal.transform.forward * 5;
        transform.position = p;
        transform.eulerAngles = animal.transform.eulerAngles + new Vector3(1, 180, 1);
        cameraT.transform.localEulerAngles = Vector3.zero;
    }
}
