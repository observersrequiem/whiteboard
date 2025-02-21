using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string TextKey { get; private set; }
    public string NameKey { get; private set; }
    public string AnimKey { get; private set; }

    public Dialogue(string DialogueKey, string NametagKey, string AnimationKey)
    {
        TextKey = DialogueKey;
        NameKey = NametagKey;
        AnimKey = AnimationKey;
    }
}
