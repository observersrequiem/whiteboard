using System;
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

    protected override void HandlePointerEnter()
    {
        base.HandlePointerEnter();
        dsbh.ButtonMoveAudio();
    }

    protected override void HandlePointerClick()
    {
        base.HandlePointerClick();
        if(dsbh.canSelect){ActedOn();}
    }
}
