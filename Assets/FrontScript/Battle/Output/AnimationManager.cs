using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
	//public static bool currentAnimationEnded = true;
	public static short currentAnimationCount = 0;
	private const string statusChangeAnimatorName = "StatusChange";
	private const string explodeChangeAnimatorName = "Explode";
	public Queue<Animator> animators = new Queue<Animator>();

	//audio
	[SerializeField]
	private AudioClip atkAudio;
	[SerializeField]
	private AudioClip spellAudio;
	[SerializeField]
	private AudioClip fakeNewsAudio;
	[SerializeField]
	private AudioClip earthquakeAudio;
	[SerializeField]
	private AudioClip explodeAudio;
	private AudioSource audioSource;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
	}

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
				// audio start
				/*if (animator.name == atkAnimatorName) {
					GetComponent<AudioSource>().PlayOneShot(atkAudio, 1);
				}
				else if (animator.name == spellAnimatorName) {
					GetComponent<AudioSource>().PlayOneShot(spellAudio, 1);
				}*/
				switch (animator.name) {
					case "同志遊行":
					case "中天新聞":
					case "通貨膨脹":
						audioSource.PlayOneShot(spellAudio, 1);
						break;
					case "假新聞":
						audioSource.PlayOneShot(fakeNewsAudio ,1);
						break;
					case "大地震":
						audioSource.PlayOneShot(earthquakeAudio, 1);
						break;
					case "侯友宜L": // all minion atk
						audioSource.PlayOneShot(atkAudio, 1);
						break;
					case explodeChangeAnimatorName:
						audioSource.PlayOneShot(explodeAudio, 1);
						break;
				}
				// audio end
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
