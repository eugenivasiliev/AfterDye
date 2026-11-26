using Tween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ColourWheelUIComponent : MonoBehaviour
{
    private ColourWheelUI colourWheelUI;
    [SerializeField] private TweenCWValues colourWheelTween;
    private InputSystem_Actions input;
    [SerializeField] private bool isFocused;
    [SerializeField] private Color? selectedColour;
    [SerializeField] private PlayerController player;

    void Start()
    {
        colourWheelUI = new ColourWheelUI(colourWheelTween.Value);
        this.GetComponent<UIDocument>().rootVisualElement.Add(colourWheelUI);

        input = new InputSystem_Actions();
        input.Enable();

        input.Player.ColourWheel.started += (ctx) => isFocused = true;
        input.Player.ColourWheel.canceled += (ctx) => isFocused = false;
    }

    void Update()
    {
        if(isFocused)
        {
            selectedColour = GetSelectedColour(out int colourIndex);
            colourWheelUI.SelectedColour = selectedColour;
            (player as IColourable).SetColour(colourIndex);
        }

        colourWheelTween.Reverse(!isFocused);
        colourWheelTween.Update(Time.deltaTime);
        colourWheelUI.Values = colourWheelTween.Value;
    }

    private Color? GetSelectedColour(out int colourIndex)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 center = 0.5f * new Vector2(Screen.width, Screen.height);

        Vector2 mouseToCenter = mousePos - center;
        float angle = Mathf.Acos(mouseToCenter.x / mouseToCenter.magnitude);
        
        colourIndex = Mathf.FloorToInt(angle * DataBase.Instance.Colours.ColourPairCount / Mathf.PI) + 1;
        if(Mathf.Sign(mouseToCenter.y) == 1.0f) colourIndex -= DataBase.Instance.Colours.ColourPairCount + 1;

        return DataBase.Instance.Colours[colourIndex];
    }

}
