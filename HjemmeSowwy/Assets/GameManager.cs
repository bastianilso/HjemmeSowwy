using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Clue {
    public string clueText;
    public bool read;
}

public class GameManager : MonoBehaviour
{
    public string hidingObject;
    public static GameObject instance;
    public List<Clue> clues;

    void Awake() {
        if (instance == null) {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        clues = new List<Clue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHidingObject(string id) {
        hidingObject = id;
    }

    public void AddClue(string clueText) {
        var clue = new Clue();
        clue.clueText = clueText;
        clue.read = false;
        clues.Add(clue);
    }

    public string RetreiveNextClue() {
        string nextClue = string.Empty;
        for (int i = 0; i < clues.Count; i++) {
            if (!clues[i].read) {
                nextClue = clues[i].clueText;
                Clue updatedClue = new Clue();
                updatedClue.clueText = clues[i].clueText;
                updatedClue.read = true;
                clues[i] = updatedClue;
            }
        }
        return nextClue;
    }

}
