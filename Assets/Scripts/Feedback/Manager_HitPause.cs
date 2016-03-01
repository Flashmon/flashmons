using UnityEngine;
using System.Collections;

public class Manager_HitPause : MonoBehaviour {

	public void HitPause()
    {
        Time.timeScale = 0.0001f;
        StartCoroutine(EndPause());
    }

    IEnumerator EndPause()
    {
        yield return new WaitForSeconds(0.03f * Time.timeScale);
        Time.timeScale = 1;
    }

}
