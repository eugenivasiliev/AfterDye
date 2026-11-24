using UnityEngine;

public interface IColourable
{
    public Renderer Renderer { get; }
    public void SetColour(int colourIndex)
    {
        Color? nullableColour = DataBase.Instance.Colours[colourIndex];
        if(nullableColour is Color colour) Renderer.material.SetColor("_Color", colour);
    }
}
