using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    Dictionary<int, GameObject> otherUsers;
    [SerializeField] GameObject otherUserPrefab;

    // Keyboard keyboard = Keyboard.current;

    void Start() {
        otherUsers = new Dictionary<int, GameObject>();
        print(Keyboard.current);
        print(InputSystem.devices);
        print(InputSystem.devices.Count);
        print(InputSystem.devices[0]);
        print(InputSystem.devices[0].description);
        print(InputSystem.devices[0].description.interfaceName);
        print(InputSystem.devices[0].description.serial);
        print(InputSystem.devices[0].description.deviceClass);
        // print(InputSystem.devices[1]);
        // print(InputSystem.devices[2]);
        // print(Keyboard.all.Count);
        // print(Keyboard.all);
        // print(Keyboard.all[0]);
        // print(Keyboard.all[1]);
        // print(Keyboard.all[2]);
        // print(GamePada.current);
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
