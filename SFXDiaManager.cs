using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SFXDiaManager : MonoBehaviour
{
    //sfx
    public List<string> keys;
    public List<AudioClip> audioClips;
    //bgm
    
    public List<string> musicKeys;
    public List<AudioClip> musicLoops;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource musicSource;

    Dictionary<string, AudioClip> audioDictionary;
    Dictionary<string, AudioClip> musicDictionary;

    void Awake(){
        audioDictionary = keys
    .Zip(audioClips, (key, clip) => new { key, clip }) 
    .ToDictionary(pair => pair.key, pair => pair.clip);
        musicDictionary = musicKeys
    .Zip(musicLoops, (key, clip) => new { key, clip }) 
    .ToDictionary(pair => pair.key, pair => pair.clip);
    }

    public void PlaySfx(string key){
        audioSource.clip = audioDictionary[key];
        audioSource.Play();
    }

    public void PlayBGM(string key){
        if (key == "silence") {
            musicSource.Stop();
            musicSource.clip = null;
        } else {
            musicSource.clip = musicDictionary[key];
            musicSource.Play();
        }
    }

}
