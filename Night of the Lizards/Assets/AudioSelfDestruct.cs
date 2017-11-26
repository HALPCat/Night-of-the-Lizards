using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        AudioSource attackAudio = GetComponent<AudioSource>();
        attackAudio.pitch = UnityEngine.Random.Range(0.75f, 1.25f);
        attackAudio.Play();
        StartCoroutine(SelfDestruct());
	}

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
