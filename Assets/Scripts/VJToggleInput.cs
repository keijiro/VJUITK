using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

public partial class VJToggleInput : VisualElement
{
    #region USS class name

    public static readonly string ussClassName = "vj-toggle__input";

    #endregion

    #region Runtime public property

    public bool ToggleState { get; set; }

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

    public VJToggleInput()
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
        var outer = Mathf.Min(center.x, center.y) - _lineWidth / 2;
        var inner = outer - _lineWidth;

        var painter = context.painter2D;
        painter.lineWidth = _lineWidth;

        painter.strokeColor = _secondaryColor;
        painter.BeginPath();
        painter.Arc(center, outer, 0, 360);
        painter.Stroke();

        if (ToggleState)
        {
            painter.fillColor = context.visualElement.resolvedStyle.color;
            painter.lineWidth = 0;
            painter.BeginPath();
            painter.Arc(center, inner, 0, 360);
            painter.Fill();
        }
    }

    #endregion
}

} // namespace VJUI
