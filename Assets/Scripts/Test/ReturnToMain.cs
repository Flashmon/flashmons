using UnityEngine;
using System.Collections;

public class ReturnToMain : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
	}
}
