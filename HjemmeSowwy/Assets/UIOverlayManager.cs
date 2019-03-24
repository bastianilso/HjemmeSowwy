using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayManager : MonoBehaviour
{

    [SerializeField]
    private Text timeText;

    private string timeTextTemplate;

    [SerializeField]
    private Text clueText;

    private string clueTextTemplate;
    
    [SerializeField]
    private Text countDownText;

    [SerializeField]
    private GameObject clueOverlay;

    [SerializeField]
    private GameObject clueTemplate;
    private float currentCountDown;
    [SerializeField]
    private GameObject winnerOverlay;

    [SerializeField]
    private Text winnerAnnouncement;

    [SerializeField]
    private Text subExplanation;

    [SerializeField]
    private Text skaterScore;

    [SerializeField]
    private Text hiderScore;

    [SerializeField]
    private Text roundText;

    [SerializeField]
    private Text unusuedCluesPoints;

    [SerializeField]
    private Text inGamePointsHider;

    [SerializeField]
    private Text inGamePointsSkater;
    
    private string unusedCluesPointsTemplate;
    private string roundTextTemplate;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        timeTextTemplate = timeText.text;
        clueTextTemplate = clueText.text;
        currentCountDown = (float) gameManager.seekerCountDown;
        gameManager.PrepareSeekGame();
        roundTextTemplate = roundText.text;
        unusedCluesPointsTemplate = unusuedCluesPoints.text;
    }

    // Update is called once per frame
    void Update()
    {
        var cluestats = gameManager.GetClueStats();
        timeText.text = string.Format(timeTextTemplate, gameManager.remainingTime);
        clueText.text = string.Format(clueTextTemplate, cluestats[0], cluestats[1]);
        inGamePointsHider.text = gameManager.currentHiderScore.ToString();
        inGamePointsSkater.text = gameManager.currentSeekerScore.ToString();
        if (gameManager.seekerState == SeekerState.Countdown) {
            currentCountDown -= Time.deltaTime;
            if (currentCountDown < 1) {
                countDownText.gameObject.SetActive(false);
                gameManager.StartSeekGame();
            } else {
                countDownText.gameObject.SetActive(true);
                countDownText.text = Mathf.Round(currentCountDown).ToString();
            }
        } else if (gameManager.seekerState == SeekerState.Seeking) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (!clueOverlay.activeSelf) {
                    var newClue = Instantiate(clueTemplate);
                    newClue.SetActive(true);
                    newClue.transform.SetParent(clueOverlay.transform);
                    var newClueText = newClue.GetComponentInChildren<Text>();
                    newClueText.text = gameManager.RetreiveNextClue();
                    clueOverlay.SetActive(true);
                } else {
                    clueOverlay.SetActive(false);
                }
            }
        } else if (gameManager.seekerState == SeekerState.GameOver) {
            winnerOverlay.SetActive(true);
            clueOverlay.SetActive(false);
            var clueStats = gameManager.GetClueStats();
            roundText.text = string.Format(roundTextTemplate, gameManager.roundNumber);
            if (gameManager.winner == Winner.Timeout) {
                winnerAnnouncement.text = "EVERYONE LOSES";
                subExplanation.text = "SKATER RAN OUT OF TIME";
                hiderScore.text = "LOST :C";
                skaterScore.text = "LOST :C";
                subExplanation.gameObject.SetActive(true);
                unusuedCluesPoints.gameObject.SetActive(false);
            } else if (gameManager.winner == Winner.WrongObject) {
                winnerAnnouncement.text = "EVERYONE LOSES";
                subExplanation.text = "SKATER CHOSE THE WRONG OBJECT";
                hiderScore.text = "LOST :C";
                skaterScore.text = "LOST :C";
                subExplanation.gameObject.SetActive(true);
                unusuedCluesPoints.gameObject.SetActive(false);
            } else if (gameManager.winner == Winner.Hider) {
                winnerAnnouncement.text = "HIDER WINS";
                skaterScore.text = gameManager.currentSeekerScore.ToString();
                hiderScore.text = gameManager.currentHiderScore.ToString();
                subExplanation.gameObject.SetActive(false);
                if (clueStats[0] == 0) {
                    unusuedCluesPoints.gameObject.SetActive(false);
                } else {
                    unusuedCluesPoints.text = string.Format(unusedCluesPointsTemplate, 1000*clueStats[0], clueStats[0]); 
                }
            } else if (gameManager.winner == Winner.Skater) {
                winnerAnnouncement.text = "SKATER WINS";
                subExplanation.gameObject.SetActive(false);
                skaterScore.text = gameManager.currentSeekerScore.ToString();
                hiderScore.text = gameManager.currentHiderScore.ToString();
                if (clueStats[0] == 0) {
                    unusuedCluesPoints.gameObject.SetActive(false);
                } else {
                    unusuedCluesPoints.text = string.Format(unusedCluesPointsTemplate, 1000*clueStats[0], clueStats[0]); 
                }
            } else if (gameManager.winner == Winner.Tie) {
                winnerAnnouncement.text = "TIE";
                subExplanation.gameObject.SetActive(true);
                subExplanation.text = "SKATER AND WINNER HAVE EQUAL AMOUNT OF POINTS";
                skaterScore.text = gameManager.currentSeekerScore.ToString();
                hiderScore.text = gameManager.currentHiderScore.ToString();
                if (clueStats[0] == 0) {
                    unusuedCluesPoints.gameObject.SetActive(false);
                } else {
                    unusuedCluesPoints.text = string.Format(unusedCluesPointsTemplate, 1000*clueStats[0], clueStats[0]); 
                }
            }

        }
    }

    public void startNextGame() {
        gameManager.newGame();
    }
}
