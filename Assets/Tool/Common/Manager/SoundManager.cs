using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;

    /*public AudioSource effect;
    public AudioSource background;
    public Dictionary<string, AudioClip> clipDict = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();*/

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /*private void Start() {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music/Effect");
        for (int i = 0; i < clips.Length; i++) {
            clipDict.Add(clips[i].name, clips[i]);
        }
        AudioClip[] musics = Resources.LoadAll<AudioClip>("Music/Music");
        for (int i = 0; i < musics.Length; i++) {
            musicDict.Add(clips[i].name, clips[i]);
        }
    }

    public void PlayEffect(string clipName) {
        if (!clipDict.ContainsKey(clipName))
            return;
        effect.clip = clipDict[clipName];
        effect.Play();
    }

    public void PlayBackgroundMusic(string musicName)
    {
        if (!musicDict.ContainsKey(musicName))
            return;
        background.clip = clipDict[musicName];
        background.Play();
    }*/
}
