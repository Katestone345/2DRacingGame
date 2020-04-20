using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio
public class VolSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void setVol( float vol) {
        audioMixer.setFloat("volume",vol);
    }
}
