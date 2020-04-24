using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Sprite explosionPrefab;
    public AudioClip deathNoise;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<AudioSource>().clip = deathNoise;
    }//this.GetComponent<AudioSource>().clip = deathNoise;
     //this.GetComponent<AudioSource>().Play();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = explosionPrefab;
        GetComponent<AudioSource>().Play();
    }
}
