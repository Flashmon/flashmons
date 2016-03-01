using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlowMoValues : MonoBehaviour {

    public float duration;
    public float targetSpeed;
    public float tweenDuration;

    public Manager_SlowMotion slowMo;

    public Text display;

    public void StartSlowMo()
    {
        slowMo.SlowMo(targetSpeed, tweenDuration, duration);
    }

    void FixedUpdate()
    {
        display.text = Time.timeScale.ToString();
    }

}
