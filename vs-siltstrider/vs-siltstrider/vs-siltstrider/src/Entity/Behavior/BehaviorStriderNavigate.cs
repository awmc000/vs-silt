using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace vssiltstrider.src
{
    internal class BehaviorStriderNavigate(Entity entity) : EntityBehavior(entity)
    {
        public override void OnInteract(EntityAgent byEntity, ItemSlot itemslot, Vec3d hitPosition, EnumInteractMode mode, ref EnumHandling handled)
        {

        }
        public override string PropertyName()
        {
            return "stridernavigate";
        }
    }
}
