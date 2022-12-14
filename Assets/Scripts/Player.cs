using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicHandler
{
    public void MoveFrontOfAnimal(GameObject animal) {
        Vector3 p = animal.transform.position;
        p.y += 0.5f;
        // p.x += 2.32f; //微調整
        p += animal.transform.forward * 5;
        transform.position = p;

        Vector3 r = animal.transform.eulerAngles + new Vector3(1, 180, 1);
        // r.y += -10.101f; //微調整
        transform.eulerAngles = r;
        cameraT.transform.localEulerAngles = Vector3.zero;

        startIdle();
    }
}
