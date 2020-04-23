using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Text txt;
   void Awake() {
       txt.text = PlayerPrefs.GetString("score").Substring(6);
   }
}
