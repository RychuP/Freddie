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

namespace Freddie.Entities.Spawners.ProjectileSpawners;

public partial class BlueFireSpawner
{
    bool _isActive;

    /// <summary>
    /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
    /// This method is called when the Entity is added to managers. Entities which are instantiated but not
    /// added to managers will not have this method called.
    /// </summary>
    private void CustomInitialize()
    {
        
    }

    /// <summary>
    /// Called from GameScreen after custom properties from Tiled have been set.
    /// </summary>
    public void InitializeFromTiled()
    {
        RotateSprite();
        DelaySpawnStart();
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

    /// <summary>
    /// Rotates sprite according to direction set in Tiled.
    /// </summary>
    void RotateSprite()
    {
        var offset = (float)Math.PI / 2;
        RotationZ = Direction switch
        {
            "Right" => offset * 1,
            "Down" => offset * 2,
            "Left" => offset * 3,
            _ => offset * 0
        };
    }

    void DelaySpawnStart()
    {
        var instr = new DelegateInstruction(StartSpawn)
        {
            TimeToExecute = TimeManager.CurrentScreenTime + StartDelay
        };
        Instructions.Add(instr);
    }

    void StartSpawn()
    {
        _isActive = true;
        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        var z = (int)ZLevel.Projectiles;
        var pos = new Vector3(X, Y, z);
        var projectile = Factories.BlueFireFactory.CreateNew(pos);
        projectile.Direction = Direction;
        projectile.Destroyed += () => SpawnProjectile();
    }
}