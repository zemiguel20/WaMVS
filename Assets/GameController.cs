using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private int runDuration;
    [SerializeField] private float startMoleSpawnCD;
    [SerializeField] private float endMoleSpawnCD;
    [SerializeField] private float startMoleActiveDuration;
    [SerializeField] private float endMoleActiveDuration;
    [SerializeField] private List<Mole> moles;

    public int score { get; private set; }
    public float time { get; private set; } // Run timer

    private float currentMoleSpawnCD;
    private float currentMoleActiveDuration;

    // Update is called once per frame
    void Update()
    {
        // Run start/restart
        if (time <= 0.0f && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Reset score
            score = 0;
            // Reset timer
            time = runDuration;
            // Reset Spawner
            currentMoleSpawnCD = startMoleSpawnCD;
            currentMoleActiveDuration = startMoleActiveDuration;
            // Reset moles
            foreach (Mole mole in moles)
            {
                mole.Hide();
            }
            // Start spawner
            StartCoroutine(MoleSpawner());
        }

        if (time > 0.0f)
        {
            // Decrease timer
            time -= Time.deltaTime;
            // Ramp up spawner
            float factor = 1 - (time / runDuration);
            currentMoleSpawnCD = Mathf.Lerp(startMoleSpawnCD, endMoleSpawnCD, factor);
            currentMoleActiveDuration = Mathf.Lerp(startMoleActiveDuration, endMoleActiveDuration, factor);
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
                Debug.Log("Spawning Mole");
                inactiveMoles.ElementAt(moleIndex).Show(currentMoleActiveDuration);
            }

            // Cooldown
            yield return new WaitForSeconds(currentMoleSpawnCD);
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
        Debug.Log(score);
    }
}
