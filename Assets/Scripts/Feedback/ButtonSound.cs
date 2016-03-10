using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour {

    public float pitch;

    public AudioSource source;

    public Transform container;

    void Awake()
    {
        pitch = source.pitch;
        if(GameObject.Find("AudioContainer"))
        {
            container = GameObject.Find("AudioContainer").transform;
            source.transform.SetParent(container);
        }
    }

	public void PlaySound()
    {
        source.pitch = pitch + (Random.Range(0, 0.1f) - 0.05f);
        source.Play();
    }
}
