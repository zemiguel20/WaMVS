using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Mole : MonoBehaviour
{
    const float ANIMATION_SPEED = 2.0f;

    public UnityEvent moleHit;

    public bool active { get; private set; }

    private Transform moleModel;
    private Collider moleCollider;
    private AudioSource appearSFX;
    private AudioSource hitSFX;

    private void Awake()
    {
        moleModel = transform.Find("MoleModel");
        moleCollider = GetComponent<Collider>();
        appearSFX = transform.Find("AppearFX").GetComponent<AudioSource>();
        hitSFX = transform.Find("HitFX").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnMoleHit();
    }

    private void OnMoleHit()
    {
        hitSFX.Play();
        Hide();
        moleHit.Invoke();
    }

    // Activate mole for X time (in seconds)
    public void Show(float time)
    {
        Debug.Log(name + " showing");
        StopAllCoroutines();
        StartCoroutine(ShowSequence(time));
    }

    private IEnumerator ShowSequence(float time)
    {
        active = true;
        moleCollider.enabled = true;
        appearSFX.Play();
        yield return TweenPosition(new Vector3(0, 0.5f, 0), ANIMATION_SPEED);
        yield return new WaitForSeconds(time);
        Hide();
    }

    public void Hide()
    {
        Debug.Log(name + " hiding");
        StopAllCoroutines();
        StartCoroutine(HideSequence());
    }

    private IEnumerator HideSequence()
    {
        // Disable trigger so player cant hit twice
        moleCollider.enabled = false;
        // Play hide animation
        yield return TweenPosition(Vector3.zero, ANIMATION_SPEED);
        // Set inactive once animation finished
        active = false;
    }

    // Tween to target position
    // Tweening animation advances X% per second until 100%
    // Animation speed %/sec
    private IEnumerator TweenPosition(Vector3 targetPosition, float animSpeed)
    {
        Vector3 startingPosition = moleModel.localPosition;
        float animProgress = 0.0f;
        while (animProgress < 1.0f)
        {
            animProgress += animSpeed * Time.deltaTime;
            moleModel.localPosition = Vector3.Lerp(moleModel.localPosition, targetPosition, animProgress);
            yield return null;
        }
    }
    

    
}
