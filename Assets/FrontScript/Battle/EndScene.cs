using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {
    void End()
    {
        SceneManager.LoadScene("CoverScene");
    }
}
