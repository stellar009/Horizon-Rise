# Horizon Rise

![Unity](https://img.shields.io/badge/Unity-6000.3.1f1-black?logo=unity&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows&logoColor=white)
![Language](https://img.shields.io/badge/C%23-47.5%25-239120?logo=csharp&logoColor=white)
![ShaderLab](https://img.shields.io/badge/ShaderLab-43.7%25-9E1F63)
![HLSL](https://img.shields.io/badge/HLSL-8.8%25-1E90FF)
![Release](https://img.shields.io/badge/Latest-v1.5Z1-brightgreen)
![Status](https://img.shields.io/badge/Status-Released-success)

> A Unity-based PC game engineered for performance, responsiveness, and system-aware optimization. Horizon Rise serves as both a college academic submission and a portfolio showcase demonstrating real-world Unity optimization techniques — from automatic graphics scaling to seamless background scene loading.

---

## Screenshots

<p align="center">
  <em>Gameplay screenshots and GIFs will be added here.</em>
</p>

> Replace this section with in-game captures once available. Recommended format: a 1280×720 PNG or a short MP4/GIF loop showing the auto-graphics detection and a transition between scenes.

---

## Key Features

- **Auto Graphics & Frame Rate Configuration** — On launch, the game probes the system's VRAM size and graphics driver, then selects the most appropriate quality preset and target frame rate. This guarantees smooth gameplay across the full hardware spectrum, from low-end integrated GPUs to high-end discrete cards.
- **Background Scene Loading** — Every scene in Horizon Rise is loaded asynchronously in the background. By overlapping asset loading with gameplay, the game eliminates long loading screens and keeps the player immersed.
- **Performance-Oriented Design** — Built and tuned specifically for Windows PCs and laptops, with attention to draw-call budgets, shader complexity, and stable frame pacing on a wide range of hardware.

---

## Optimization Deep-Dive

Horizon Rise is as much a study in Unity performance engineering as it is a game. The optimization work centers on three pillars:

### 1. Hardware-Aware Bootup Graphic System

At startup, the `BootupGraphicSystem` queries the GPU's VRAM and identifies the active graphics driver (Intel, NVIDIA, or other). The system then categorizes the device into a performance tier and applies a matched graphics preset — texture streaming limits, shadow resolution, anti-aliasing, and target frame rate are all derived from this tier rather than left to a one-size-fits-all default. This is the same technique shipped in the 1.4Z5 and 1.4Z7 patches, where Intel UHD driver handling and system stability for low-end devices were significantly improved.

### 2. Centralized Graphics Pipeline

Instead of scattering quality settings across scenes, Horizon Rise routes all graphics decisions through a single centralized graphic system. This keeps behavior predictable, makes runtime adjustments safe, and dramatically simplifies debugging when a new hardware configuration is reported.

### 3. Background Scene Streaming

Scene transitions are streamed asynchronously using Unity's async loading APIs. Loading work is spread across frames to avoid frame-time spikes, so the player never sees a frozen screen while the next area prepares in the background. This pattern scales naturally as more scenes are added without re-architecting the loading flow.

---

## Built With

| Layer            | Technology                  |
|------------------|-----------------------------|
| Engine           | Unity 6000.3.1f1            |
| Platform         | Windows (PC / Laptop)       |
| Gameplay Code    | C#                          |
| Shaders          | ShaderLab + HLSL            |
| Focus Areas      | Optimization, Performance Scaling, Scene Management |

---

## Project Structure

```
Horizon-Rise/
├── Assets/                  # Game assets: scenes, scripts, shaders, art, audio
│   ├── Scenes/              # Unity scenes loaded in the background at runtime
│   ├── Scripts/             # C# gameplay + system code (BootupGraphicSystem, etc.)
│   ├── Shaders/             # ShaderLab + HLSL custom shaders
│   └── ...                  # Art, audio, prefabs, settings
├── Packages/                # Unity package manifest and dependencies
├── ProjectSettings/         # Unity project configuration (graphics, quality, input)
├── .vs/Horizon Rise/        # Visual Studio local workspace
└── Horizon Rise.sln         # Visual Studio solution file
```

---

## How to Play

1. **Download** the latest release build from the [Releases](#releases) section below.
2. **Unzip** the downloaded archive to a folder of your choice.
3. **Run** `Horizon Rise.exe`.

No additional dependencies are required — the build is self-contained for Windows.

---

## Releases

Horizon Rise has shipped four tagged releases, each focused on stability and graphics-system maturity:

| Version            | Tag                 | Date              | Highlights                                                                                          |
|--------------------|---------------------|-------------------|-----------------------------------------------------------------------------------------------------|
| **1.5Z1** (latest) | `Horizon-Rise-v1.5Z1` | Mar 28, 2026     | Added missing tutorials from all previous versions.                                                 |
| 1.4Z7              | `1.4Z7`             | Jan 16, 2026      | Updated `BootupGraphicSystem` to increase overall game stability.                                   |
| 1.4Z5              | `1.4Z5`             | Jan 11, 2026      | Centralized graphic system; Intel/NVIDIA driver categorization; new Help Menu UI; low-end stability.|
| 1.4Z3              | `Horizon-Rise`      | Jan 9, 2026       | Initial public build. Tested on NVIDIA GTX/RTX; auto graphic optimization based on VRAM.            |

> Each release ships a ready-to-play Windows build. Download the zip, extract, and run `Horizon Rise.exe`.

---

## Purpose of This Project

Horizon Rise was built to serve three overlapping goals:

1. **College academic submission** — a complete, shipped game demonstrating end-to-end Unity development.
2. **Personal portfolio piece** — a public showcase of real-world optimization work, available at [github.com/stellar009/Horizon-Rise](https://github.com/stellar009/Horizon-Rise).
3. **Practical demonstration of Unity optimization techniques** — specifically hardware-aware graphics scaling and background scene loading, applied in a shipping title rather than a contrived demo.

---

## Roadmap

Future work being considered for Horizon Rise:

- [ ] Add in-game screenshots and gameplay GIFs to this README
- [ ] Expand auto-detection to AMD drivers with dedicated performance tiers
- [ ] Optional high-refresh-rate mode (120/144 Hz) for capable displays
- [ ] Mac / Linux build pipeline once Unity 6000 LTS stabilizes cross-platform shaders
- [ ] In-engine benchmark scene for quick hardware-tier validation
- [ ] Save system and per-device quality overrides
- [ ] Localization framework for multi-language tutorials

---

## Author

**stellar009** — Unity developer focused on performance engineering and PC optimization.
GitHub: [@stellar009](https://github.com/stellar009) · Repository: [Horizon-Rise](https://github.com/stellar009/Horizon-Rise)

---

> *Horizon Rise — built to scale, engineered to perform.*
