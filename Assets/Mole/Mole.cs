using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Mole : MonoBehaviour
{
    public static float s_animationSpeed = 2.0f;
    public static bool s_soundActive = false;
    public static bool s_vibrationActive = false;

    public Action<int> moleHit;

    private Transform moleModel;
    private Collider moleCollider;
    private AudioSource appearSFX;
    private VibrationSource appearVib;
    private AudioSource hitSFX;

    private DateTime startTimestamp; // To measure time

    private void Awake()
    {
        moleModel = transform.Find("MoleModel");
        moleCollider = GetComponent<Collider>();
        appearSFX = transform.Find("AppearFX").GetComponent<AudioSource>();
        appearVib = transform.Find("AppearFX").GetComponent<VibrationSource>();
        hitSFX = transform.Find("HitFX").GetComponent<AudioSource>();
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(ShowSequence());
    }

    private IEnumerator ShowSequence()
    {
        moleCollider.enabled = true;
        if (s_soundActive)
        {
            appearSFX.Play();
        }
        if (s_vibrationActive)
        {
            appearVib.Play();
        }
        // Start stopwatch
        startTimestamp = DateTime.Now;
        // Animation
        yield return TweenPosition(new Vector3(0, 0.5f, 0));
    }

    // On mole hit
    private void OnTriggerEnter(Collider other)
    {
        DateTime endTimestamp = DateTime.Now;
        TimeSpan elapsedTime = endTimestamp.Subtract(startTimestamp);
        hitSFX.Play();
        Hide();
        moleHit.Invoke((int)elapsedTime.TotalMilliseconds);
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideSequence());
    }

    private IEnumerator HideSequence()
    {
        moleCollider.enabled = false;
        // Animation
        yield return TweenPosition(Vector3.zero);
    }

    // Tween to target position
    // Tweening animation advances X% per second until 100%
    // Animation speed %/sec
    private IEnumerator TweenPosition(Vector3 targetPosition)
    {
        Vector3 startingPosition = moleModel.localPosition;
        float animProgress = 0.0f;
        while (animProgress < 1.0f)
        {
            animProgress += s_animationSpeed * Time.deltaTime;
            moleModel.localPosition = Vector3.Lerp(startingPosition, targetPosition, animProgress);
            yield return null;
        }
    }
}
