using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

[UxmlElement]
public partial class VJKnob : BaseField<float>
{
    #region Public UI properties

    [UxmlAttribute]
    public float lowValue { get; set; } = 0;

    [UxmlAttribute]
    public float highValue { get; set; } = 100;

    [UxmlAttribute]
    public float sensitivity { get; set; } = 1;

    #endregion

    #region USS class names

    public static readonly new string ussClassName = "vj-knob";
    public static readonly new string labelUssClassName = "vj-knob__label";

    #endregion

    #region Visual element implementation

    VJKnobInput _input;

    public VJKnob() : this(null) {}

    public VJKnob(string label) : base(label, new VJKnobInput())
    {
        AddToClassList(ussClassName);
        labelElement.AddToClassList(labelUssClassName);
        _input = (VJKnobInput)this.Q(className: VJKnobInput.ussClassName);
        _input.AddManipulator(new VJDragger(this));
    }

    public override void SetValueWithoutNotify(float newValue)
    {
        newValue = Mathf.Clamp(newValue, lowValue, highValue);
        base.SetValueWithoutNotify(newValue);
        _input.NormalizedValue = (newValue - lowValue) / (highValue - lowValue);
        _input.MarkDirtyRepaint();
    }

    #endregion
}

} // namespace VJUI
