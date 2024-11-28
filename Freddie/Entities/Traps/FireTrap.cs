using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;

namespace Freddie.Entities.Traps;

public partial class FireTrap
{
    /// <summary>
    /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
    /// This method is called when the Entity is added to managers. Entities which are instantiated but not
    /// added to managers will not have this method called.
    /// </summary>
    private void CustomInitialize()
    {
        
    }

    private void CustomActivity()
    {
        // apply collision shapes defined in the animation file
        SpriteInstance.CurrentFrame?.ShapeCollectionSave?.SetValuesOn(Collision, this, true);
        ForceUpdateDependenciesDeep();
    }

    private void CustomDestroy()
    {
        
    }

    private static void CustomLoadStaticContent(string contentManagerName)
    {
        
    }

    public async void InitializeFromTiled()
    {
        SpriteInstance.Animate = false;
        SpriteInstance.CurrentFrameIndex = 0;
        await TimeManager.DelaySeconds(StartDelay);
        SpriteInstance.Animate = true;
    }

    public void DelayAnimationStart()
    {
        var instr = new DelegateInstruction(StartAnimation)
        {
            TimeToExecute = TimeManager.CurrentScreenTime + StartDelay
        };
        Instructions.Add(instr);
    }

    void StartAnimation()
    {
        SpriteInstance.Animate = true;
    }
}