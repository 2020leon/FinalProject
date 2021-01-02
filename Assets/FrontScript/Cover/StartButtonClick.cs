using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalProject
{
    public class StartButtonClick : MonoBehaviour {

        private bool isFadingIn;
        private bool isFadingOut;
        private CanvasGroup canvasGroup;

        private void Start() {
            isFadingOut = false;
            isFadingIn = true;
            canvasGroup = transform.parent.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        private void Update() {
            if (isFadingIn && canvasGroup.alpha < 1) {
                canvasGroup.alpha += 1f / 64f;
            }
            if (isFadingIn && canvasGroup.alpha == 1) {
                isFadingIn = false;
            }
            if (isFadingOut && canvasGroup.alpha > 0) {
                canvasGroup.alpha -= 1f / 128f;
            }
            if (isFadingOut && canvasGroup.alpha == 0) {
                SceneManager.LoadScene("BattleScene");
                isFadingOut = false;
            }
        }

        public void OnClick() {
            if (!isFadingIn && !isFadingOut) {
                isFadingOut = true;
            }
        }
    }
}
