using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;

    public AudioSource effect;
    public Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
    }

    private void Start() {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music/Effect");
        for (int i = 0; i < clips.Length; i++) {
            audioDict.Add(clips[i].name, clips[i]);
        }
    }

    public void PlayEffect(string clipName) {
        if (!audioDict.ContainsKey(clipName))
            return;
        effect.clip = audioDict[clipName];
        effect.Play();
    }

    public void PlayerHitEffect() {
        int effid = Random.Range(1, 4);
        PlayEffect("hit" + effid);
    }
}
