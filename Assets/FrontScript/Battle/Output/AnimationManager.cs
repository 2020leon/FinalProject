using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
	//public static bool currentAnimationEnded = true;
	public static short currentAnimationCount = 0;
	private static string statusChangeAnimatorName = "StatusChange";
	private static string explodeChangeAnimatorName = "Explode";
	public Queue<Animator> animators = new Queue<Animator>();

	public void EnqueueAnimator(Animator animator)
	{
		EnqueueAnimator(animator, (a) => { });
	}

	public void EnqueueAnimator(Animator animator, AnimatorSetup setup)
	{

		EnqueueAnimator(animator, setup, () => { });
	}

	public delegate void AnimatorSetup(Animator animator);
	public delegate void AnimationEnd();
	private Dictionary<Animator, List<AnimatorSetup>> animatorSetup = new Dictionary<Animator, List<AnimatorSetup>>();
	private Dictionary<Animator, List<AnimationEnd>> animationEnd = new Dictionary<Animator, List<AnimationEnd>>();

	public void EnqueueAnimator(Animator animator, AnimatorSetup setup, AnimationEnd end)
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

		if (animationEnd.ContainsKey(animator))
		{
			animationEnd[animator].Add(end);
		}
		else
		{
			List<AnimationEnd> list = new List<AnimationEnd>();
			list.Add(end);
			animationEnd.Add(animator, list);
		}

		animators.Enqueue(animator);
	}



	void Update()
	{
		if (/*currentAnimationEnded && */currentAnimationCount == 0)
        {
			while (animators.Count > 0)
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
				animator.gameObject.GetComponent<AnimationListener>().end = animationEnd[animator][0];
				animationEnd[animator].RemoveAt(0);
				if (animationEnd[animator].Count == 0)
                {
					animationEnd.Remove(animator);
                }

				animator.SetTrigger("start");
				currentAnimationCount++;
				if (animators.Count > 0) {
					var peek = animators.Peek();
					if (!(
						((peek.name == animator.name || peek.name == explodeChangeAnimatorName) && animator.name == statusChangeAnimatorName) ||
						(animator.name == explodeChangeAnimatorName && peek.name == statusChangeAnimatorName)
					)) {
						break;
					}
				}
			}
        }
	}

	class AnimationListener: MonoBehaviour
    {
		public AnimationEnd end;
        public void onAnimationFinished()
		{
			try {
				end();
			} catch(System.NullReferenceException e) {
				Debug.Log(e);
			}
			//currentAnimationEnded = true;
			Destroy(this);
		}

        private void OnDestroy()
        {
			try {
				end();
			} catch(System.NullReferenceException e) {
				Debug.Log(e);
			}
			currentAnimationCount--;
			//currentAnimationEnded = true;
        }
    }
}
