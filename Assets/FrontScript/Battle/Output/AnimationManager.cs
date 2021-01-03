using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
	public static bool currentAnimationEnded = true;
	public Queue<Animator> animators = new Queue<Animator>();

	public void EnqueueAnimator(Animator animator)
	{
		EnqueueAnimator(animator, (a) => { });
	}

	public delegate void AnimatorSetup(Animator animator);
	private Dictionary<Animator, List<AnimatorSetup>> animatorSetup = new Dictionary<Animator, List<AnimatorSetup>>();
	public void EnqueueAnimator(Animator animator, AnimatorSetup setup)
    {
		if (animatorSetup.ContainsKey(animator))
        {
			animatorSetup[animator].Add(setup);
        }
		else
        {
			List<AnimatorSetup> list = new List<AnimatorSetup>();
			list.Add(setup);
			animatorSetup.Add(animator, list);
        }

		animators.Enqueue(animator);
    }

	void Update()
	{
		if (currentAnimationEnded)
        {
			if (animators.Count > 0)
			{
				Animator animator = animators.Dequeue();

				animatorSetup[animator][0](animator);
				animatorSetup[animator].RemoveAt(0);
				if (animatorSetup[animator].Count == 0)
                {
					animatorSetup.Remove(animator);
                }

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

        private void OnDestroy()
        {
			currentAnimationEnded = true;
        }
    }
}
