using System;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ColourWheelUI : VisualElement
{
    [Serializable]
    public struct Values
    {
        public float radius;
        public float width;
        public float padding;
        public Vector2 center => (radius + width / 2 + padding) * Vector2.one;
    }

    private Values values;

    public ColourWheelUI()
    {
        this.values = new Values();
    }

    public ColourWheelUI(Values values)
    {
        this.values = values;
        generateVisualContent += GenerateVisualContent;
    }

    private void GenerateVisualContent(MeshGenerationContext ctx)
    {
        Painter2D painter = ctx.painter2D;
        painter.lineWidth = values.width;
        painter.lineCap = LineCap.Butt;
        ColoursDB coloursDB = DataBase.Instance.Colours;
        float angle = 0.0f; float nextAngle = 0.0f;
        foreach(ColoursDB.ColourPair pair in coloursDB.List)
        {
            nextAngle += 180.0f / coloursDB.ColourPairCount;

            painter.strokeColor = pair.colour;

            painter.BeginPath();
            painter.Arc(values.center, values.radius, angle, nextAngle);
            painter.Stroke();

            painter.strokeColor = pair.opposite;

            painter.BeginPath();
            painter.Arc(values.center, values.radius, 180.0f + angle, 180.0f + nextAngle);
            painter.Stroke();

            angle = nextAngle;
        }
    }

    public void SetValues(Values values)
    {
        this.values = values;
        MarkDirtyRepaint();
    }
}
