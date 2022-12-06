using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    Dictionary<int, GameObject> otherUsers;
    [SerializeField] GameObject otherUserPrefab;
    int _userID;
    public int UserID {
        get { return _userID; }
        set {
            _userID = value;
        }
    }

    void Start() {
        otherUsers = new Dictionary<int, GameObject>();
    }

    public void CreateOtherUser(int id) {
        if (otherUsers.ContainsKey(id) || id == _userID) return;

        GameObject otherUser = Instantiate(otherUserPrefab, Vector3.zero, transform.rotation);
        otherUser.transform.SetParent(GameObject.FindWithTag("OtherUsersContainer").transform);
        otherUsers.Add(id, otherUser);
    }
    
    public void RemoveOtherUser(int id) {
        if (!otherUsers.ContainsKey(id) || id == _userID) return;

        GameObject otherUser = otherUsers[id];
        Destroy(otherUser);
        otherUsers.Remove(id);
    }

    void RemoveUser(int _userID) {
        otherUsers.Remove(_userID);
    }

    public void UpdateOtherUserT(int id, float[] transformAry) {
        if (!otherUsers.ContainsKey(id)) return;
        otherUsers[id].GetComponent<OtherUser>().UpdateTransform(transformAry);
    }

}
