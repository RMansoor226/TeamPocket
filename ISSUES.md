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

### [CMB-1] Hitbox and Hurtbox Core Pipeline
 
**Labels:** `combat` `v0.3.0-combat` `ready`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CORE-3, CORE-4
 
**Description**
 
Implement the reusable hitbox/hurtbox pipeline that every melee attack, enemy attack, and defense mechanic in this build will use. This is foundational and contains no attack feel or animation work yet.
 
**Tasks / Acceptance Criteria**
 
- [ ] `Hitbox` component that can be enabled and disabled during a defined active window
- [ ] `Hurtbox` component that forwards damage to the target's existing `Health` component
- [ ] Layer/collision matrix set up to separate player, enemy, and environment interactions
- [ ] A single hitbox can only register one hit per target per activation
- [ ] Damage data (amount, source, hit type) is passed with each hit
- [ ] Hitboxes and hurtboxes are visualized with debug gizmos
- [ ] Verified by scripting a temporary test hitbox against a temporary test object (no dummy prefab yet)
---
 
### [CMB-2] Training Dummy
 
**Labels:** `combat` `v0.3.0-combat`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CMB-1
 
**Description**
 
Create a stationary, damageable training dummy prefab. This is a pure test target used by later attack and defense tickets, and it should not contain any attack logic of its own.
 
**Tasks / Acceptance Criteria**
 
- [ ] Dummy prefab with `Health` and `Hurtbox`
- [ ] Visible feedback when hit (flash, knockback, or hit counter)
- [ ] Optional damage numbers or console logging for debugging
- [ ] Dummy resets or respawns a few seconds after death, so it can be hit repeatedly during testing
- [ ] Multiple dummies placed in `TestArena`
---
 
### [CMB-3] Player Target Lock
 
**Labels:** `combat` `v0.3.0-combat` `ready`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** None (uses the existing camera and movement systems)
 
**Description**
 
Implement a target-lock system so the player can focus an enemy, keeping the camera and player orientation on it. This supports the DOOM-inspired "read the encounter" design and will be used by attacks, defense, and future magic targeting.
 
**Tasks / Acceptance Criteria**
 
- [ ] Target-lock input toggles lock onto the nearest valid target in front of the player
- [ ] Camera orbits around the player while keeping the locked target in frame
- [ ] Player faces the locked target automatically during movement
- [ ] Lock is dropped when the target dies, leaves range, or breaks line of sight
- [ ] Switching to a different nearby target is possible while locked
- [ ] Works against the dummy prefab and any `Health`-bearing object, independent of whether an enemy AI exists yet
---
 
### [CMB-4] Basic Melee Attack (Light Archetype)
 
**Labels:** `combat` `v0.3.0-combat`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CMB-1, CMB-2
 
**Description**
 
Implement the player's first attack archetype: a fast, low-damage light attack. This establishes the input-to-hitbox pipeline and the configurable attack data format that the heavy archetype will reuse. Combo chaining and a second archetype are separate tickets so this can merge as soon as one attack feels good.
 
**Tasks / Acceptance Criteria**
 
- [ ] Attack input triggers the light attack
- [ ] Attack activates the player hitbox during a defined active window
- [ ] Attack cooldown and basic input buffering implemented
- [ ] Attack values (damage, range, timing) stored in configurable data (ScriptableObject) so future archetypes reuse the same structure
- [ ] Movement is appropriately limited or slowed during the attack
- [ ] Attack feedback (animation placeholder, sound, hit reaction)
- [ ] Verified against training dummies in `TestArena`
---
 
### [CMB-5] Heavy Attack Archetype and Combo Chain
 
**Labels:** `combat` `v0.3.0-combat`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CMB-4
 
**Description**
 
Add a second attack archetype — a slower, higher-damage heavy attack — and connect it with the light attack into a basic combo chain. This is the first real test of "which attack should I use," a core pillar from the design vision.
 
**Tasks / Acceptance Criteria**
 
- [ ] Heavy attack uses its own configurable attack data (higher damage, longer wind-up, shorter active window)
- [ ] Combo chain of at least light → light → heavy is possible
- [ ] Combo resets if the player waits too long between inputs
- [ ] Heavy attack has clearer tells (wind-up animation placeholder, audio cue) to support future enemy counterplay
- [ ] Movement restriction during the heavy attack is more pronounced than during the light attack
- [ ] Verified against training dummies in `TestArena`, including interrupting a combo mid-chain
---
 
### [CMB-6] Player Defense: Block
 
**Labels:** `combat` `v0.3.0-combat`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CMB-1
 
**Description**
 
Implement blocking, reducing or negating incoming damage while held. This is the simplest defense option and is the foundation for the parry timing window.
 
**Tasks / Acceptance Criteria**
 
- [ ] Block input raises a guard state while held
- [ ] Incoming hits while blocking are reduced by a configurable percentage (not necessarily full negation)
- [ ] Player movement speed is reduced while blocking
- [ ] Block has a stamina or duration limit, or another configurable cost, to prevent permanent blocking
- [ ] Block feedback (animation placeholder, blocked-hit sound/effect distinct from a normal hit)
- [ ] Verified by having a test hitbox (or dummy, if it can attack) strike a blocking player
---
 
