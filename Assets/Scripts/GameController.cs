using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    Dictionary<int, GameObject> otherUsers;
    [SerializeField] GameObject otherUserPrefab;

    void Start() {
        otherUsers = new Dictionary<int, GameObject>();
    }

    public void CreateOtherUser(int id) {
        GameObject otherUser = Instantiate(otherUserPrefab, Vector3.zero, transform.rotation);
        otherUser.transform.SetParent(GameObject.FindWithTag("OtherUsersContainer").transform);
    }

    public void AddUser(int userID, GameObject otherUser) {
        otherUsers.Add(userID, otherUser);
    }

    public void RemoveUser(int userID) {
        otherUsers.Remove(userID);
    }
}
