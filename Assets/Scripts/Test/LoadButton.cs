using UnityEngine;
using System.Collections;

public class LoadButton : MonoBehaviour {

	public void LoadAScene(int i)
    {
        Application.LoadLevel(i);
    }
}
