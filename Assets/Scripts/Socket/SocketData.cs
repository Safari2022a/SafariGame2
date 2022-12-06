using System;
using UnityEngine;

class SocketData {
    public string type; //event?
    public string content;

    public SocketData(string type, string content) {
        this.type = type;
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

class DataActiveUsers {
    public int userID;
    public DataCreateUser[] activeUsers;
}

class DataTransform {
    public int userID;
    public float[] transformAry;

    public DataTransform(int userID, float[] transformAry) {
        this.userID = userID;
        this.transformAry = transformAry;
    }    
}

class DataRemoveUser {
    public int userID;
    public DataRemoveUser(int userID) {
        this.userID = userID;
    }
}

class DataOKUser {
    public int userID;
    public DataOKUser(int userID) {
        this.userID = userID;
    }
}
