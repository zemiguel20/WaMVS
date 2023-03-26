using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class GameController : MonoBehaviour
{
    [Range(0, 3)]
    [SerializeField] private int experimentMode = 3; // 0: only visual, 1: with sound, 2: with vibration, 3: both
    [SerializeField] private int runDuration;
    [SerializeField] private float moleDuration;
    [SerializeField] private float moleSpawnCooldown;
    [SerializeField] private List<Mole> moles;

    public int score { get; private set; }
    public float time { get; private set; } // Run timer

    private float currentMoleSpawnCD;
    private float currentMoleActiveDuration;

        // Update is called once per frame
        void Update()
    {
        // T FOR TEST ACTIVATION
        if(time <= 0.0f && Keyboard.current.tKey.wasPressedThisFrame)
        {
            foreach (Mole mole in moles)
            {
                mole.Show(3.0f);
            }
        }

        // Run start/restart
        if (time <= 0.0f && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Reset score
            score = 0;
            // Reset timer
            time = runDuration;
            // Reset moles
            experimentMode = UIBoard.Instance.currentMode;
            foreach (Mole mole in moles)
            {
                mole.mode = experimentMode;
                mole.Hide();
            }
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
                Debug.Log("Spawning Mole");
                inactiveMoles.ElementAt(moleIndex).Show(moleDuration);
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
