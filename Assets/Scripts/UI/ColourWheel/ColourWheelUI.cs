using System;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ColourWheelUI : VisualElement
{
    [Serializable]
    public struct UIValues
    {
        public float radius;
        public float width;
        [Tooltip("Relative to screen")]
        public Vector2 center;
        public static UIValues Lerp(UIValues start, UIValues end, float t)
        {
            UIValues res = new UIValues();
            res.radius = ( 1 - t ) * start.radius + t * end.radius;
            res.width = ( 1 - t ) * start.width + t * end.width;
            res.center = ( 1 - t ) * start.center + t * end.center;
            return res;
        }
    }

    private UIValues values;
    public UIValues Values { get => values; set { values = value; MarkDirtyRepaint(); } }

    private Color? selectedColour;
    public Color? SelectedColour { get => selectedColour; set { selectedColour = value; MarkDirtyRepaint(); } }

    public ColourWheelUI()
    {
        this.values = new UIValues();
    }

    public ColourWheelUI(UIValues values)
    {
        this.values = values;
        generateVisualContent += GenerateVisualContent;
    }

    private void GenerateVisualContent(MeshGenerationContext ctx)
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        Painter2D painter = ctx.painter2D;
        painter.lineCap = LineCap.Butt;
        ColoursDB coloursDB = DataBase.Instance.Colours;
        float angle = 0.0f; float nextAngle = 0.0f;
        foreach(ColoursDB.ColourPair pair in coloursDB.List)
        {
            nextAngle += 180.0f / coloursDB.ColourPairCount;

            painter.strokeColor = pair.colour;
            painter.lineWidth = (painter.strokeColor == selectedColour ? 1.5f : 1.0f) * values.width;

            painter.BeginPath();
            painter.Arc(Vector2.Scale(values.center, screenSize), values.radius, angle, nextAngle);
            painter.Stroke();

            painter.strokeColor = pair.opposite;
            painter.lineWidth = (painter.strokeColor == selectedColour ? 1.5f : 1.0f) * values.width;

            painter.BeginPath();
            painter.Arc(Vector2.Scale(values.center, screenSize), values.radius, 180.0f + angle, 180.0f + nextAngle);
            painter.Stroke();

            angle = nextAngle;
        }
    }
}
