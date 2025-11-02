using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using vssiltstrider.src;

namespace vs_siltstrider
{
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
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("vs-siltstrider:hello"));
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("vs-siltstrider:hello"));
        }

    }
}
