using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicHandler
{
    // Dictionary<string, Vector3[]> pos;

    // protected override void Start() {
    //     base.Start();
    //     // pos = new Dictionary<string, Vector3[]>() {
    //     //     { "ダマジカ", new Vector3[] { t, new Vector3(1, -15, 1) } },
    //     //     { "シマウマ", new Vector3[] { t, new Vector3(1, -15, 1) } },
    //     //     // { "ダマジカ", new Vector3[] { new Vector3(5.98f, 3.35f, -8.19f), new Vector3(1, -15, 1) } },
    //     //     // { "シマウマ", new Vector3[] { new Vector3(5.98f, 3.35f, -8.19f), new Vector3(1, -15, 1) } },
    //     //     { "ライオンの赤ちゃん", new Vector3[] { new Vector3(5.98f, 3.35f, -8.19f), new Vector3(1, -15, 1) } },
    //     // };
    // }

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
