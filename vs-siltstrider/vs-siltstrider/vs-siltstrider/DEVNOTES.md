# Dev Notes

## Architecture

- AiTask that tells it to go toward the waypoint
- Behaviour that marks the entity as being capable of silt strider navigation
- GUI that sends a message to server, to set the target destination of a strider's FollowCourse behavior

## Research

### How are PetAI behaviors and tasks used in the Cats mod?

- Cats have the `tameable`, `receivecommand`, `nametag` behaviors.
- They have the `followmaster`, `seeknest`, `receivecommand` AITasks, which live inside the taskai behavior.

### Can AI Tasks be manually set with a func or setter?

- We can't set the current executing task, but we can set the condition it will execute under, and make it the highest priority task.
- It looks like the entity can get the task object like this (Wolf taming mod)
- `var task = dog?.GetBehavior<EntityBehaviorTaskAI>()?.TaskManager?.GetTask<AiTaskPlayFetch>()`
- Then it could set the variables on the task object
- `task = strider.GetBehavior<EntityBehaviorTaskAI>()?.TaskManager?.GetTask<AiTaskFollowCourse>();`
- Ah, task is always null, because this is client side code (GuiDialog) trying to fuck with server side behavior (TaskAI)
- For fix, look into:
	- https://wiki.vintagestory.at/Modding:Network_API
	- https://apidocs.vintagestory.at/api/Vintagestory.API.Server.IServerNetworkAPI.html
	- https://apidocs.vintagestory.at/api/Vintagestory.API.Client.IClientNetworkAPI.html