using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
	public class ResolutionSet : MonoBehaviour {

		private static int width = 16;
		private static int height = 9;
		private static float ratio = (float)width / height;

		// Use this for initialization
		private void Start () {
			var ratio = (float)Screen.width / Screen.height;
			if (ratio < ResolutionSet.ratio) {
				Screen.SetResolution(Screen.width, height * Screen.width / width, Screen.fullScreen);
			}
			else if (ratio > ResolutionSet.ratio) {
				Screen.SetResolution(width * Screen.height / height, Screen.height, Screen.fullScreen);
			}
		}
	}
}