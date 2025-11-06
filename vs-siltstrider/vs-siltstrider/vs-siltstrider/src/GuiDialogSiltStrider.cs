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
using Vintagestory.API.MathTools;
using vs_siltstrider;
/*
 * Reference: https://github.com/anegostudios/vssurvivalmod/blob/master/Gui/GuiDialogTrader.cs
 */
namespace vssiltstrider.src
{
    public class GuiDialogSiltStrider: GuiDialog
    {
        protected SiltStrider strider;
        protected GuiDialog dlg;
        public List<EntityPlayer> interactingWithPlayer = [];
        public SiltStriderNavigationMessage CurrentMessage;
        public String destinationText;

        public GuiDialogSiltStrider(SiltStrider ss, ICoreClientAPI capi) : base(capi)
        {
            strider = ss;
            CurrentMessage = new()
            {
                Destination = new(1, 1, 1),
                Stop = true,
                EntityCode = strider.EntityId
            };
            Compose();
        }

        public override string ToggleKeyCombinationCode => null;

        public bool OnButtonPress()
        {
            capi.TriggerIngameError(this, "onlyonedialog", Lang.Get("button pressed"));
            // Toggle stop
            BlockPos dest = new(1, 1, 1);
            if (destinationText != null)
            {
                int[] coords = destinationText.Split(',').Select(int.Parse).ToArray();
                dest.X = coords[0];
                dest.Y = coords[1];
                dest.Z = coords[2];
            }
            CurrentMessage.Destination = dest;
            CurrentMessage.Stop = !CurrentMessage.Stop;
            capi.Network.GetChannel("SiltStriderNavigation").SendPacket(CurrentMessage);
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

            // TODO: Get list of player map waypoints, select from dropdown!

            SingleComposer = capi.Gui.CreateCompo("myAwesomeDialog", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddStaticText("Strider Navigation", CairoFont.WhiteDetailText(), textBounds)
                .AddButton("Enter Destination", OnButtonPress, buttonBounds)
                .AddTextInput(fieldBounds, (string s) => { destinationText = s; })
                .Compose()
            ;
        }
    }
}
