using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float moleAnimationSpeed;
    [SerializeField] private float moleCooldown;
    [SerializeField] private bool soundActive;
    [SerializeField] private bool vibrationActive;
    [SerializeField] private List<Mole> moles;

    private List<Mole> sequence;
    private List<float> times;
    public int moleCount { get; private set; }

    private void Start()
    {
        // Connect events
        foreach (Mole mole in moles)
        {
            mole.moleHit += OnMoleHit;
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            StartRun();
        }
    }

    public void StartRun()
    {
        // Reset times list
        times = new List<float>();

        // Reset moles
        foreach (Mole mole in moles)
        {
            mole.Hide();
        }

        // Set mole animation speed
        Mole.s_animationSpeed = moleAnimationSpeed;
        // Set Sound and Vibration flags
        Mole.s_soundActive = soundActive;
        Mole.s_vibrationActive = vibrationActive;

        // Create sequence
        sequence = new List<Mole>();
        sequence.AddRange(moles.OrderBy(mole => Random.value));
        sequence.AddRange(moles.OrderBy(mole => Random.value));

        moleCount = 0;

        // Show first mole
        sequence[moleCount].Show();
    }

    public void OnMoleHit(int time)
    {
        times.Add(time);
        StartCoroutine(SpawnNextCoroutine());
    }

    public IEnumerator SpawnNextCoroutine()
    {
        moleCount++;
        if (moleCount < sequence.Count)
        {
            yield return new WaitForSeconds(moleCooldown);
            sequence[moleCount].Show();
        }
    }
}
