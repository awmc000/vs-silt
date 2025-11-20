using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.Essentials;
using Vintagestory.GameContent;

namespace vssiltstrider.src
{
    public class AiTaskFollowCourse : AiTaskBase
    {
        public bool active = false;
        public BlockPos destination = new(0,0,0);
        float moveSpeed = 0.02f;
        bool stuck = false;
        EntityAgent entity;

        public AiTaskFollowCourse(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
            moveSpeed = taskConfig["movespeed"].AsFloat(0.02f);
            entity = entity;
        }

        public override void StartExecute()
        {
            //     public override bool WalkTowards(Vec3d target, float movingSpeed, float targetDistance, Action OnGoalReached, Action OnStuck, EnumAICreatureType creatureType = EnumAICreatureType.Default)
            //WaypointsTraverser wt = new WaypointsTraverser(base.entity);
            Vec3d d = new Vec3d(destination.X, destination.Y, destination.Z);
            Console.WriteLine("Dest:" + destination.X +", " + destination.Z);
            pathTraverser.NavigateTo_Async(
                d, // target
                0.02f, // movingspeed
                0.12f, // target distance
                () => { }, // on goal reached
                () => { stuck = true; Console.WriteLine("Silt strider is Stuck"); },
                null,
                1000,
                    1
            );
        }

        public override bool CanContinueExecute()
        {
            return pathTraverser.Ready;
        }

        public override bool ShouldExecute()
        {
            return active;
        }

        public override bool ContinueExecute(float dt)
        {
            return active;
        }
    }
}
