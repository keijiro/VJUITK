# VJUITK (VJUI for UI Toolkit)

![gif](https://github.com/user-attachments/assets/951adfee-f417-4389-968a-44ffcee1f131)

**VJUITK** (VJUI for UI Toolkit) is a set of custom controls designed for
VJing, built using Unity's UI Toolkit.

This package is a port of the original [VJUI], which was developed for Unity UI
(uGUI).

[VJUI]: https://github.com/keijiro/VJUI

## Included Controls

- Button
- Knob
- Toggle

To use these controls, make sure to apply the custom stylesheet (`VJUITK.uss`).

## System Requirements

- Unity 6
- UI Toolkit

## Installation

The VJUITK package (`jp.keijiro.vjuitk`) can be installed via the "Keijiro"
scoped registry using Package Manager. To add the registry to your project,
please follow [these instructions].

[these instructions]:
  https://gist.github.com/keijiro/f8c7e8ff29bfe63d86b888901b82644c

## Touch Input Enhancements

VJUITK provides an **Initial Movement Rejection** option to reduce initial
resistance in touch drag inputs, which is espacially noticeable on iPhone.

![gif](https://github.com/user-attachments/assets/d47d58c3-770e-4c55-acbb-b5d714a9ed82)

The iOS input system applies a movement threshold to distinguish between a
stationary touch and a dragging touch, which can cause a sudden jump in initial
movements. The **Initial Movement Rejection** option mitigates thie issue by
rejecting abrupt initial movments.

To enable this feature, add `VJUITK_INITIAL_MOVEMENT_REJECTION` to Scripting
Defines in the Player Settings or a build profile for iPhone builds.
