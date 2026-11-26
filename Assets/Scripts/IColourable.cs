using UnityEngine;

public interface IColourable
{
    public Renderer Renderer { get; }
    public void SetColour(int colourIndex)
    {
        Color? nullableColour = DataBase.Instance.Colours[colourIndex];
        if(nullableColour is Color colour) Renderer.material.SetColor("_Color", colour);
    }
    public Color currentColour => Renderer.material.GetColor("_Color");
    public bool GetCurrentColourIndex(out int colourIndex) => DataBase.Instance.Colours.GetColourIndex(currentColour, out colourIndex);
}
