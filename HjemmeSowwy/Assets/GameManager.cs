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

    public enum Winner {
        WrongObject,
        Timeout,
        Hider,
        Skater,
        Tie

    }
public class GameManager : MonoBehaviour
{
    public int hidingObject;
    public static GameObject instance;
    public List<Clue> clues;
    public int cluesLeft = 0;
    public TotalScore totalScore;

    public float seekTimeMax = 180;
    public float remainingTime = -1;
    public SeekerState seekerState;
    public Winner winner = Winner.Timeout;

    public int currentSeekerScore = 0;
    public int currentHiderScore = 0;
    public int seekerCountDown = 10;
    public int roundNumber = 0;

    void Awake() {
        if (instance == null) {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        newGame();
    }

    public void newGame() {
        clues = new List<Clue>();
        totalScore = new TotalScore();
        totalScore.seekerScore = 0;
        totalScore.hiderScore = 0;
        totalScore.rounds = 0;
        roundNumber++;
        seekerState = SeekerState.Standby;
    }

    // Update is called once per frame
    void Update()
    {
        if (seekerState == SeekerState.Standby) {
            // do nothing
        } else if (seekerState == SeekerState.Countdown) {
            currentSeekerScore = Mathf.RoundToInt(remainingTime*100);
            // count down
        } else if (seekerState == SeekerState.Seeking) {
            if (remainingTime > 0f) {
                remainingTime -= Time.deltaTime;
                currentSeekerScore -= Mathf.RoundToInt(Time.deltaTime*100);
                currentHiderScore = Mathf.RoundToInt((seekTimeMax - remainingTime)*100);
            } else {
                seekerState = SeekerState.GameOver;
                winner = Winner.Timeout;
            }
        } else if (seekerState == SeekerState.GameOver) {
            // Trigger High score table
            // Calculate number of clues read
            // Award points based on that
            // Calculate new TotalScore
            //seekerState = SeekerState.Standby;
        }
    }

    public void SetHidingObject(int id) {
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
        Shuffle<Clue>(clues);
        for (int i = 0; i < clues.Count; i++) {
            if (!clues[i].read) {
                nextClue = clues[i].clueText;
                Clue updatedClue = new Clue();
                updatedClue.clueText = clues[i].clueText;
                updatedClue.read = true;
                cluesLeft--;
                currentSeekerScore -= 1000;
                clues[i] = updatedClue;
                break;
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

    public void SetChosenObject(int id) {
        seekerState = SeekerState.GameOver;
        Debug.Log("Id: " + id.ToString() + " obj: " + hidingObject.ToString());
        if (id == hidingObject) {
            if (currentSeekerScore > currentHiderScore) {
                winner = Winner.Skater;
            } else if (currentHiderScore > currentSeekerScore) {
                winner = Winner.Hider;
            } else {
                winner = Winner.Tie;
            }
        } else {
            winner = Winner.WrongObject;
        }
    }

	public static void Shuffle<T>(IList<T> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

}
