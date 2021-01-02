using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFieldDestroy : MonoBehaviour {

    void Hide()
    {
        gameObject.transform.parent.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
    }

    void Destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
