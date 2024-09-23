using UnityEngine;
using UnityEngine.UIElements;

namespace VJUITK {

[UxmlElement]
public partial class VJButton : BaseField<bool>, IVJBoolState
{
    #region USS class names

    public static readonly new string ussClassName = "vj-button";
    public static readonly new string labelUssClassName = "vj-button__label";

    #endregion

    #region Public events

    public event System.Action Clicked
    {
        add => _clicker.Clicked += value;
        remove => _clicker.Clicked -= value;
    }

    #endregion

    #region Visual element implementation

    VJButtonInput _input;
    VJClicker _clicker;

    public VJButton() : this(null) {}

    public VJButton(string label) : base(label, new VJButtonInput())
    {
        AddToClassList(ussClassName);
        labelElement.AddToClassList(labelUssClassName);
        _input = (VJButtonInput)this.Q(className: VJButtonInput.ussClassName);
        _clicker = new VJClicker(this, isToggle: false);
        _input.AddManipulator(_clicker);
    }

    public override void SetValueWithoutNotify(bool newValue)
    {
        base.SetValueWithoutNotify(newValue);
        _input.ButtonState = newValue;
        _input.MarkDirtyRepaint();
    }

    #endregion
}

} // namespace VJUITK
