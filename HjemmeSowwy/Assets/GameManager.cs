using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public struct Clue {
    public string clueText;
    public bool read;
}

public struct TotalScore {
    public int seekerScore;
    public int hiderScore;
    public int rounds;
}
    public enum SeekerState {
        Standby,
        Countdown,
        Seeking,
        GameOver

    }
public class GameManager : MonoBehaviour
{



    public string hidingObject;
    public static GameObject instance;
    public List<Clue> clues;
    private int cluesLeft = -1;
    public TotalScore totalScore;

    public float seekTimeMax = 180;
    public float remainingTime = -1;
    public SeekerState seekerState;

    public int currentSeekerScore = 0;
    public int currentHiderScore = 0;
    public int seekerCountDown = 5;

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
        totalScore = new TotalScore();
        totalScore.seekerScore = 0;
        totalScore.hiderScore = 0;
        totalScore.rounds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (seekerState == SeekerState.Standby) {
            // do nothing
        } else if (seekerState == SeekerState.Countdown) {
            // count down
        } else if (seekerState == SeekerState.Seeking) {
            if (remainingTime > 0f) {
                remainingTime -= Time.deltaTime;
                currentSeekerScore = Mathf.RoundToInt(remainingTime*100);
                currentHiderScore = Mathf.RoundToInt((seekTimeMax - remainingTime)*100);
            } else {
                seekerState = SeekerState.GameOver;
            }
        } else if (seekerState == SeekerState.GameOver) {
            // Trigger High score table
            // Calculate number of clues read
            // Award points based on that
            // Calculate new TotalScore
            seekerState = SeekerState.Standby;
        }
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
                cluesLeft--;
                clues[i] = updatedClue;
            }
        }
        return nextClue;
    }

    public int[] GetClueStats() {
        int[] cluestats = new int[2];
        cluestats[0] = cluesLeft;
        cluestats[1] = clues.Count;
        return cluestats;
    }

    public SeekerState GetSeekerState() {
        return seekerState;
    }

    public void PrepareSeekGame() {
        cluesLeft = clues.Count;
        remainingTime = seekTimeMax;
        seekerState = SeekerState.Countdown;
    }

    public void StartSeekGame() {
        seekerState = SeekerState.Seeking;
    }

}
