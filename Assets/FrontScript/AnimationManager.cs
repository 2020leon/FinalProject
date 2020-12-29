using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public static bool currentAnimationEnded = true;
	public Queue<Animator> animators = new Queue<Animator>();

	public void EnqueueAnimator(Animator animator)
	{
		animators.Enqueue(animator);
	}

	void Update()
	{
		if (currentAnimationEnded)
        {
			if (animators.Count > 0)
			{
				Animator animator = animators.Dequeue();

				AnimationEvent animEvent = new AnimationEvent();
				animEvent.functionName = "onAnimationFinished";
				AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
				animEvent.time = clip.length;
				clip.AddEvent(animEvent);

				animator.gameObject.AddComponent<AnimationListener>();
				animator.SetTrigger("start");
				currentAnimationEnded = false;
			}
        }
	}

	class AnimationListener: MonoBehaviour
    {
        public void onAnimationFinished()
		{
			currentAnimationEnded = true;
			Destroy(this);
		}
	}
}
