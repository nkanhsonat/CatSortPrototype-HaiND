using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public int idBird;

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

    // SkeletonAnimation
    private SkeletonAnimation skeletonAnimation;


    void Start(){
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        SetIdle();
    }

    public void FlipBirdImage()
    {
        // Get transform of bird
        Transform birdTransform = GetComponent<Transform>();

        // Get scale of bird
        Vector3 birdScale = birdTransform.localScale;

        // Set scale of bird
        birdTransform.localScale =
            new Vector3(-birdScale.x, birdScale.y, birdScale.z);
    }

    // SkeletonAnimation skeletonAnimation;

    public void SetIdle(){
        skeletonAnimation.AnimationState.SetAnimation(0, idleAnimationName, true);
    }

    public void SetJumpLanding(){
        skeletonAnimation.AnimationState.SetAnimation(0, jumpAnimationName, false);
        //mix jump and landing, jump duration is 1s and landing duration is 0.5s
        skeletonAnimation.AnimationState.AddAnimation(0, landingAnimationName, false, 0.5f);
        SetIdle();
    }

    public void SetCheer(){
        skeletonAnimation.AnimationState.SetAnimation(0, cheerAnimationName, false);
    }

    public void SetSelect(){
        skeletonAnimation.AnimationState.SetAnimation(0, selectAnimationName, false);
    }
}
