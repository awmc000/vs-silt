# Dev Notes

## Architecture

- AiTask that tells it to go toward the waypoint
- Behaviour that marks the entity as being capable of silt strider navigation

## Research

### How are PetAI behaviors and tasks used in the Cats mod?

- Cats have the `tameable`, `receivecommand`, `nametag` behaviors.
- They have the `followmaster`, `seeknest`, `receivecommand` AITasks, which live inside the taskai behavior.

### Can AI Tasks be manually set with a func or setter?

- Lead: can `entity.GetBehavior<AiTasks>()` be used to get partway there?