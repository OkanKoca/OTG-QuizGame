using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteScript : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject muteMusicSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MuteMusic()
    {
        muteMusicSprite.SetActive(false);
        audioSource.volume = 0;
    }
    public void MusicOn()
    {
        muteMusicSprite.SetActive(true);
        audioSource.volume = 0.1f;
    }
}
