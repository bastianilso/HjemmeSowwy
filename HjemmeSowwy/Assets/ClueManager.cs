using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ClueManager : MonoBehaviour
{
    [SerializeField]
    public Camera sceneCamera;
    [SerializeField]
    public GameObject otherPos;
    public int numberOfClues = 4;

    public List<Clue> clues;

    [SerializeField]
    public List<InputField> clueInputFields;
    
    [SerializeField]
    public InputField clueInputFieldTemplate;

    [SerializeField]
    public Button continueButton;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera.transform.position = otherPos.transform.position;
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        clues = new List<Clue>();
        clueInputFields = new List<InputField>();
        for (int i = 0; i < numberOfClues; i++) {
            var inputField = Instantiate(clueInputFieldTemplate) as InputField;
            inputField.gameObject.transform.SetParent(clueInputFieldTemplate.transform.parent);
            inputField.gameObject.SetActive(true);
            var children = inputField.gameObject.GetComponentsInChildren<Text>();
            children[0].text = "Write Clue..";
            clueInputFields.Add(inputField);
        }
    }

    void Update() {
        bool shouldContinue = true;
        foreach (var clueField in clueInputFields) {
                if (string.IsNullOrEmpty(clueField.text)) {
                    shouldContinue = false;
                }   
        }
        continueButton.interactable = shouldContinue;
    }

    public void SaveClues() {
        foreach (var clueField in clueInputFields) {
                gameManager.AddClue(clueField.text);
        }
    }
}
