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
using Freddie.EnemyInput;
using FlatRedBall.TileCollisions;

namespace Freddie.Entities.Enemies;

public partial class Ball
{
    /// <summary>
    /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
    /// This method is called when the Entity is added to managers. Entities which are instantiated but not
    /// added to managers will not have this method called.
    /// </summary>
    private void CustomInitialize()
    {
        AssignInput();
    }

    private void CustomActivity()
    {
        
    }

    private void CustomDestroy()
    {
        
    }

    private static void CustomLoadStaticContent(string contentManagerName)
    {
        
    }

    void AssignInput()
    {
        var input = new BallEnemyInput();
        InitializePlatformerInput(input);
    }

    public void HandleSolidCollision(TileShapeCollection solidCollision)
    {
        var collisionReposition = CircleInstance.LastMoveCollisionReposition;

        // check for horizontal collisions with a wall
        if (collisionReposition.X != 0)
        {
            var input = InputDevice as BallEnemyInput;
            var isWallToTheRight = collisionReposition.X < 0;

            if (isWallToTheRight && input.DesiredDirection == HorizontalDirection.Right)
            {
                input.DesiredDirection = HorizontalDirection.Left;
            }
            else if (!isWallToTheRight && input.DesiredDirection == HorizontalDirection.Left)
            {
                input.DesiredDirection = HorizontalDirection.Right;
            }
        }
    }
}