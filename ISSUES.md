# Iceflame Redemption: Project Issues

Issue definitions for the Weeks 1-2 core gameplay systems. These are read by the `Create Project Issues` workflow (`create-issues.yml`).

Suggested implementation order: SETUP-1, then CORE-1 to CORE-4 (movement/camera and health can be developed in parallel), then CMB-1 to CMB-3, then AI-1 and AI-2, then TEST-1.

---

### [SETUP-1] Project Setup and Input Foundation

**Labels:** `project-setup` `devops` `v0.1.0-project-setup` `ready`

**Owner:** Project Lead (Rohaan)

**Depends on:** None

**Description**

Establish the shared Unity project foundation so all programmers can work in parallel without conflicts. This includes folder structure, the Unity Input System, and a common test scene.

**Tasks / Acceptance Criteria**

- [ ] Unity project created with the agreed version and committed to `main`
- [ ] `.gitignore` and Git LFS configured for Unity and large binary assets
- [ ] Folder structure created (`Scripts/Player`, `Scripts/Enemies`, `Scripts/Magic`, `Scripts/Managers`, `Scripts/UI`, `Prefabs`, `Scenes`)
- [ ] Unity Input System installed with an Input Actions asset (Move, Look, Jump, Attack, Pause)
- [ ] `TestArena` scene created with a flat floor and basic geometry for testing
- [ ] Branching and pull request workflow documented in the README and confirmed by the whole team

---

### [CORE-1] Third-Person Camera

**Labels:** `core-gameplay` `v0.2.0-core-gameplay` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** SETUP-1

**Description**

Implement a third-person camera that follows the player and supports mouse look. The camera must feel responsive and not clip through the environment, since it will be used for combat and parkour later.

**Tasks / Acceptance Criteria**

- [ ] Camera follows the player at a configurable distance and offset
- [ ] Mouse look rotates the camera around the player
- [ ] Vertical look is clamped to prevent flipping
- [ ] Camera collision prevents clipping through walls
- [ ] Sensitivity and distance are exposed as configurable values
- [ ] Cursor is locked during gameplay

---

### [CORE-2] Player Movement

**Labels:** `core-gameplay` `v0.2.0-core-gameplay` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** SETUP-1

**Description**

Implement the core third-person player controller: movement, jumping, gravity, and ground detection. This forms the foundation for later parkour traversal, so the controller should be easy to extend.

**Tasks / Acceptance Criteria**

- [ ] WASD movement is camera-relative
- [ ] Player model rotates to face movement direction
- [ ] Sprint is implemented with configurable speed
- [ ] Jump is implemented with configurable height
- [ ] Custom gravity and reliable ground detection work on slopes and edges
- [ ] Movement values are exposed in the Inspector (or a ScriptableObject) for tuning
- [ ] Movement is tested in `TestArena` with no jitter or getting stuck on geometry

---

### [CORE-3] Health System

**Labels:** `core-gameplay` `v0.2.0-core-gameplay` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** SETUP-1

**Description**

Create a reusable health component and a shared damage interface used by the player, enemies, and any destructible objects. Keeping it generic avoids duplicating logic when new enemy types are added.

**Tasks / Acceptance Criteria**

- [ ] `Health` component with max health, current health, and clamping
- [ ] `IDamageable` interface with a `TakeDamage` method
- [ ] Healing support with a maximum cap
- [ ] Events fired for damage taken, health changed, and death (e.g. `OnDamaged`, `OnDeath`)
- [ ] Prevent damage after death
- [ ] Component works when attached to both the player and a test object

---

### [CORE-4] Player Health UI and Death State

**Labels:** `core-gameplay` `ui` `v0.2.0-core-gameplay` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** CORE-3

**Description**

Give the player visible health feedback and a defined death state, so the player always knows their condition and the game handles defeat cleanly.

**Tasks / Acceptance Criteria**

- [ ] Health bar UI reflects current player health using health events
- [ ] Damage feedback is shown when the player is hit (e.g. screen flash or vignette)
- [ ] Player input is disabled on death
- [ ] Simple game-over screen with a restart option
- [ ] Restart correctly resets player health and scene state

---

### [CMB-1] Hitboxes and Damage Pipeline

**Labels:** `combat` `v0.3.0-combat` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** CORE-3

**Description**

Implement hitboxes and hurtboxes that deliver damage between attackers and targets. This pipeline is used for both player and enemy attacks, so it must be reliable and reusable.

**Tasks / Acceptance Criteria**

