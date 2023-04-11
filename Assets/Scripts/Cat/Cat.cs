using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using DG.Tweening;

public class Cat : MonoBehaviour
{
    public int idCat;

#region
    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string jumpAnimationName;

    [SpineAnimation]
    public string landingAnimationName;

    [SpineAnimation]
    public string cheerAnimationName;

    [SpineAnimation]
    public string selectAnimationName;
#endregion


    private SkeletonAnimation skeletonAnimation;

    void Start()
    {
        Transform transform = GetComponent<Transform>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        SetIdle();
    }

    public void FlipCat()
    {
        transform.Rotate(0, 180, 0);
    }

    public void SetIdle()
    {
        skeletonAnimation
            .AnimationState
            .SetAnimation(0, idleAnimationName, true);
    }

    public void SetSelect()
    {
        skeletonAnimation
            .AnimationState
            .SetAnimation(0, selectAnimationName, true);
    }

    public void SetJumpAndLanding()
    {
        skeletonAnimation.AnimationState.SetAnimation(1, jumpAnimationName, false);
        skeletonAnimation.AnimationState.AddAnimation(1, landingAnimationName, false, 0.5f);
        SetIdle();
    }

    public void SetCheer()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, cheerAnimationName, true);
    }
}
