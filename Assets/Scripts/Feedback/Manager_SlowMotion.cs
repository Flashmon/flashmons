using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Manager_SlowMotion : MonoBehaviour {

    public void SlowMo(float targetSpeed, float timeToGetToSpeed, float durationOfSlow)
    {
        Tween myTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, targetSpeed, timeToGetToSpeed);
        StartCoroutine(StopSlowMo(timeToGetToSpeed, durationOfSlow, myTween));
    }

    IEnumerator StopSlowMo(float timeToGetToSpeed, float durationOfSlow, Tween tween)
    {
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(durationOfSlow*Time.timeScale);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, timeToGetToSpeed/4);
    }
}
