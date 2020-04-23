using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class collision : MonoBehaviour
{
    public Text timer;
   private void OnTriggerEnter2D( Collider2D other) {
       this.gameObject.SetActive(false);
       PlayerPrefs.SetString("score",timer.text);
       SceneManager.LoadScene("GameOver");
   }
}
