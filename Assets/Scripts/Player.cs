using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicHandler
{
    // うまいこと動物の目の前に視点を移動させたいけどうまくいかない。ベクトルの勉強するべき。
    public void MoveFrontOfAnimal(GameObject animal) {
        transform.position = animal.transform.position + animal.transform.forward * 5;
        transform.localEulerAngles = animal.transform.eulerAngles + new Vector3(1, 180, 1);
        cameraT.transform.localEulerAngles = Vector3.zero;

        startIdle();
    }

    protected override void Update() {
        base.Update();
        if (Input.GetKeyDown(",")) {
            print(transform.position);
        }
    }

}
