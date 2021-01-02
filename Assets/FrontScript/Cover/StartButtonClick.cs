using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalProject
{
    public class StartButtonClick : MonoBehaviour {
        public void OnClick() {
            SceneManager.LoadScene("BattleScene");
        }
    }
}
