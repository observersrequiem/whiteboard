using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

public class SFXDiaManager : MonoBehaviour
{

    public List<string> keys;
    public List<AudioClip> audioClips;
    [SerializeField] AudioSource audioSource;

    Dictionary<string, AudioClip> audioDictionary;

    void Awake(){
        audioDictionary = keys
    .Zip(audioClips, (key, clip) => new { key, clip }) 
    .ToDictionary(pair => pair.key, pair => pair.clip);
    }

    public void PlaySfx(string key){
        audioSource.clip = audioDictionary[key];
        audioSource.Play();
    }

}
