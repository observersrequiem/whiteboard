using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected DialogueSystem ds;
    private void Start() {
        ds = GameObject.FindGameObjectWithTag("VNUI").GetComponent<DialogueSystem>();
    }
    protected virtual void Interact()
    {
        //Stuff stuff stuff stuff here
    }
}
