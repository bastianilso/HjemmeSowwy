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
    private float currentCountDown;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        timeTextTemplate = timeText.text;
        clueTextTemplate = clueText.text;
        currentCountDown = (float) gameManager.seekerCountDown;
        gameManager.PrepareSeekGame();
    }

    // Update is called once per frame
    void Update()
    {
        var cluestats = gameManager.GetClueStats();
        timeText.text = string.Format(timeTextTemplate, gameManager.remainingTime);
        clueText.text = string.Format(clueTextTemplate, cluestats[0], cluestats[1]);
        if (gameManager.seekerState == SeekerState.Countdown) {
            currentCountDown -= Time.deltaTime;
            if (currentCountDown < 1) {
                countDownText.gameObject.SetActive(false);
                gameManager.StartSeekGame();
            } else {
                countDownText.gameObject.SetActive(true);
                countDownText.text = Mathf.Round(currentCountDown).ToString();
            }
        }
    }
}
