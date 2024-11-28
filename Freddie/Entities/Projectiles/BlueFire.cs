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
using Freddie.DataTypes;
using FlatRedBall.Utilities;

namespace Freddie.Entities.Projectiles;

public partial class BlueFire
{
    bool _isLaunching = true;

    string _direction;
    public string Direction
    {
        get => _direction;
        set
        {
            if (_direction ==  value) return;
            if (!Strings.Directions.Contains(value))
                throw new ArgumentException("Invalid direction.");
            _direction = value;
            OnDirectionChanged();
        }
    }

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
        ApplyAnimationCollisionShapes();
        LaunchingActivity();
    }

    private void CustomDestroy()
    {
        
    }

    private static void CustomLoadStaticContent(string contentManagerName)
    {
        
    }

    void LaunchingActivity()
    {
        if (_isLaunching)
        {
            if (SpriteInstance.CurrentChain.Name == "BlueFireLaunch" + Direction &&
                SpriteInstance.CurrentFrameIndex == 3)
            {
                var animName = "BlueFire" + Direction;
                SpriteInstance.CurrentChain = SpriteInstance.AnimationChains[animName];
            }
            else if (SpriteInstance.CurrentChain.Name == "BlueFire" + Direction &&
                SpriteInstance.CurrentFrameIndex == 1)
            {
                _isLaunching = false;
                Velocity = Direction switch
                {
                    "Up" => Vector3.UnitY * Speed,
                    "Down" => Vector3.UnitY * -Speed,
                    "Right" => Vector3.UnitX * Speed,
                    _ => Vector3.UnitX * Speed
                };
            }
        }
    }

    /// <summary>
    /// Applies collision shapes defined in the animation file.
    /// </summary>
    void ApplyAnimationCollisionShapes()
    {
        SpriteInstance.CurrentFrame?.ShapeCollectionSave?.SetValuesOn(Collision, this, true);
        ForceUpdateDependenciesDeep();
    }

    void OnDirectionChanged()
    {
        SpriteInstance.CurrentChain = SpriteInstance.AnimationChains["BlueFireLaunch" + Direction];

        // apply position offset depending on the direction
        X += Direction switch
        {
            "Right" => 8,
            "Left" => -8,
            _ => 0
        };

        Y += Direction switch
        {
            "Up" => 8,
            "Down" => -8,
            _ => 0
        };
    }
}