using System;
using UnityEngine;

class SocketData {
    public string type; //event?
    public string content;

    public SocketData(string type, string content) {
        this.type = type;
        Debug.Log(content);
        this.content = content;
    }

    public override string ToString() {
        return $"type: {type}\ncontent:\n{content}";
    }
}

class DataCreateUser {
    public int userID;
    
    public DataCreateUser(int userID) {
        this.userID = userID;
    }
}

class DataTransform : MonoBehaviour {
    public int userID;
    public Transform transform;
}
