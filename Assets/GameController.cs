using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class GameController : MonoBehaviour
{
    [SerializeField] private int runDuration;
    [SerializeField] private float moleAnimationDuration;
    [SerializeField] private float moleStayDuration;
    [SerializeField] private float moleSpawnCooldown;
    [SerializeField] private bool soundActive;
    [SerializeField] private bool vibrationActive;
    [SerializeField] private List<Mole> moles;

    public InputAction startGame;

    public int score { get; private set; }
    public float time { get; private set; } // Run timer

    private void Start()
    {
        startGame.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // T FOR TEST ACTIVATION
        if (time <= 0.0f && Keyboard.current.tKey.wasPressedThisFrame)
        {
            foreach (Mole mole in moles)
            {
                mole.Show(3.0f);
            }
        }

        // Run start/restart
        
        if (time <= 0.0f && startGame.WasPerformedThisFrame())
        {
            // Reset score
            score = 0;
            // Reset timer
            time = runDuration;
            // Reset moles
            foreach (Mole mole in moles)
            {
                mole.Hide();
            }
            // Set mole animation speed
            Mole.s_animationSpeed = 1.0f / moleAnimationDuration;
            // Set Sound and Vibration flags
            Mole.s_soundActive = soundActive;
            Mole.s_vibrationActive = vibrationActive;
            // Start spawner
            StartCoroutine(MoleSpawner());
        }

        if (time > 0.0f)
        {
            // Decrease timer
            time -= Time.deltaTime;
        }
    }

    private IEnumerator MoleSpawner()
    {
        // Spawn while there is time
        while (time > 0.0f)
        {
            // Get inactive moles
            IEnumerable<Mole> inactiveMoles = moles.Where(mole => mole.active == false);
            if (inactiveMoles.Count() > 0)
            {
                // Choose random inactive mole
                int moleIndex = Random.Range(0, inactiveMoles.Count());
                // Activate mole
                inactiveMoles.ElementAt(moleIndex).Show(moleStayDuration);
            }

            // Cooldown
            yield return new WaitForSeconds(moleSpawnCooldown);
        }
        Debug.Log("Finished Spawner");

        // Hide all moles
        foreach (Mole mole in moles)
        {
            mole.Hide();
        }
    }

    public void OnMoleHit()
    {
        score += 100;
    }
}
