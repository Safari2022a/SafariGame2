using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelModeSelect : MonoBehaviour
{
    void Start() {
        PanelController pc = GameObject.FindWithTag("Panel/Controller").GetComponent<PanelController>();
        transform.Find("AnimalName").GetComponent<Text>().text = pc.CurrentAnimal.GetComponent<AnimalBase>().NickName;
        Button touchButton = transform.Find("Modes/Touch").GetComponent<Button>();
        touchButton.onClick.AddListener(pc.OnModeTouchButton);
    }
}
