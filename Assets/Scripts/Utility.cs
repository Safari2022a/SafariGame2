using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StartCoroutine(Utility.DelayCoroutine(3, () => {

// }));

public delegate void ActionDelegate();

class Utility {
    public static IEnumerator DelayCoroutine(float seconds, ActionDelegate action) {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
}
