# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview
This is a Unity 3D project called "Time Trial" running on Unity 6000.0.55f1. It appears to be a racing or time-based game with player movement controls and potentially multiplayer elements.

## Key Architecture

### Unity Project Structure
- **Scripts/**: Contains C# gameplay code (currently minimal with a basic Player.cs)
- **Prefabs/**: Game objects including Player, NPC, and Wheel prefabs
- **Materials/**: Rendering materials for car paint, ground, and wheels
- **Scenes/**: Unity scenes (SampleScene.unity is the main scene)
- **Settings/**: Render pipeline and graphics settings for both mobile and PC builds

### Input System
The project uses Unity's new Input System with comprehensive input mappings defined in `InputSystem_Actions.inputactions`:
- **Player actions**: Move, Look, Attack, Interact, Crouch, Jump, Sprint, Previous, Next
- **UI actions**: Standard UI navigation and interaction
- **Multiple control schemes**: Keyboard&Mouse, Gamepad, Touch, Joystick, XR

### Dependencies
Key packages include:
- Universal Render Pipeline (URP) 17.0.4 for graphics rendering
- Input System 1.14.1 for modern input handling  
- AI Navigation 2.0.8 for pathfinding
- Visual Scripting 1.9.7
- Multiplayer Center 1.0.0 (suggests multiplayer features)

## Development Commands
Unity projects are typically built and run through the Unity Editor. There is no command-line build setup detected in this repository.

**Building**: Use Unity Editor File > Build Settings or File > Build and Run
**Testing**: Use Unity Editor Window > General > Test Runner for Unity Test Framework

## Working with this Codebase
- The main gameplay logic should be implemented in Assets/Scripts/Player.cs
- Input handling is already configured - reference InputSystem_Actions for available actions
- The project uses URP, so follow URP-compatible shader and rendering practices
- Multiple platform support is configured (PC, Mobile) with separate render pipeline assets