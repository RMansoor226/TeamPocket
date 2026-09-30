## Contributing: Branch and Pull Request Workflow
 
Every task is tracked as a GitHub Issue. When an issue is marked `ready`, a branch is created for it automatically. You work on that branch, then open a pull request to merge it into `main`.
 
### Rules at a Glance
 
| Branch | Can I commit directly? | Requirements |
|---|---|---|
| Feature branch (e.g. `v0.2.0-core-gameplay/issue-3-player-movement`) | ✅ Yes, commit and push freely | None |
| `main` | ❌ No | Pull request with **at least 1 approving review** and **no "Request changes" reviews** |
 
`main` must always contain a stable, playable build.
 
### Branch Naming
 
Branches are created as `<version-phase>/issue-<number>-<title>`:
 
```text
v0.1.0-project-setup/issue-1-project-setup-and-input-foundation
v0.2.0-core-gameplay/issue-3-player-movement
v0.3.0-combat/issue-8-player-attack-mechanics
v0.4.0-enemy-ai/issue-9-basic-enemy-prefab-and-navmesh-pathing
```
 
### Step-by-Step Process
 
**1. Clone the repository (first time only)**
 
```bash
git clone https://github.com/RMansoor226/TeamPocket.git
cd TeamPocket
git lfs install
git lfs pull
```
 
**2. Pick an issue**
 
Choose an issue labeled `ready`, assign yourself, and check its **Depends on** line. Make sure those issues are merged first.
 
**3. Switch to the issue's branch**
 
The branch link is in the comment on the issue.
 
```bash
git fetch origin
git checkout v0.2.0-core-gameplay/issue-3-player-movement
```
 
**4. Bring in the latest `main`**
 
Do this before you start, and again any time `main` has changed.
 
```bash
git merge origin/main
```
 
**5. Make changes and commit often**
 
```bash
git status                  # see what changed
git add Assets/Scripts/Player/PlayerMovement.cs
git commit -m "feat: add camera-relative movement (#3)"
git push origin v0.2.0-core-gameplay/issue-3-player-movement
```
 
You can commit and push as often as you like on your feature branch, so small, frequent commits are encouraged.
 
**6. Open a pull request**
 
When every acceptance criterion in the issue is checked off:
 
1. On GitHub, click **Compare & pull request** for your branch (or go to **Pull requests → New pull request**).
2. Set the base branch to `main`.
3. Title it like the issue and add `Closes #3` in the description so the issue closes on merge.
4. Describe what changed and how you tested it.
5. Request a review from at least one teammate.
**7. Review and merge**
 
- Reviewers pull the branch, open it in Unity, and test it.
- **Approve** means it works and is ready to merge.
- **Request changes** means it is not ready to merge. Fix the problems, push new commits to the same branch, and ask for a re-review.
- The PR can only be merged when it has at least 1 approval and no outstanding "Request changes" reviews.
- Merge with **Squash and merge** to keep `main` history clean, then delete the branch.
**8. Update your local copy**
 
```bash
git checkout main
git pull origin main
```
 
### Commit Messages
 
Use the format `type: short description (#issue-number)`:
 
| Type | Use for | Example |
|---|---|---|
| `feat` | New feature | `feat: add sprint and jump (#3)` |
| `fix` | Bug fix | `fix: prevent player sticking to walls (#3)` |
| `refactor` | Code change without behavior change | `refactor: move input into PlayerInput (#3)` |
| `art` | Sprites, models, materials | `art: add player idle sprite sheet (#8)` |
| `docs` | Documentation | `docs: update controls table` |
| `test` | Test scenes or test setup | `test: add training dummies to TestArena (#7)` |
 
Write in the present tense, keep the first line under about 70 characters, and make each commit one logical change.
 
### Working with Unity
 
- **Avoid editing the same scene or prefab as a teammate at the same time.** These merge poorly. Ask in the team chat before editing `TestArena` or shared prefabs.
- **Commit `.meta` files** together with their assets, and never commit the `Library/`, `Temp/`, or `Logs/` folders.
- **Large binary assets** (models, audio, textures) are tracked with Git LFS.
- **Test in Unity before opening a PR.** Check the console for errors.
### If Something Goes Wrong
 
**Merge conflicts** when bringing in `main`:
 
```bash
git merge origin/main
# Fix the conflicted files, then:
git add <resolved-files>
git commit
```
 
**Undo your last commit** (keeps your changes):
 
```bash
git reset --soft HEAD~1
```
 
**Save unfinished work** before switching branches:
 
```bash
git stash
git checkout <other-branch>
git stash pop
```
 
Never force-push (`git push --force`) to a shared branch.
