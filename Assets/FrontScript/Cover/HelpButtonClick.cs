using UnityEngine;

namespace FinalProject
{
    public class HelpButtonClick : MonoBehaviour {
        public void OnClick() {
            Debug.Log($"file://{Application.streamingAssetsPath}/Rule.html");
			Application.OpenURL($"file://{Application.streamingAssetsPath}/Rule.html");
        }
    }
}
