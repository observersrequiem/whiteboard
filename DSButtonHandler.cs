using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DSButtonHandler : MonoBehaviour
{
    [SerializeField] Image indicator;
    [SerializeField] RectTransform bgpanel;
    [SerializeField] Image bgpanelImage;
    [SerializeField,Tooltip("Assign this.")] DialogueSystem ds;
    [SerializeField] TextDeployerBasic tdb;
    public bool DoAudio = true;
    public AudioSource a;
    public AudioClip a1;
    public AudioClip a2;
    public AudioClip a3;
    public AudioClip a4;
    public float ButtonsPerSecond;
    private Coroutine deploymentCoroutine;
    public bool DSBHInUse;
    private List<BranchDialogue> currentArrayButtons;
    public GameObject selected;
    public int currentIndex;
    public GameObject buttonPrefab;
    public bool canSelect = false;

    public List<GameObject> buttons = new();
    public void ClearSelected()
    {
        selected = null;
    }

    private void Start() {
        bgpanelImage = bgpanel.GetComponent<Image>();
    }

    /*/
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
    /*/
    public void PushButtons(List<BranchDialogue> bd)
    {
        if (deploymentCoroutine != null){
            StopCoroutine(deploymentCoroutine);
        }
        deploymentCoroutine = StartCoroutine(DeployButtonsCool(bd));
    }
    private IEnumerator DeployButtonsCool(List<BranchDialogue> bd)
    {
        yield return new WaitUntil(() => !tdb.isTyping);
        DSBHInUse = true;
        bgpanelImage.enabled = true;
        currentArrayButtons = bd;
        float delay = 1f / ButtonsPerSecond;
        for (int i = 0; i < currentArrayButtons.Count; i++){
            canSelect = false;
            var bot = Instantiate(buttonPrefab, bgpanel);
            ButtonSpawnAudio();
            buttons.Add(bot);
            bot.GetComponentInChildren<TMP_Text>().text = bd[i].label;
            bot.GetComponent<DialogueButton>().ds = ds;
            bot.GetComponent<DialogueButton>().dsbh = this;
            bot.GetComponent<DialogueButton>()._dialogues = bd[i].dialogues;
            LayoutRebuilder.ForceRebuildLayoutImmediate(bgpanel);
            yield return new WaitForSeconds(delay);
        }
        deploymentCoroutine = null;
        currentArrayButtons = null;
        canSelect = true;
    }
    public void PurgeButtons()
    {
        selected = null;
        bgpanelImage.enabled = false;
        ButtonPurgeAudio();
        for (int i = buttons.Count - 1; i >= 0; i--)
        {   
            Destroy(buttons[i]);
            buttons.RemoveAt(i);
        }
        canSelect = false;
        ds.CanNextFrame = true;
        DSBHInUse = false;
    }
    private void ButtonSpawnAudio()
    {
        if(DoAudio){
            AudioClip[] pot = {a1, a2};
            int z = Random.Range(1, 2);
            a.clip = pot[z]; a.Play();
        }
    }
    private void ButtonPurgeAudio()
    {
        if(DoAudio){
            a.clip = a3; a.Play();
        }
    }
    public void ButtonMoveAudio()
    {
        if(DoAudio){
            a.clip = a4; a.Play();
        }
    }
    // There's probably a better way to do this
    // Oh well :^)
    public void IndexDown(){
        if(buttons != null){
        ButtonMoveAudio();
        currentIndex++;
        if(currentIndex > buttons.Count - 1){
            currentIndex = 0;
            selected.GetComponent<ButtonInternal>().DSBHUnselected();
            selected = buttons[currentIndex];
            selected.GetComponent<ButtonInternal>().DSBHSelected();}
        else {selected.GetComponent<ButtonInternal>().DSBHUnselected();
            selected = buttons[currentIndex];
            selected.GetComponent<ButtonInternal>().DSBHSelected();}}
    }
    public void IndexUp(){
        if(buttons != null){
        ButtonMoveAudio();
        currentIndex--;
        if(currentIndex < 0){
            selected.GetComponent<ButtonInternal>().DSBHUnselected();
            currentIndex = buttons.Count - 1;
            selected = buttons[currentIndex];
            selected.GetComponent<ButtonInternal>().DSBHSelected();}
        else {selected.GetComponent<ButtonInternal>().DSBHUnselected();
            selected = buttons[currentIndex];
            selected.GetComponent<ButtonInternal>().DSBHSelected();}}
    }
    public void SelectDown(){
        if (canSelect){
            ButtonMoveAudio();
            if(buttons != null && !selected){
            currentIndex = buttons.Count - 1;
            selected = buttons[currentIndex];
            selected.GetComponent<ButtonInternal>().DSBHSelected();
            Debug.Log("DOWN START");}
        }
    }
    public void SelectUp(){
        if (canSelect){
            ButtonMoveAudio();
            if(buttons != null && !selected){
            currentIndex = 0;
            selected = buttons[currentIndex];
            selected.GetComponent<ButtonInternal>().DSBHSelected();
            Debug.Log("UP START");}
        }
    }
    public void ActSelected(){
        if (selected){
            selected.GetComponent<ButtonInternal>().DSBHActSelected();
        }
    }
}
