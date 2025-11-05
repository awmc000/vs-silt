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

        public AiTaskFollowCourse(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
            moveSpeed = taskConfig["movespeed"].AsFloat(0.02f);
        }

        public override void StartExecute()
        {
            //     public override bool WalkTowards(Vec3d target, float movingSpeed, float targetDistance, Action OnGoalReached, Action OnStuck, EnumAICreatureType creatureType = EnumAICreatureType.Default)
            //WaypointsTraverser wt = new WaypointsTraverser(base.entity);
            if (active)
            {
                pathTraverser.WalkTowards(
                    new Vec3d(destination.X, destination.Y, destination.Z), // target
                    moveSpeed, // movingspeed
                    0.12f, // target distance
                    () => { }, // on goal reached
                    () => stuck = true // onstuck
                );
            }
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
