using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

public sealed class VJClicker : PointerManipulator
{
    #region Private variables

    IVJBoolState _state;
    bool _isToggle;
    int _pointerID;

    bool IsActive => _pointerID >= 0;

    #endregion

    #region PointerManipulator implementation

    public VJClicker(IVJBoolState state, bool isToggle)
    {
        (_state, _isToggle, _pointerID) = (state, isToggle, -1);
        activators.Add(new ManipulatorActivationFilter{button = MouseButton.LeftMouse});
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    #endregion

    #region Pointer callbacks

    void OnPointerDown(PointerDownEvent e)
    {
        if (IsActive)
        {
            e.StopImmediatePropagation();
        }
        else if (CanStartManipulation(e))
        {
            if (!_isToggle) _state.value = true;
            target.CapturePointer(_pointerID = e.pointerId);
            e.StopPropagation();
        }
    }

    void OnPointerUp(PointerUpEvent e)
    {
        if (!IsActive || !target.HasPointerCapture(_pointerID)) return;

        if (CanStopManipulation(e))
        {
            _state.value = _isToggle ? !_state.value : false;
            _pointerID = -1;
            target.ReleaseMouse();
            e.StopPropagation();
        }
    }

    #endregion
}

} // namespace VJUI
