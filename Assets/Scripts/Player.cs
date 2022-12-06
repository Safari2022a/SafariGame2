using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // int _id;
    GameObject _gc;
    GameObject _sc;

    void Start() {
        _gc = GameObject.FindWithTag("GameController");
        _sc = GameObject.FindWithTag("SocketClient");

        SocketData data = new SocketData("CreateUser", "");
        _sc.GetComponent<SocketClient>().Send(JsonUtility.ToJson(data));
    }

    void OnDestroy() {
        int userID = _gc.GetComponent<GameController>().UserID;
        DataRemoveUser dru = new DataRemoveUser(userID);
        SocketData data = new SocketData("RemoveUser", JsonUtility.ToJson(dru));
        _sc.GetComponent<SocketClient>().Send(JsonUtility.ToJson(data));
    }

    void Update() {
        if (Time.frameCount % 2 == 0) {
            int userID = _gc.GetComponent<GameController>().UserID;
            DataTransform dt = new DataTransform(userID, transformToArray());
            SocketData data = new SocketData("UpdT", JsonUtility.ToJson(dt));
            _sc.GetComponent<SocketClient>().Send(JsonUtility.ToJson(data));
        }
    }

    float[] transformToArray() {
        float[] res = new float[6];
        res[0] = transform.position.x;
        res[1] = transform.position.y;
        res[2] = transform.position.z;
        
        res[3] = transform.eulerAngles.x;
        res[4] = transform.eulerAngles.y;
        res[5] = transform.eulerAngles.z;

        // transformAry[6] = transform.scale.x;
        // transformAry[7] = transform.scale.y;
        // transformAry[8] = transform.scale.z;

        return res;
    }
}
