using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler
{
    private Animator _animator;

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.Play("Shown");
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        AddEvents();
    }
    #region animation events
    private void AddEvents()
    {
        for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];
            Debug.Log(clip.name);

            AnimationEvent animationStartEvent = new AnimationEvent();
            animationStartEvent.time = 0;
            animationStartEvent.functionName = "AnimationStartHandler";
            animationStartEvent.stringParameter = clip.name;

            AnimationEvent animationEndEvent = new AnimationEvent();
            animationEndEvent.time = clip.length;
            animationEndEvent.functionName = "AnimationCompleteHandler";
            animationEndEvent.stringParameter = clip.name;

            clip.AddEvent(animationStartEvent);
            clip.AddEvent(animationEndEvent);

        }
    }

    public void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} animation start.");
    }

    public void AnimationCompleteHandler(string name)
    {
        Debug.Log($"{name} animation end.");
    }
    #endregion

}
