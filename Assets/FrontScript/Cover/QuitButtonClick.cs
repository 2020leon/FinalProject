using UnityEngine;

namespace FinalProject
{
    public class QuitButtonClick : MonoBehaviour {
        public void OnClick() {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
