using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

[UxmlElement]
public partial class VJButton : BaseField<bool>, IVJBoolState
{
    #region USS class names

    public static readonly new string ussClassName = "vj-button";
    public static readonly new string labelUssClassName = "vj-button__label";

    #endregion

    #region Visual element implementation

    VJButtonInput _input;

    public VJButton() : this(null) {}

    public VJButton(string label) : base(label, new VJButtonInput())
    {
        AddToClassList(ussClassName);
        labelElement.AddToClassList(labelUssClassName);
        _input = (VJButtonInput)this.Q(className: VJButtonInput.ussClassName);
        _input.AddManipulator(new VJClicker(this, isToggle: false));
    }

    public override void SetValueWithoutNotify(bool newValue)
    {
        base.SetValueWithoutNotify(newValue);
        _input.ButtonState = newValue;
        _input.MarkDirtyRepaint();
    }

    #endregion
}

} // namespace VJUI
