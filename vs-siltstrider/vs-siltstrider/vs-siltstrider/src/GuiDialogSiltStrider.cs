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
        protected SiltStrider ss;
        protected GuiDialog dlg;
        public List<EntityPlayer> interactingWithPlayer = new List<EntityPlayer>();

        public String testGuiText;

        public GuiDialogSiltStrider(SiltStrider ss, ICoreClientAPI capi) : base(capi)
        {
            this.ss = ss;
            testGuiText = "Hello, World!";
            Compose();
        }

        public override string ToggleKeyCombinationCode => null;

        public void Compose()
        {
            // Auto-sized dialog at the center of the screen
            ElementBounds dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.LeftTop);

            // Just a simple 300x100 pixel box with 40 pixels top spacing for the title bar
            ElementBounds textBounds = ElementBounds.Fixed(0, 40, 300, 100);

            // Background boundaries. Again, just make it fit it's child elements, then add the text as a child element
            ElementBounds bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
            bgBounds.BothSizing = ElementSizing.FitToChildren;
            bgBounds.WithChildren(textBounds);

            SingleComposer = capi.Gui.CreateCompo("myAwesomeDialog", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddStaticText("Silt Strider Navigation", CairoFont.WhiteDetailText(), textBounds)
                .Compose()
            ;
        }
    }
}
