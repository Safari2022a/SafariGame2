using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panel {
    None,
    Start,
    OP,
    AnimalSelect,
    ModeSelect,
    TouchMode,
}

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject panelStartPrefab;
    [SerializeField] GameObject panelOPPrefab;
    [SerializeField] GameObject panelAnimalSelectPrefab;
    [SerializeField] GameObject panelModeSelectPrefab;
    [SerializeField] GameObject panelTouchModePrefab;

    Panel _currentPanel;
    public Panel CurrentPanel { get { return _currentPanel; } }
    
    GameObject _currentAnimal;
    public GameObject CurrentAnimal { get { return _currentAnimal; } }

    GameObject pCont;
    void Start()
    {
        pCont = GameObject.FindWithTag("Panel/Container");

        GameObject panel = Instantiate(panelStartPrefab, pCont.transform.position, pCont.transform.rotation);
        panel.transform.SetParent(pCont.transform);

        _currentPanel = Panel.Start;
    }

    public void OnStartButtonClick() {
        Destroy(GameObject.FindWithTag("Panel/Start"));
        GameObject panel = Instantiate(panelOPPrefab, pCont.transform.position, pCont.transform.rotation);
        panel.transform.SetParent(pCont.transform);
        
        _currentPanel = Panel.OP;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(GameObject.FindWithTag("Panel/OP"));
            GameObject panel = Instantiate(panelAnimalSelectPrefab, pCont.transform.position, pCont.transform.rotation);
            panel.transform.SetParent(pCont.transform);   
            _currentPanel = Panel.AnimalSelect;
        }
    }

    public void OnAnimalClick(GameObject animal) {
        _currentAnimal = animal;
        GameObject.FindWithTag("Player").GetComponent<Player>().MoveFrontOfAnimal(animal);
        
        Destroy(GameObject.FindWithTag("Panel/AnimalSelect"));
        GameObject panel = Instantiate(panelModeSelectPrefab, pCont.transform.position, pCont.transform.rotation);
        panel.transform.SetParent(pCont.transform);
    }

    public void OnModeTouchButton() {
        Destroy(GameObject.FindWithTag("Panel/ModeSelect"));
        GameObject panel = Instantiate(panelTouchModePrefab, pCont.transform.position, pCont.transform.rotation);
        panel.transform.SetParent(pCont.transform);
    }
}
