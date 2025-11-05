using ProtoBuf;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using vssiltstrider.src;
using Vintagestory.API.MathTools;

namespace vs_siltstrider
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SiltStriderNavigationMessage
    {
        public bool Stop;
        public BlockPos Destination;
        public long EntityCode;
    }

    public class vs_siltstriderModSystem : ModSystem
    {

        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
            Mod.Logger.Notification("Hello from template mod: " + api.Side);
            api.RegisterEntity("SiltStrider", typeof(SiltStrider));
            api.RegisterEntityBehaviorClass("stridernavigate", typeof(BehaviorStriderNavigate));
            AiTaskRegistry.Register<AiTaskFollowCourse>("followcourse");
            api.Network
                .RegisterChannel("SiltStriderNavigation")
                .RegisterMessageType(typeof(SiltStriderNavigationMessage));
        }

        #region Server
        IServerNetworkChannel serverChannel;
        ICoreServerAPI serverApi;

        public override void StartServerSide(ICoreServerAPI api)
        {
            Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("vs-siltstrider:hello"));
            serverApi = api;
            serverChannel = serverApi.Network
                .GetChannel("SiltStriderNavigation")
                .SetMessageHandler<SiltStriderNavigationMessage>(OnClientRequest);
        }
        private void OnClientRequest(IPlayer fromPlayer, SiltStriderNavigationMessage networkRequest)
        {
            Mod.Logger.Notification("Received SiltStriderNavigationMessage from " + fromPlayer.PlayerName + "with dest" + networkRequest.Destination);
            var ent = serverApi.World.GetEntityById(networkRequest.EntityCode);
            var task = ent.GetBehavior<EntityBehaviorTaskAI>()?.TaskManager?.GetTask<AiTaskFollowCourse>();
            Mod.Logger.Notification("Found AiTask on strider:" + task.destination + ", " + task);
        }

        #endregion
        #region Client
        IClientNetworkChannel clientChannel;
        ICoreClientAPI clientApi;

        public override void StartClientSide(ICoreClientAPI api)
        {
            Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("vs-siltstrider:hello"));
            clientApi = api;
            clientChannel = clientApi.Network
                .GetChannel("SiltStriderNavigation");
            clientApi.Input.RegisterHotKey("vs-silt:navigate", Lang.Get("vs-silt:navigate"), GlKeys.N, shiftPressed: true, ctrlPressed: false);
            clientApi.Input.SetHotKeyHandler("vs-silt:navigate", HandleSendNavigationMessage);
        }

        private bool HandleSendNavigationMessage(KeyCombination keyCombination)
        {
            SiltStriderNavigationMessage msg = new()
            {
                Stop = false,
                Destination = new(1, 1, 1)
            };
            clientChannel.SendPacket(msg);
            return false;
        }
        #endregion
    }
}
