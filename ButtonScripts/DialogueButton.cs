using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueButton : ButtonInternal
{   
    public List<Dialogue> _dialogues;
    public DialogueSystem ds;
    public DSButtonHandler dsbh;

    protected override void ActedOn()
    {
        base.ActedOn();
        dsbh.PurgeButtons();
        ds.ComplexSequence(_dialogues);
    }
}
