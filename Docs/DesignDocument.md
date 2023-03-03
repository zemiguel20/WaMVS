# Design Document

## Game Design

A **timer** counts down X seconds. Defines the length of the run.

A **hammer** is used to **hit** the **moles** in front of the player.
Hitting a mole **increases** the **score** by X amount.

**Mole spawn system** that controls the active state of each mole.
The **mole** is only **active** for a **short period**.
Overtime, mole **activation rate increases** (more moles active at a time), and mole **active period decreases**.

The player **camera** is seen and moved through the **VR headset**.
The player has **one hammer in each hand**, with each hand being controlled by the **VR controller movement**.

**Moles** emit a **spatial sound** and **spatial vibration** when **activated**. **Spatial vibration** works similar to spatial audio, by **changing** the vibration **intensity** in **both controllers**.

**Sound** and **visual feedback** when mole **hit successfully**. *NOTE:* vibration not used as feedback as it needs to be Morse code which is not efficient for fast gameplay.

A menu with a **start button**, **options button** and **quit button** appears at the start of the game and at the end of the run.

The **options** menu has a **switch** for **audio** and **vibration**.

There is a **display** for the **time**, current **score**, and **highscore**.

*Uncertain*:
- volume changer in options
- pause button/menu
- interaction with UI (button press? pointer?)
- sound effect when hit ground or other object?

### Design Parameters

- Hole position
- Number of holes
- Mole size
- Hammer size
- Timer length
- Max vibration frequency
- Vibration duration
- Spawn system
  - Start spawn rate and mole active time
  - End spawn rate and mole active time
  - Function for interpolation between start and end values

## Technical Design

### VR Setup
The player VR rig is setup using the Unity framework template.

The hammers are the "hand" models. They are modular, have a collider and a script that interacts with a `Hittable` interface.

### Mole
The Hole object has a Mole object as a child, that is "hidden" inside it.
The Hole script provides the function `Activate(seconds)` that makes the Mole appear for some given time.
The Mole has a collider and a `Hittable` interface, for the Hammer to interact with. <br>
There is a flag that is set to prevent the player hitting more than one time in one activation.

### Game System

This system controls the game flow. It hooks with the input events and UI for things like game start.

It has a `Timer` and keeps track of the current player score and highscore. 