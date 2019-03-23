using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ClueManager : MonoBehaviour
{

    public int numberOfClues = 5;

    public List<Clue> clues;

    [SerializeField]
    public List<InputField> clueInputFields;
    
    [SerializeField]
    public InputField clueInputFieldTemplate;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        clues = new List<Clue>();
        clueInputFields = new List<InputField>();
        for (int i = 0; i < numberOfClues; i++) {
            var inputField = Instantiate(clueInputFieldTemplate) as InputField;
            inputField.gameObject.transform.SetParent(clueInputFieldTemplate.transform.parent);
            inputField.gameObject.SetActive(true);
            var children = inputField.gameObject.GetComponentsInChildren<Text>();
            children[0].text = "Write Clue " + i + "..";
            clueInputFields.Add(inputField);
        }
    }

    public void SaveClues() {
        foreach (var clueField in clueInputFields) {
            gameManager.AddClue(clueField.text);
        }
    }
}