- [ ] `Hitbox` component that can be enabled and disabled during attack windows
- [ ] `Hurtbox` component that forwards damage to the target's `Health`
- [ ] Layer/collision matrix set up to separate player, enemy, and environment interactions
- [ ] A single attack can only hit each target once per swing
- [ ] Damage data (amount, source, optional type) is passed with each hit
- [ ] Hitboxes are visualized with debug gizmos
- [ ] Verified with a test hitbox damaging a dummy object

---

### [CMB-2] Training Dummy and Test Setup

**Labels:** `combat` `v0.3.0-combat` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** CORE-3, CMB-1

**Description**

Create a stationary, damageable training dummy so attacks and damage can be tested before enemy AI exists. This decouples player attack development from enemy development.

**Tasks / Acceptance Criteria**

- [ ] Dummy prefab with `Health` and `Hurtbox`
- [ ] Visible feedback when hit (flash, knockback, or hit counter)
- [ ] Optional damage numbers or console logging for debugging
- [ ] Dummy can be destroyed or reset after death
- [ ] Multiple dummies placed in `TestArena`

---

### [CMB-3] Player Attack Mechanics

**Labels:** `combat` `v0.3.0-combat` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** CORE-2, CMB-1, CMB-2

**Description**

Implement the player's basic melee attack, which serves as the foundation of the magic-melee combat system. Magic will later enhance this, so attack data should be configurable and extensible.

**Tasks / Acceptance Criteria**

- [ ] Attack input triggers a melee attack
- [ ] Attack activates the player hitbox during a defined window
- [ ] Attack cooldown and input buffering implemented
- [ ] Basic combo chain of at least 2 attacks
- [ ] Attack values (damage, range, timing) stored in configurable data (ScriptableObject)
- [ ] Movement is appropriately limited or slowed during attacks
- [ ] Attack feedback (animation placeholder, sound, hit reaction)
- [ ] Verified against training dummies in `TestArena`

---

### [AI-1] Basic Enemy Prefab and NavMesh Pathing

**Labels:** `enemy-ai` `v0.4.0-enemy-ai` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** SETUP-1, CORE-3

**Description**

Create the base enemy prefab and implement basic pathfinding toward the player using Unity's NavMesh. This is the foundation for the enemy sandbox planned in Weeks 3-4.

**Tasks / Acceptance Criteria**

- [ ] NavMesh baked in `TestArena`
- [ ] Enemy prefab with `NavMeshAgent`, `Health`, and `Hurtbox`
- [ ] Enemy detects the player and paths toward them
- [ ] Enemy stops at a configurable attack range
- [ ] Enemy handles obstacles and does not get stuck on corners
- [ ] Enemy stats (speed, detection range) are configurable
- [ ] Multiple enemies can path simultaneously without major issues

---

### [AI-2] Enemy State Machine and Attack

**Labels:** `enemy-ai` `v0.4.0-enemy-ai` `ready`

**Owner:** Gameplay Programmer (TBD)

**Depends on:** CORE-4, CMB-1, AI-1

**Description**

Add an explicit state machine to the enemy so behavior is cleanly separated and new archetypes can be built later without rewriting core logic. Enemies should also be able to damage the player.

**Tasks / Acceptance Criteria**

- [ ] State machine implemented with Idle, Chase, Attack, and Dead states
- [ ] Enemy attack uses the shared hitbox/damage pipeline
- [ ] Attack cooldown implemented
- [ ] Enemy takes damage from player attacks and dies when health reaches zero
- [ ] Death state disables AI and collision, then removes or pools the enemy
- [ ] State transitions are debuggable (logging or gizmos)
- [ ] Architecture allows adding new enemy types by adding or overriding states

---

### [TEST-1] Core Systems Integration Test

**Labels:** `testing` `docs` `v0.5.0-testing` `ready`

**Owner:** Whole team

**Depends on:** SETUP-1, CORE-1 to CORE-4, CMB-1 to CMB-3, AI-1, AI-2

**Description**

Full walkthrough of the Weeks 1-2 build to catch integration issues before starting Enemy Design in Week 3.

**Tasks / Acceptance Criteria**

- [ ] Move, jump, and sprint work with the camera in `TestArena`
- [ ] Player can attack and damage both dummies and enemies
- [ ] Enemies chase and damage the player, and player health and death behave correctly
- [ ] Game-over and restart flow works without leftover state
- [ ] No blocking bugs or console errors during a 10-minute playtest
- [ ] Build runs outside the Unity editor
- [ ] `main` branch is stable and README is current

---
