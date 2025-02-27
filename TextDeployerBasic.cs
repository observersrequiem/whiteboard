using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextDeployerBasic : MonoBehaviour
{
     // Hello, anyone reading this :-)
     // XOXO 18.02.2025 20:55 TURKEY !!!
     // 333 - 333 - 333

    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private float charactersPerSecond = 30;
    [SerializeField] private Image indicator;
    [SerializeField] private TMP_Text nameText;
    public bool isTyping;
    private Coroutine typingCoroutine;
    private string currentFullText;
    [SerializeField]private AudioSource player;
    [SerializeField]private AudioClip sfx1;
    [SerializeField]private AudioClip sfx2;
    [SerializeField]private AudioClip sfx3;
    [SerializeField]private AudioClip skip;
    

    public void Skip()
    {
        if (typingCoroutine != null && isTyping == true)
            {
                StopCoroutine(typingCoroutine);
                textComponent.text = currentFullText;
                typingCoroutine = null;
                isTyping = false;
                indicator.enabled = true;
                PlaySkipAudio();
            }
    }   

    public void StartTyping(string dialogue){
        if (typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
            isTyping = true;
            indicator.enabled = false;
        }
        typingCoroutine = StartCoroutine(TypeText(dialogue));
        isTyping = true;
        indicator.enabled = false;
    }
    public void SetName(string name){
        nameText.text = name;
    }
    // Typewriter effect :D
    private IEnumerator TypeText(string fullText) {
        currentFullText = fullText; // reference is stored here for use
        textComponent.text = "";
        float delay = 1f / charactersPerSecond;

        for (int i = 0; i < fullText.Length; i++) {
            textComponent.text += fullText[i];
            PlayAudio();
            yield return new WaitForSeconds(delay);
        }
        typingCoroutine = null; // when finished naturally, it clears itself.
        isTyping = false; //for skipping
        indicator.enabled = true;
    }

    private void PlayAudio()
    {
        AudioClip[] pot = {sfx1,sfx2,sfx3};
        int z = Random.Range(1,3);
        AudioClip x = pot[z];
        player.clip = x;
        player.Play();
    }

    private void PlaySkipAudio()
    {
        player.clip = skip;
        player.Play();
    }
}
