using TMPro;
using UnityEngine;

public class ArcadeDisplay : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private TMP_Text counter;

    private void Awake()
    {
        counter = transform.Find("Canvas/CounterPanel/CounterLabel").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController != null)
        {
            counter.text = gameController.moleCount.ToString();
        }
    }
}
