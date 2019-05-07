using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

    AudioSource netSound;

    
    void Start()
    {
        netSound = this.GetComponent<AudioSource>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            netSound.Play();
        }
    }
}
