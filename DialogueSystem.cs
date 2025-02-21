using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using NUnit.Framework.Internal;
using Unity.Mathematics;

public class DialogueSystem : MonoBehaviour
{
     [SerializeField]
    Dictionary<string, int> animkeys = new Dictionary<string, int> (){
        { "none", 0 },
        { "defo", 1 },
        { "tagged", 2 },
    };

    public TextDeployerBasic tdb;
    public Animator anim;
    public List<Dialogue> dialogues = new();

    private int currentIndex;

    public void PushDialogueBasic() 
    {
        anim.SetInteger("AnimID",animkeys[dialogues[currentIndex].AnimKey]);
        tdb.StartTyping(dialogues[currentIndex].TextKey);tdb.SetName(dialogues[currentIndex].NameKey);
    }

    public void BeginSequence(List<Dialogue> chatter) {
        dialogues.Clear();
        currentIndex = 0;
        dialogues = chatter;
        PushDialogueBasic();
    }

    void EndSequence() {
        // clears all lists
        // clears the dialoguebox
        // clears anim final
        dialogues.Clear();
        tdb.StartTyping("");
        tdb.SetName("");
        anim.SetInteger("AnimID", 0);
        //add anything necessary at the bottom of the list
    }

    public void NextFrame()
    {
        if (currentIndex > dialogues.Count && !tdb.isTyping) {
            Debug.Log("Sequence over");
            EndSequence();
        } else {
            if(currentIndex <= dialogues.Count && !tdb.isTyping) {
                Debug.Log("One frame ahead");
                currentIndex += 1;
                PushDialogueBasic(); //add relevant information!
            } else {tdb.Skip(); Debug.Log("Skipped");}
        }
    }

}
