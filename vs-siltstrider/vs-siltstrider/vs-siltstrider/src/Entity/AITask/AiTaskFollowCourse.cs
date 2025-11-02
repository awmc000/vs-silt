using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace vssiltstrider.src
{
    public class AiTaskFollowCourse : AiTaskBase
    {
        public double? x;
        public double? y;
        public double? z;
        float moveSpeed = 0.02f;
        bool stuck = false;

        public AiTaskFollowCourse(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
            moveSpeed = taskConfig["movespeed"].AsFloat(0.02f);
        }

        public override void StartExecute()
        {
            //     public override bool WalkTowards(Vec3d target, float movingSpeed, float targetDistance, Action OnGoalReached, Action OnStuck, EnumAICreatureType creatureType = EnumAICreatureType.Default)
            pathTraverser.WalkTowards(
                new Vec3d(0.0,100.0,0.0), // target
                moveSpeed, // movingspeed
                0.12f, // target distance
                () => { }, // on goal reached
                () => stuck = true // onstuck
            );
        }

        public override bool ShouldExecute()
        {
            return true;
        }

        public override bool ContinueExecute(float dt)
        {
            return true;
        }
    }
}
