using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    Dictionary<int, GameObject> otherUsers;
    [SerializeField] GameObject otherUserPrefab;

    AudioSource audioSource;
    [SerializeField] AudioClip birdSound;

    // Keyboard keyboard = Keyboard.current;

    void Start() {
        otherUsers = new Dictionary<int, GameObject>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(BirdCoroutine());
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

    IEnumerator BirdCoroutine() {
        while (true) {
            audioSource.PlayOneShot(birdSound);
            yield return new WaitForSeconds(Random.Range(4f, 12f));
        }
    }
}
