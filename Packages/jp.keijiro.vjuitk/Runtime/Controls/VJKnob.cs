using UnityEngine;
using UnityEngine.UIElements;

namespace VJUITK {

[UxmlElement]
public partial class VJKnob : BaseField<float>
{
    #region Public UI properties

    [UxmlAttribute]
    public float lowValue { get => _lowValue; set => SetLowValue(value); }

    [UxmlAttribute]
    public float highValue { get => _highValue; set => SetHighValue(value); }

    [UxmlAttribute]
    public float sensitivity { get; set; } = 1;

    #endregion

    #region Property backend

    float _lowValue = 0;
    float _highValue = 100;

    void SetLowValue(float value)
    {
        _lowValue = value;
        SetValueWithoutNotify(this.value);
    }

    void SetHighValue(float value)
    {
        _highValue = value;
        SetValueWithoutNotify(this.value);
    }

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

} // namespace VJUITK
