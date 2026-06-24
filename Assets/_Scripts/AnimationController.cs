using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerController playerController;

    private string currentAnimation;
    private void Awake()
    {
        if (playerController == null) playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ReadPlayerStateAndAnimate();
    }

    // Update is called once per frame
    void Update()
    {
        ReadPlayerStateAndAnimate();
    }
    private void ReadPlayerStateAndAnimate()
    {
        if (animator == null)
        {
            return;
        }
        switch (playerController.state)
        {
            case PlayerState.Idle:
                ChangeAnimation("a_Idle");
                break;
            case PlayerState.Moving:
                ChangeAnimation("a_Walking");
                break;
            case PlayerState.Running:
                ChangeAnimation("a_Running");
                break;
            case PlayerState.Jumping:
                ChangeAnimation("Jump", 0);
                break;
            case PlayerState.Falling:
                ChangeAnimation("FallingToLanding");
                break;
            default:
                break;
        }
    }
    private void ChangeAnimation(string animationName, float crossfade = 0.2f)
    {
        if (currentAnimation != animationName)
        {
            currentAnimation = animationName;
            animator.CrossFade(animationName, crossfade);
        }
    }
}
