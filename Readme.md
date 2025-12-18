# Batman Game – Game Design Homework

## Table of Contents
- [Overview](#overview)  
- [Gameplay](#gameplay)  
- [Features](#features)  
- [Controls](#controls)  
- [Scripts Overview](#scripts-overview)  
- [Download & Video](#download--video)  

---

## Overview
This is a 3D Batman-inspired Unity game where the player can control Batman, switch between different states (Normal, Alert, Stealth), interact with the Bat-Signal, and experience dynamic lighting and alarms. The game emphasizes stealth, alertness, and intuitive player control.

---

## Gameplay
- Move Batman using keyboard inputs.  
- Switch between **Normal**, **Alert**, and **Stealth** states to manage lights and alarms.  
- Toggle the **Bat-Signal** on or off to alert the city.  
- Use mouse movement to look around and rotate Batman’s view.  
- Lighting dynamically changes according to Batman's state:
  - **Normal:** Full lighting  
  - **Alert:** Flashing red/blue lights with alarm  
  - **Stealth:** Dim lighting for covert movement  

---

## Features
- **State-based Gameplay:** Batman has Normal, Alert, and Stealth modes.  
- **Alarm System:** In Alert mode, red and blue lights blink with sound effects.  
- **Dynamic Lighting:** GameManager handles light intensity globally.  
- **Bat-Signal:** Can be toggled independently.  
- **Mouse Look:** Smooth rotation with angle clamping.  
- **Movement & Rotation:** Speed and rotation adjust based on state and Shift key.  

---

## Controls

### Keyboard
| Action | Key |
|--------|-----|
| Move Forward/Backward | W / S / Up Arrow / Down Arrow |
| Move Left/Right / Rotate | A / D / Left Arrow / Right Arrow |
| Switch to Normal State | N |
| Switch to Stealth State | C |
| Switch to Alert State | Space |
| Toggle Bat-Signal | B |
| Sprint | Left Shift / Right Shift |

### Mouse
| Action | Mouse Input |
|--------|-------------|
| Look Around | Move Mouse |

---

## Scripts Overview

### `BatmanController.cs`
- Handles Batman movement, rotation, states, and interaction with lights, alarms, and Bat-Signal.  
- Key functions:
  - `HandleMovement()`: Player movement based on input (WASD and arrow keys).  
  - `HandleState()`: Switches between Normal, Alert, and Stealth.  
  - `HandleLightsandAlarm()`: Controls light intensity and alarm sounds.  
  - `StartAlarm()` / `StopAlarm()`: Activates and deactivates blinking lights and sound.  

### `BatSignalController.cs`
- Rotates the Bat-Signal continuously around its Z-axis.  

### `GameManager.cs`
- Manages global lighting.  
- Functions:
  - `LightLow()`: Dims all main lights.  
  - `LightHigh()`: Sets all main lights to full intensity.  

### `MouseController.cs`
- Handles smooth camera/character rotation with mouse input.  
- Clamps rotation to prevent over-rotation.  
- Hides and locks cursor during gameplay.  

---

## Download & Video
All project files have been uploaded to Google Drive and can be accessed here:  
[Google Drive Link](https://drive.google.com/drive/folders/1JzPx3c_A1I2hXU4JtXLb0vYYJQTsDfiq?usp=sharing)

You can also view the gameplay video in the file `gameplay.mkv` included in the Google Drive folder.
