using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

/*
 * Reference: https://github.com/anegostudios/vssurvivalmod/blob/master/Systems/Trading/EntityTradingHumanoid.cs
 */

namespace vssiltstrider.src
{
    public class SiltStrider: EntityAgent
    {
        protected GuiDialogSiltStrider dlg;


        void TryOpenNavigateDialog(EntityAgent byEntity)
        {
            if (World.Side != EnumAppSide.Client) return;

            EntityPlayer entityplr = byEntity as EntityPlayer;
            IPlayer player = World.PlayerByUid(entityplr.PlayerUID);
            ICoreClientAPI capi = (ICoreClientAPI)Api;

            if (dlg?.IsOpened() != true)
            {
                // Will break all kinds of things if we allow multiple concurrent of these dialogs
                if (capi.Gui.OpenedGuis.FirstOrDefault(dlg => dlg is GuiDialogSiltStrider && dlg.IsOpened()) == null)
                {

                    dlg = new GuiDialogSiltStrider(this, World.Api as ICoreClientAPI);
                    dlg.TryOpen();
                }
                else
                {
                    capi.TriggerIngameError(this, "onlyonedialog", Lang.Get("Can only interact with one silt strider at a time"));
                }
            }
            else
            {
            }
        }

        public override void OnInteract(EntityAgent byEntity, ItemSlot slot, Vec3d hitPosition, EnumInteractMode mode)
        {

            if (Api.Side == EnumAppSide.Client)
            {
                Console.WriteLine("task x: " + GetBehavior<EntityBehaviorTaskAI>()?.TaskManager?.GetTask<AiTaskFollowCourse>().destination);
                TryOpenNavigateDialog(byEntity);
            }
        }

    }
}
