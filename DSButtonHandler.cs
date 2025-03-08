using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DSButtonHandler : MonoBehaviour
{
    [SerializeField] Image indicator;
    [SerializeField] RectTransform bgpanel;
    public bool DoAudio = true;
    public AudioSource a;
    public AudioClip a1;
    public AudioClip a2;
    public float ButtonsPerSecond;
    private Coroutine deploymentCoroutine;

    private List<BranchDialogue> currentArrayButtons;

    public GameObject selected;
    public GameObject buttonPrefab;

    public List<GameObject> buttons = new();
    public void ClearSelected()
    {
        selected = null;
    }
    public void DeployButtons(List<BranchDialogue> db)
    {
        foreach (BranchDialogue but in db){
            var bot = Instantiate(buttonPrefab, bgpanel);
            buttons.Add(bot);
            bot.GetComponentInChildren<TMP_Text>().text = but.label;
            Debug.Log("Deployed new button -- example line being deployed --:"+but.dialogues[0].NameKey);
            //lets keep this as-is for debug for now
        }
    }

    // SEPERATION
    public void PushButtons(List<BranchDialogue> bd)
    {
        if (deploymentCoroutine != null){
            StopCoroutine(deploymentCoroutine);
        }
        deploymentCoroutine = StartCoroutine(DeployButtonsCool(bd));
    }


    private IEnumerator DeployButtonsCool(List<BranchDialogue> bd)
    {
        currentArrayButtons = bd;
        float delay = 1f / ButtonsPerSecond;

        for (int i = 0; i < currentArrayButtons.Count; i++){
            var bot = Instantiate(buttonPrefab, bgpanel);
            ButtonSpawnAudio();
            buttons.Add(bot);
            bot.GetComponentInChildren<TMP_Text>().text = bd[i].label;
            Debug.Log("Deployed new button -- example line being deployed --:"+bd[i].dialogues[0].NameKey);
            LayoutRebuilder.ForceRebuildLayoutImmediate(bgpanel);
            yield return new WaitForSeconds(delay);
        }
        deploymentCoroutine = null;
    }
    // SEPERATION

    public void PurgeButtons()
    {
        foreach (GameObject but in buttons){
            var x = but;
            buttons.Remove(but);Destroy(x);
        }
    }

    public void PressButton()
    {
    }

    private void ButtonSpawnAudio()
    {
        if(DoAudio){
            AudioClip[] pot = {a1, a2};
            int z = Random.Range(1, 2);
            a.clip = pot[z]; a.Play();
        }
    }
}
