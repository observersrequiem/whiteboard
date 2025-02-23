using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [SerializeField]
    public string TextKey;

    [SerializeField]
    public string NameKey;

    [SerializeField]
    public string AnimKey;

    [SerializeField]
    public DialogueAction Action;

    public Dialogue(string DialogueKey, string NametagKey, string AnimationKey, DialogueAction DiaAction)
    {
        TextKey = DialogueKey;
        NameKey = NametagKey;
        AnimKey = AnimationKey;
        Action = DiaAction;
    }
}
