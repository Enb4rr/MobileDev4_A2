# MobileDev4_A2

### Made by PG29 Julian R
### Last Updated 6/14/2026

---

## Concept

The player scans their physical environment to detect a horizontal surface, strategically places towers on that surface via touch input, and then launches a wave of enemies that traverse the real-world plane. Towers automatically detect and engage enemies within range using a hitscan system with a visible laser effect.

---

## How to Interact

The application runs through three sequential states:

### 1. Scanning
Point your device at a flat floor surface and move slowly. The instruction panel reads **"Move your phone slowly to scan the floor"**. Once a surface is detected, a transparent plane overlay appears and the application advances automatically.

### 2. Placing
The instruction panel reads **"Tap on the detected surface to place a Tower"**. Tap anywhere on the detected plane to place a tower (up to 5). Once all towers are placed, a **Start Wave** button appears.

### 3. Playing
Tap **Start Wave** to begin. Enemies spawn at one edge of the detected plane and walk toward the opposite edge. Towers within range automatically target and shoot the nearest enemy. Enemies that take enough hits are destroyed; enemies that reach the goal disappear.

---

## Setup & Build Notes

1. Clone the repository and open in **Unity 6000.3.12f1**
2. Ensure the following packages are installed via Package Manager:
   - AR Foundation
   - Google ARCore XR Plugin
   - XR Plugin Management
3. In **Project Settings → XR Plug-in Management → Android**, enable **ARCore**
4. In **Project Settings → Player → Android**, set Scripting Backend to **IL2CPP** and Target Architecture to **ARM64**
5. Add **AR Background Renderer Feature** to the URP Mobile Renderer asset (required for correct camera background rendering in URP)
6. Switch build target to **Android** and build to a connected ARCore-compatible device

> **Editor Testing:** XR Simulation is supported in the editor via **Window → XR → AR Foundation → XR Environment**. Use right-click + drag to look around and WASD to move. Left-click to place towers on detected simulation planes.

---
