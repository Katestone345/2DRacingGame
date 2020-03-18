using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   public void startGame () {
       SceneManager.LoadScene("RaceScene");
   }
   public void quitGame(){
       Application.Quit();
   }
}
