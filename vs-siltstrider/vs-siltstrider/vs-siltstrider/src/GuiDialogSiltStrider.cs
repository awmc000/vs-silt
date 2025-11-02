using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Util;
using Vintagestory.GameContent;
/*
 * Reference: https://github.com/anegostudios/vssurvivalmod/blob/master/Gui/GuiDialogTrader.cs
 */
namespace vssiltstrider.src
{
    public class GuiDialogSiltStrider: GuiDialog
    {
        protected SiltStrider strider;
        protected GuiDialog dlg;
        public List<EntityPlayer> interactingWithPlayer = new List<EntityPlayer>();

        public String testGuiText;

        public GuiDialogSiltStrider(SiltStrider ss, ICoreClientAPI capi) : base(capi)
        {
            this.strider = ss;
            testGuiText = "Hello, World!";
            Compose();
        }

        public override string ToggleKeyCombinationCode => null;

        public bool OnButtonPress()
        {
            capi.TriggerIngameError(this, "onlyonedialog", Lang.Get("button pressed"));
            
            // TODO: Move all this logic to a network message to the server side, we are on client side!
            // TODO: Set X, Y, Z on the driving AITask
            var tm = strider.GetBehavior<EntityBehaviorTaskAI>()?.TaskManager;
            if (tm != null)
            {
                Console.WriteLine("taskmgr: " + tm.ToString());
                Console.WriteLine(tm.AllTasks);
            }
            var task = strider.GetBehavior<EntityBehaviorTaskAI>()?.TaskManager?.GetTask<AiTaskFollowCourse>();
            if (task == null)
            {
                Console.WriteLine("The task is null in GuiDialogSiltStrider!");
            }
            else
            {
                task.x = 1;
                task.y = 1;
                task.z = 1;
            }
            // TODO: Set a flag that allows the task to execute
            return true;
        }
        public void Compose()
        {
            // Auto-sized dialog at the center of the screen
            ElementBounds dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.CenterMiddle);

            ElementBounds textBounds = ElementBounds.Fixed(0, 0, 150, 50);
            ElementBounds fieldBounds = ElementBounds.Fixed(0, 100, 100, 50);
            ElementBounds buttonBounds = ElementBounds.Fixed(0, 200, 100, 50);

            // Background boundaries. Again, just make it fit it's child elements, then add the text as a child element
            ElementBounds bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
            bgBounds.BothSizing = ElementSizing.FitToChildren;
            bgBounds.WithChildren(textBounds);
            bgBounds.WithChildren(fieldBounds);
            bgBounds.WithChildren(buttonBounds);

            SingleComposer = capi.Gui.CreateCompo("myAwesomeDialog", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddStaticText("Strider Navigation", CairoFont.WhiteDetailText(), textBounds)
                .AddButton("Enter Destination", OnButtonPress, buttonBounds)
                .AddTextInput(fieldBounds, (string s) => {  })
                .Compose()
            ;
        }
    }
}
