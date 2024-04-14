using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

public partial class VJKnobInput : VisualElement
{
    #region USS class name

    public static readonly string ussClassName = "vj-knob__input";

    #endregion

    #region Runtime public property

    public float NormalizedValue { get; set; }

    #endregion

    #region Custom style properties

    static CustomStyleProperty<int> _lineWidthProp
      = new CustomStyleProperty<int>("--line-width");

    static CustomStyleProperty<Color> _secondaryColorProp
      = new CustomStyleProperty<Color>("--secondary-color");

    int _lineWidth = 10;
    Color _secondaryColor = Color.gray;

    #endregion

    #region Visual element implementation

    public VJKnobInput()
    {
        AddToClassList(ussClassName);
        RegisterCallback<CustomStyleResolvedEvent>(UpdateCustomStyles);
        generateVisualContent += GenerateVisualContent;
    }

    void UpdateCustomStyles(CustomStyleResolvedEvent e)
    {
        var (style, dirty) = (e.customStyle, false);
        dirty |= style.TryGetValue(_lineWidthProp, out _lineWidth);
        dirty |= style.TryGetValue(_secondaryColorProp, out _secondaryColor);
        if (dirty) MarkDirtyRepaint();
    }

    #endregion

    #region Render callback

    void GenerateVisualContent(MeshGenerationContext context)
    {
        var center = context.visualElement.contentRect.center;
        var radius = Mathf.Min(center.x, center.y) - _lineWidth / 2;

        var tip_deg = 120 + 300 * NormalizedValue;
        var tip_rad = Mathf.Deg2Rad * tip_deg;
        var tip_vec = new Vector2(Mathf.Cos(tip_rad), Mathf.Sin(tip_rad));

        var painter = context.painter2D;
        painter.lineWidth = _lineWidth;
        painter.lineCap = LineCap.Round;

        painter.strokeColor = _secondaryColor;
        painter.BeginPath();
        painter.Arc(center, radius, 120, 120 + 300);
        painter.Stroke();

        painter.strokeColor = context.visualElement.resolvedStyle.color;
        painter.BeginPath();
        painter.Arc(center, radius, 120, tip_deg);
        painter.Stroke();

        painter.BeginPath();
        painter.MoveTo(center + tip_vec * radius / 2);
        painter.LineTo(center + tip_vec * radius);
        painter.Stroke();
    }

    #endregion
}

} // namespace VJUI
