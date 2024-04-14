using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

public partial class VJButtonInput : VisualElement
{
    #region USS class name

    public static readonly string ussClassName = "vj-button__input";

    #endregion

    #region Runtime public property

    public bool ButtonState { get; set; }

    #endregion

    #region Custom style properties

    static CustomStyleProperty<Color> _secondaryColorProp
      = new CustomStyleProperty<Color>("--secondary-color");

    Color _secondaryColor = Color.gray;

    #endregion

    #region Visual element implementation

    public VJButtonInput()
    {
        AddToClassList(ussClassName);
        RegisterCallback<CustomStyleResolvedEvent>(UpdateCustomStyles);
        generateVisualContent += GenerateVisualContent;
    }

    void UpdateCustomStyles(CustomStyleResolvedEvent e)
    {
        if (e.customStyle.TryGetValue(_secondaryColorProp, out _secondaryColor))
            MarkDirtyRepaint();
    }

    #endregion

    #region Render callback

    void GenerateVisualContent(MeshGenerationContext context)
    {
        var center = context.visualElement.contentRect.center;
        var radius = Mathf.Min(center.x, center.y);

        var painter = context.painter2D;

        painter.fillColor = ButtonState ?
            context.visualElement.resolvedStyle.color : _secondaryColor;
        painter.BeginPath();
        painter.Arc(center, radius, 0, 360);
        painter.Fill();
    }

    #endregion
}

} // namespace VJUI
