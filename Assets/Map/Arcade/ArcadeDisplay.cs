using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeDisplay : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private TMP_Text score;
    private TMP_Text time;

    private void Awake()
    {
        score = transform.Find("Canvas/ScorePanel/ScoreLabel").GetComponent<TMP_Text>();
        time = transform.Find("Canvas/TimePanel/TimeLabel").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController != null)
        {
            score.text = gameController.score.ToString();
            time.text = gameController.time.ToString("F0");
        }
    }
}
