using UnityEngine;
using System.Collections;

public class AnimationWatcher : MonoBehaviour
{
    private PlayerShooting _playerShooting;
    public Animator animator;
    public string animationStateName;
    //public bool animationFinished = true;
    void Start()
    {
        _playerShooting = gameObject.GetComponent<Transform>().parent.GetComponent<PlayerShooting>();
        _playerShooting._animationWatcher = this;
        _playerShooting._animator = animator;
        _playerShooting.clipLength = GetClipLength(animationStateName);
    }

    IEnumerator WatchAnimationLoop()
    {
        //animationFinished = false;


        animator.Play(animationStateName, 0, 0f);

        float clipLength = GetClipLength(animationStateName);
        yield return new WaitForSeconds(clipLength / animator.speed);
        if (!_playerShooting.dragonSpawner.isPaused)
        {
            OnAnimationLoopEnd();
        }
        //animationFinished = true;
    }

    public float GetClipLength(string clipName)
    {
        // Finds the length of the clip by name
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }
        Debug.LogWarning("Animation clip not found: " + clipName);
        return 0f;
    }
    public void StartAnimation()
    {
        StartCoroutine(WatchAnimationLoop());
    }
    void OnAnimationLoopEnd()
    {
        _playerShooting.Shoot();
    }
}
