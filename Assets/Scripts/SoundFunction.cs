using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFunction : MonoBehaviour
{
    public string m_SoundPath;
    bool IsPlaying = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (null == audio)
            return;

        if (audio.isPlaying)
        {
            IsPlaying = true;
            return;
        }

        if(IsPlaying)
        {
            IsPlaying = false;
            SoundsPool.get().Recycle(gameObject);
        }
    }
}
