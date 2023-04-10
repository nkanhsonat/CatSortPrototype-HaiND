using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

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
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        SetIdle();
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
}
