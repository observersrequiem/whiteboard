using UnityEngine;

[System.Serializable]
public class VarChange
{
    //key object pair
    public enum TypeF
    {
        String,
        Int,
        Float,
        Bool
    }

    public TypeF type;

    public string key;
    public int intValue;
    public float floatValue;
    public bool boolValue;
    public string stringValue;
    public object value
    {
        get
        {
            return type switch
            {
                TypeF.Int => intValue,
                TypeF.Float => floatValue,
                TypeF.Bool => boolValue,
                TypeF.String => stringValue,
                _ => null,
            };
        }
        set
        {
            if (value is int) { type = TypeF.Int; intValue = (int)value;}
            else if (value is float) { type = TypeF.Float; floatValue = (float)value;}
            else if (value is bool) { type = TypeF.Bool; boolValue = (bool)value;}
            else if (value is string) { type = TypeF.String; stringValue = (string)value;}
            else throw new System.ArgumentException("Unsupported type");
        }
    }
}
