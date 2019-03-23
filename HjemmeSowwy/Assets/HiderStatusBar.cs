using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiderStatusBar : MonoBehaviour
{
    [SerializeField]
    private Text roundNumber;
    private string roundNumberTemplate;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        roundNumberTemplate = roundNumber.text;
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        roundNumber.text = string.Format(roundNumberTemplate, gameManager.roundNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
