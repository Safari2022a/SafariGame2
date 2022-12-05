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

class DataTransform {
    public int userID;
    public float[] transformAry;

    // public DataTransform(int userID, Transform transform) {
    //     this.userID = userID;
        
    //     transformAry = new float[6];
    //     transformAry[0] = transform.position.x;
    //     transformAry[1] = transform.position.y;
    //     transformAry[2] = transform.position.z;
        
    //     transformAry[3] = transform.eulerAngles.x;
    //     transformAry[4] = transform.eulerAngles.y;
    //     transformAry[5] = transform.eulerAngles.z;

    //     // transformAry[6] = transform.scale.x;
    //     // transformAry[7] = transform.scale.y;
    //     // transformAry[8] = transform.scale.z;
    // }

    // public DataTransform(int userID, float[] transformAry) {
    //     this.userID = userID;
    //     this.transformAry = transformAry;
    // }
}
