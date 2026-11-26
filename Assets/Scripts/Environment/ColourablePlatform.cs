using UnityEngine;

public class ColourablePlatform : MonoBehaviour, IColourable
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private int colourIndex;

    public Renderer Renderer => sr;

    void Start()
    {
        (this as IColourable).SetColour(colourIndex);
    }
}
