using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ColourWheelUIComponent : MonoBehaviour
{
    private ColourWheelUI colourWheelUI;
    [SerializeField] private ColourWheelUI.Values colourWheelValues;

    void Start()
    {
        colourWheelUI = new ColourWheelUI(colourWheelValues);
        this.GetComponent<UIDocument>().rootVisualElement.Add(colourWheelUI);
    }

#if UNITY_EDITOR
    void Update()
    {
        colourWheelUI.SetValues(colourWheelValues);
    }
#endif

}
