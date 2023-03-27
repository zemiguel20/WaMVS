using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Mole : MonoBehaviour
{
    public static float s_animationSpeed = 2.0f;
    public static bool s_soundActive = false;
    public static bool s_vibrationActive = false;

    public UnityEvent moleHit;

    public bool active { get; private set; }

    private Transform moleModel;
    private Collider moleCollider;
    private AudioSource appearSFX;
    private VibrationSource appearVib;
    private AudioSource hitSFX;

    [HideInInspector]public int mode = 3; // 0: only visual, 1: with sound, 2: with vibration, 3: both

    private void Awake()
    {
        moleModel = transform.Find("MoleModel");
        moleCollider = GetComponent<Collider>();
        appearSFX = transform.Find("AppearFX").GetComponent<AudioSource>();
        appearVib = transform.Find("AppearFX").GetComponent<VibrationSource>();
        hitSFX = transform.Find("HitFX").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnMoleHit();
    }

    private void OnMoleHit()
    {
        if (mode == 3 || mode == 1)
            hitSFX.Play();

        Hide();
        moleHit.Invoke();
    }

    // Activate mole for X time (in seconds)
    public void Show(float time)
    {
        StopAllCoroutines();
        StartCoroutine(ShowSequence(time));
    }

    private IEnumerator ShowSequence(float time)
    {
        active = true;
        moleCollider.enabled = true;
        if(s_soundActive)
        {
            appearSFX.Play();
        }
        if(s_vibrationActive)
        {
            appearVib.Play();
        }
        yield return TweenPosition(new Vector3(0, 0.5f, 0));
        yield return new WaitForSeconds(time);
        Hide();
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideSequence());
    }

    private IEnumerator HideSequence()
    {
        // Disable trigger so player cant hit twice
        moleCollider.enabled = false;
        // Play hide animation
        yield return TweenPosition(Vector3.zero);
        // Set inactive once animation finished
        active = false;
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
