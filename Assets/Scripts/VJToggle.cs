using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

[UxmlElement]
public partial class VJToggle : BaseField<bool>, IVJBoolState
{
    #region USS class names

    public static readonly new string ussClassName = "vj-toggle";
    public static readonly new string labelUssClassName = "vj-toggle__label";

    #endregion

    #region Visual element implementation

    VJToggleInput _input;

    public VJToggle() : this(null) {}

    public VJToggle(string label) : base(label, new VJToggleInput())
    {
        AddToClassList(ussClassName);
        labelElement.AddToClassList(labelUssClassName);
        _input = (VJToggleInput)this.Q(className: VJToggleInput.ussClassName);
        _input.AddManipulator(new VJClicker(this, isToggle: true));
    }

    public override void SetValueWithoutNotify(bool newValue)
    {
        base.SetValueWithoutNotify(newValue);
        _input.ToggleState = newValue;
        _input.MarkDirtyRepaint();
    }

    #endregion
}

} // namespace VJUI
