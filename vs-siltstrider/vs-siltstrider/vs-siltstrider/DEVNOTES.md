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
- Re: how to use network facilities in api
	- Server side: RegisterChannel(string) Supplies you with your very own and personal network channel that you can use to send packets across the network. Use the same channelName on the client and server to have them link up.
	- SendEntityPacket(IServerPlayer, long, int, byte[]): Sends a entity packet to the given player and entity. For quick an easy entity network communication without setting up a channel first.
	- GetChannel(string): Returns a previously registered channel, null otherwise
	- Client side: GetChannel(string) Returns a previously registered channel, null otherwise
	- SendEntityPacket(long, int, byte[]) Sends a entity interaction packet to the server. For quick an easy entity network communication without setting up a channel first.
	- IClientNetworkChannel.SendPacket<T>(T message)
	- IServerNetworkChannel.SendPacket<T>(T, params IServerPlayer[])
- Architecture:

Button press in GUI 
=> Send message to channel with new destination and walking/idle state.
=> Server side handler receives message with destination, sets variable in AITask
=> While AI Task is running, it routes the strider toward this destination

- - Refer to https://github.com/G3rste/vsvillage/blob/b77d3164c1486eff2279086bc55434abd63301c6/src/Systems/VillageSystem/VillageManager.cs#L33

- Advice from Dana: refer to board game mod shuffle network code
- EEK, the client side GUI is reaching server side mod system, which is able to find the entity!

### AI Tasks are shut off when an entity is being ridden

- see line 305
- Do I need to inherit BehaviorRideable and change this...?
- just make a separate behaviour?
- ... don't want to repeat work, so probably inherit BehaviorRideable and override methods necessary