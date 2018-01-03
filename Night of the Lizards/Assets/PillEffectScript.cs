using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillEffectScript : MonoBehaviour {

    Animator anim;
    SpriteRenderer sr;

    void Start () {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        anim.Play("Inactive", 0);

    }
	
	// Update is called once per frame
	void Update () {
        /* Debug
        if (Input.GetKeyDown(KeyCode.I)) {
            PlayFX();
        }
        */

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Active")) {
            sr.enabled = true;
        }else {
            sr.enabled = false;
        }
    }

    public void PlayFX()
    {
        anim.Play("Active", 0);
    }
}
