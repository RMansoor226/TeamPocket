# Work in Progress Title
### Unity | C# | 3D Action-Adventure | Team Pocket

> A 3rd-person action-adventure game where magic enhances melee, enemies form a
> combat sandbox, and parkour traversal connects exploration to combat.

## Project Summary

**Work in Progress Title** is a 3rd-person 3D action-adventure game developed by **Team Pocket** for a college game development club. The project is scoped as a **9-week minimum viable product (MVP)**: a vertical slice of 1–2 levels that demonstrates the core combat loop and enemy design.

You play as **Vyx**, a young mage born into a town where magic is taboo and sacrilegious. Vyx has innate control of the magic arts and must use their power for good while evading a condemning society, becoming a symbol of the hope magic can bring.

## Team

| Member | Role |
|---|---|
| **Rohaan** | Project Lead / Game Designer / Programmer |
| **Riley** | Programmer / Artist |
| **Mari** | Composer / Artist |
| **Solar** | Writer / Programmer |
| **Gabriel** | Programmer / Game Designer |

---

# Game Design Vision

Iceflame Redemption is built on three gameplay pillars:

### Magic-Melee Combat

Combat is **not just hack and slash**. Melee is the foundation, and magic builds on top of it.

- Magic abilities enhance melee attacks.
- Magic spells provide ranged options.
- Elemental magic enhances player power and self-expression.

### Enemy Sandbox

A varied roster of enemy types where different magic types counter different enemies. Players must read the encounter and choose the right tools rather than repeating a single strategy.

### Combat Parkour Traversal

Levels are designed so that parkour movement creates combat advantages. Traversal enhances the pace of fights and ties exploration directly into combat.

---

# Story

Vyx was born in a town where magic and powers are taboo and sacrilegious. Unfortunately, they were born with innate control of the magic arts.

While Vyx uses their power for good, they must also learn to evade being caught by their condemning society and peers. On their own, they will demonstrate the hope that magic can bring.

---

# Game Mechanics

## Magic-Based Combat

- Attack enemies from range or up close.
- Different magic types counter different enemy types.
- Combine favorite magic abilities to form a unique playstyle.

## Parkour Traversal

- Levels facilitate parkour to achieve combat advantages.
- Enhances the pace of fights.
- Connects exploration to combat.

## Exploration Puzzles

- Reinforce the player's combat mechanics.
- Break the pace between combat encounters.
- Provide gameplay upgrades and abilities.

---

# Core Gameplay Loop

The moment-to-moment loop connects all three pillars:

```text
        ┌──────────────────────────────────────────────┐
        │                                              │
        ▼                                              │
   Explore & Traverse                                  │
 (parkour, exploration puzzles)                        │
        │                                              │
        ▼                                              │
   Enter Combat Arena                                  │
 (position using parkour for advantage)                │
        │                                              │
        ▼                                              │
   Read the Enemy Mix                                  │
 (identify enemy types and weaknesses)                 │
        │                                              │
        ▼                                              │
   Fight with Magic-Melee                              │
 (melee + elemental spells + counters)                 │
        │                                              │
        ▼                                              │
   Earn Upgrades & Abilities  ─────────────────────────┘
 (from puzzles and progression)
```

**Explore → Traverse → Engage → Adapt → Grow → Repeat**

Exploration puzzles break up the pace between fights while granting the upgrades and abilities that expand the player's combat options. Those new options feed back into more expressive encounters.

---

# Art & Aesthetics

Iceflame Redemption uses a **hybrid 2D and 3D art style**:

- **2D sprites** for characters
- **3D world** for environments

The tone is **light but focused and serious**. The hybrid style helps make the game immediately brighter by approaching the visual likeness of a cartoon, while the story retains its weight.

---

# Scope: Minimum Viable Product

The target for the 9-week timeline is a **vertical slice**:

- 1–2 levels
- Demonstrates the core combat loop
- Demonstrates enemy design
- Demonstrates magic-melee combat and combat parkour traversal

---

# Development Timeline

| Weeks | Phase | Focus | Status |
|---|---|---|---|
| **1–2** | Core Gameplay Systems | Player movement, health mechanics, hitboxes, basic enemy AI | Planned |
| **3–4** | Enemy Design | Basic enemy archetypes, special enemy classes, combat arenas | Planned |
| **5–6** | Unique Magic Combat | Elemental magic, magic spells | Planned |
| **7–8** | Testing | Playtesting, bug fixing, balancing | Planned |
| **9 → Finish** | Polishing | Visual, audio, and gameplay polish | Planned |

