using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class OtherUser : MonoBehaviour {
    int id;

    public void Initialize(int id) {
        this.id = id;
    }

    public void UpdateTransform(float[] a) {
        transform.position = new Vector3(a[0], a[1], a[2]);
        transform.eulerAngles = new Vector3(a[3], a[4], a[5]);
    }
}
