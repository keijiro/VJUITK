using UnityEngine;
using UnityEngine.UIElements;

namespace VJUI {

public sealed class VJDragger : PointerManipulator
{
    #region Private variables

    VJKnob _knob;
    int _pointerID;
    (Vector3 origin, float value) _start;

    bool IsActive => _pointerID >= 0;

    #endregion

    #region PointerManipulator implementation

    public VJDragger(VJKnob knob)
    {
        (_knob, _pointerID) = (knob, -1);
        activators.Add(new ManipulatorActivationFilter{button = MouseButton.LeftMouse});
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
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
            _start = (e.localPosition, _knob.value);
            target.CapturePointer(_pointerID = e.pointerId);
            e.StopPropagation();
        }
    }

    void OnPointerMove(PointerMoveEvent e)
    {
        if (!IsActive || !target.HasPointerCapture(_pointerID)) return;

        var diff = e.localPosition - _start.origin;
        var delta = (diff.x - diff.y) * _knob.sensitivity / 100;
        _knob.value = _start.value + delta * (_knob.highValue - _knob.lowValue);

        e.StopPropagation();
    }

    void OnPointerUp(PointerUpEvent e)
    {
        if (!IsActive || !target.HasPointerCapture(_pointerID)) return;

        if (CanStopManipulation(e))
        {
            _pointerID = -1;
            target.ReleaseMouse();
            e.StopPropagation();
        }
    }

    #endregion
}

} // namespace VJUI