---

# Planned Features

## Player

- Third-person camera
- Player movement
- Parkour traversal
- Health system
- Hitboxes

## Combat

- Melee combat
- Magic-enhanced melee
- Ranged magic spells
- Elemental magic system
- Elemental strengths and weaknesses

## Enemies

- Basic enemy AI
- Basic enemy archetypes
- Special enemy classes
- Enemy types countered by specific magic types
- Combat arenas

## World & Progression

- Parkour-oriented level design
- Exploration puzzles
- Gameplay upgrades and abilities

## Presentation

- Hybrid 2D sprite / 3D world art style
- Original music composition
- Narrative featuring the protagonist Vyx

---

# Workflows

Team Pocket follows a lightweight, professional-style workflow so that five contributors can work in parallel without breaking the main build.

## Tools

| Purpose | Tool |
|---|---|
| Game Engine | Unity |
| Version Control | Git / GitHub |
| Automated Workflows | GitHub Actions |
| Pixel Art | Aseprite |
| 2D Art | Krita |
| 3D Art | Blender |

## Branching Strategy

- `main` always contains a stable, playable build.
- Each feature or task is developed on its own branch (e.g. `feature/player-movement`, `feature/enemy-ai`).
- Work is merged into `main` through **pull requests** with at least one teammate review.
- Never commit directly to `main`.

## Task Tracking

Development is divided into small, testable tickets using **GitHub Issues** and **Milestones**. Each ticket should include:

- A defined objective
- A description
- Acceptance criteria
- A dedicated branch
- Testing before merge

Milestones map to the timeline phases: Core Gameplay Systems, Enemy Design, Unique Magic Combat, Testing, and Polishing.

## Automated Workflows

**GitHub Actions** is used to automate repetitive checks and builds, helping catch integration problems early. Planned automation includes:

- Build validation on pull requests
- Automated build artifacts for playtesting

## Contribution Workflow

1. Pick up a ticket from GitHub Issues and assign yourself.
2. Create a branch from the latest `main`.
3. Implement the feature and test it in Unity.
4. Open a pull request linking the issue.
5. Get a teammate review and resolve feedback.
6. Merge into `main` and close the issue.

## Asset Workflow

- Pixel art is created in **Aseprite** and general 2D art in **Krita**.
- 3D models are created in **Blender**.
- Assets are committed in organized folders and named consistently to avoid merge conflicts.
- Large binary assets should be tracked with **Git LFS**.

---

# Development Philosophy

## Build Systems Before Content

Core systems (movement, health, hitboxes, AI) come first, before levels and polish.

## Scope to the Vertical Slice

The goal is not a large quantity of unfinished features, but a **small, highly representative slice** of the final game that proves the design.

## Test Before Expanding

New systems are tested on their own and in integrated gameplay before more complexity is added.

## Prefer Reusable Architecture

Enemies, spells, and abilities should reuse shared systems so that new content is quick to add.

## Design Serves the Fantasy

Every mechanic should support the core pillars: magic-melee combat, enemy variety, and parkour traversal.

---

# Controls

> Controls are provisional and will be finalized during development.

| Action | Input |
|---|---|
| Move | `WASD` |
| Look | `Mouse` |
| Jump | `Space` |
| Melee Attack | `Mouse 1` |
| Cast Spell | `Mouse 2` |
| Pause | `Escape` |

---

# Repository Structure

```text
Assets/
├── Art/
│   ├── Sprites/
│   ├── Models/
│   └── Materials/
├── Audio/
│   ├── Music/
│   └── SFX/
├── Prefabs/
├── Scenes/
├── Scripts/
│   ├── Player/
│   ├── Enemies/
│   ├── Magic/
│   ├── Managers/
│   └── UI/
└── ...
```

---

# Current Status

**Development Status: Planning / Pre-production**

The design, scope, and team roles are established. Development begins with the **Core Gameplay Systems** phase (Weeks 1–2).

---

# Known Limitations (MVP)

The MVP is a vertical slice, not a complete game. Expected limitations include:

- Only 1–2 levels.
- Limited enemy roster.
- Limited magic element variety.
- Placeholder assets during early development.
- Story is only partially represented.

---

# Contributing

This is a club project developed by Team Pocket. Team members should follow the workflows above. Please open an issue to discuss any major design change before implementing it.

---

# License

This project is currently a college club project. Unless otherwise specified, project assets and source code should not be redistributed or reused without permission.
