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
        if (otherUsers.ContainsKey(id)) return;

        GameObject otherUser = Instantiate(otherUserPrefab, Vector3.zero, transform.rotation);
        otherUser.transform.SetParent(GameObject.FindWithTag("OtherUsersContainer").transform);
        otherUsers.Add(id, otherUser);
        print(id);
    }

    void RemoveUser(int userID) {
        otherUsers.Remove(userID);
    }

    public void UpdateOtherUserT(int id, float[] transformAry) {
        if (!otherUsers.ContainsKey(id)) return;
        otherUsers[id].GetComponent<OtherUser>().UpdateTransform(transformAry);
    }
}
