using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class collision : MonoBehaviour
{
    public Text timer;
    public Sprite explosionPrefab;

    private void OnTriggerEnter2D( Collider2D other)
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = explosionPrefab;
        this.gameObject.SetActive(false);
       PlayerPrefs.SetString("score",timer.text);
       SceneManager.LoadScene("GameOver");
    }
}