### [CMB-7] Player Defense: Dodge
 
**Labels:** `combat` `v0.3.0-combat`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CMB-1
 
**Description**
 
Implement a directional dodge that grants brief invulnerability and repositions the player. This supports the "positioning and momentum" pillar from the design vision and gives the player an option against attacks that can't be blocked.
 
**Tasks / Acceptance Criteria**
 
- [ ] Dodge input moves the player a fixed distance in the held movement direction (or backward if no input is held)
- [ ] Dodge grants invulnerability frames for a configurable portion of the dodge
- [ ] Dodge has a cooldown so it can't be spammed
- [ ] Dodge feedback (animation placeholder, movement trail or effect, audio cue)
- [ ] Verified by dodging through a test hitbox's active window without taking damage
---
 
### [CMB-8] Player Defense: Parry
 
**Labels:** `combat` `v0.3.0-combat`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** CMB-6
 
**Description**
 
Add a short, precise timing window at the start of a block that fully negates damage and briefly staggers the attacker. This turns defense into an active decision rather than a passive damage reduction, per the "exploiting enemy weaknesses" pillar.
 
**Tasks / Acceptance Criteria**
 
- [ ] A parry window exists at the start of the block input (configurable duration)
- [ ] A hit landing inside the parry window deals zero damage to the player
- [ ] A successful parry applies a stagger/flinch to the attacker, if the attacker supports it
- [ ] Parry has distinct feedback from a normal block (sound, flash, or effect)
- [ ] Missing the parry window falls back to a normal block, not a free hit
- [ ] Verified by timing a test hitbox's swing against the parry window, including deliberately missing it
---
 
### [AI-1] Basic Enemy Prefab and NavMesh Pathing
 
**Labels:** `enemy-ai` `v0.4.0-enemy-ai` `ready`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** None (uses the existing Health system)
 
**Description**
 
Create the base enemy prefab and implement basic pathfinding toward the player using Unity's NavMesh. This ticket is movement only; no attacking or state machine yet.
 
**Tasks / Acceptance Criteria**
 
- [ ] NavMesh baked in `TestArena`
- [ ] Enemy prefab with `NavMeshAgent`, `Health`, and `Hurtbox`
- [ ] Enemy detects the player and paths toward them
- [ ] Enemy stops at a configurable distance from the player
- [ ] Enemy handles obstacles and does not get stuck on corners
- [ ] Enemy stats (speed, detection range) are configurable
- [ ] Multiple enemies can path simultaneously without major issues
---
 
### [AI-2] Enemy State Machine (Idle and Chase)
 
**Labels:** `enemy-ai` `v0.4.0-enemy-ai`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** AI-1
 
**Description**
 
Add an explicit state machine with Idle and Chase states so enemy behavior is clean and extensible. Attack and Dead states are a separate ticket so this can merge and be tested as pure movement behavior first.
 
**Tasks / Acceptance Criteria**
 
- [ ] State machine architecture supports adding states without rewriting existing ones
- [ ] Idle state: enemy stays in place until the player is detected
- [ ] Chase state: enemy paths toward the player once detected, using AI-1's pathing
- [ ] Enemy returns to Idle if it loses the player for a configurable duration
- [ ] State transitions are debuggable (logging or gizmos)
---
 
### [AI-3] Enemy Attack and Death
 
**Labels:** `enemy-ai` `v0.4.0-enemy-ai`
 
**Owner:** Gameplay Programmer (TBD)
 
**Depends on:** AI-2, CMB-1
 
**Description**
 
Add Attack and Dead states to the enemy state machine, using the existing hitbox/hurtbox pipeline. This makes enemies a real threat and a valid target for the player's defense options.
 
**Tasks / Acceptance Criteria**
 
- [ ] Attack state triggers when the enemy is within range, using the shared hitbox/damage pipeline
- [ ] Attack has a visible tell (wind-up) so it can be blocked, dodged, or parried
- [ ] Attack cooldown implemented
- [ ] Enemy takes damage from the player's attacks and transitions to Dead when health reaches zero
- [ ] Dead state disables AI and collision, then removes or pools the enemy
- [ ] Verified against the player's block, dodge, and parry from CMB-6 through CMB-8
---
 
### [TEST-1] Melee Build Integration Test
 
**Labels:** `testing` `docs` `v0.5.0-testing`
 
**Owner:** Whole team
 
**Depends on:** CMB-1 through CMB-8, AI-1 through AI-3
 
**Description**
 
Full walkthrough of the melee-focused build to catch integration issues before moving on to magic and the wider enemy sandbox.
 
**Tasks / Acceptance Criteria**
 
- [ ] Player can target-lock an enemy and maintain lock through a fight
- [ ] Light attack, heavy attack, and the combo chain all connect and deal damage correctly
- [ ] Block, dodge, and parry all function against a live enemy attack, not just a test hitbox
- [ ] Enemies chase, attack, and die correctly, and can be damaged mid-attack
- [ ] Player health, damage feedback, and death/restart still behave correctly with the new combat systems in place
- [ ] No blocking bugs or console errors during a 10-minute playtest mixing attacks and defense
- [ ] Build runs outside the Unity editor
- [ ] `main` branch is stable and README is current

---
