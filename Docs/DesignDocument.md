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

**Sound** and **visual feedback** when mole **hit successfully**. **Sound feedback** when hit **failed target**. *NOTE:* vibration not used as feedback as it needs to be Morse code which is not efficient for fast gameplay.

A menu with a **start button**, **options button** and **quit button** appears at the start of the game and at the end of the run.

The **options** menu has a **switch** for **audio** and **vibration**.

There is a **display** for the **time**, current **score**, and **highscore**.

*Uncertain*:
- volume changer in options
- pause button/menu
- interaction with UI (button press? pointer?)

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

WIP