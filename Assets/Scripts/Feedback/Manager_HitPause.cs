using UnityEngine;
using System.Collections;

public class Manager_HitPause : MonoBehaviour {

    public void HitPause(float duration)
    {
        Time.timeScale = 0.0001f;
        StartCoroutine(EndPause(duration));
    }

    IEnumerator EndPause(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
    }

}
