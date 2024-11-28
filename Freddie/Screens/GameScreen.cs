using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Gui;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Localization;
using Microsoft.Xna.Framework;
using FlatRedBall.Entities;
using System.Diagnostics;
using Freddie.Entities.Checkpoints;
using Freddie.Entities.Projectiles;
using Freddie.Entities;
using Freddie.Entities.Traps;
using Freddie.Entities.Spawners.ProjectileSpawners;
using Freddie.DataTypes;
using Freddie.Entities.Enemies;
using FlatRedBall.TileCollisions;
using Freddie.EnemyInput;

namespace Freddie.Screens;

public partial class GameScreen
{
    protected Checkpoint CurrentCheckpoint { get; set; }

    private void CustomInitialize()
    {
        RegisterEventHandlers();
        SetInitialValues();
        SetZLevels();
        InitializeTraps();
        SpawnPlayer();
    }

    private void CustomActivity(bool firstTimeCalled)
    {
        if (GuiManager.Cursor.PrimaryClick)
        {
            var z = Player.Z;
            Player.Position = GuiManager.Cursor.WorldPosition.ToVector3();
            Player.Z = z;
        }
        //Debug();
    }

    private void CustomDestroy()
    {
        var x = SpriteManager.AutomaticallyUpdatedSprites.Count;

        for (int i = SpriteManager.AutomaticallyUpdatedSprites.Count - 1; i >= 0 ; i--)
        {
            var sprite = SpriteManager.AutomaticallyUpdatedSprites[i];
            SpriteManager.RemoveSprite(sprite);
        }
    }

    private static void CustomLoadStaticContent(string contentManagerName)
    {
        
    }

    void Debug()
    {
        FlatRedBall.Debugging.Debugger.TextBlue = 0;
        FlatRedBall.Debugging.Debugger.TextGreen = 0;
        FlatRedBall.Debugging.Debugger.TextRed = 0;
        FlatRedBall.Debugging.Debugger.CommandLineWrite(Player.CurrentHealth);
    }

    void SetInitialValues()
    {
        Player.Coins = 0;
    }

    void SetZLevels()
    {
        SetZ(CheckpointList, ZLevel.Props);
        SetZ(CollectableList, ZLevel.Collectables);
        SetZ(EnemyList, ZLevel.Enemies);
        SetZ(ProjectileList, ZLevel.Projectiles);
        SetZ(Player, ZLevel.Player);
    }

    static void SetZ<T>(PositionedObjectList<T> list, ZLevel level) where T: PositionedObject
    {
        foreach (var item in list)
            item.Z = (int)level;
    }

    static void SetZ(PositionedObject posObject, ZLevel level)
    {
        posObject.Z = (int)level;
    }

    void SpawnPlayer()
    {
        Player.Respawn(CheckpointList.FindByName("GameStart"));
        MainCamera.ApplyTarget(MainCamera.GetTarget(), lerpSmooth: false);
    }

    /// <summary>
    /// Initializes traps after the custom properties from Tiled have been set.
    /// </summary>
    void InitializeTraps()
    {
        foreach (var trap in TrapList)
        {
            if (trap is Pit) continue;
            trap.InitializeFromTiled();
        }

        foreach (var projectileSpawner in ProjectileSpawnerList)
        {
            if (projectileSpawner is BlueFireSpawner blueFireSpawner)
            {
                blueFireSpawner.InitializeFromTiled();
            }
        }
    }

    void UpdateLifeMeter()
    {
        GumScreen.CurrentHealthPercentState = Player.CurrentHealth switch
        {
            1 => GumRuntimes.GameScreenGumRuntime.HealthPercent.One,
            2 => GumRuntimes.GameScreenGumRuntime.HealthPercent.Two,
            3 => GumRuntimes.GameScreenGumRuntime.HealthPercent.Three,
            4 => GumRuntimes.GameScreenGumRuntime.HealthPercent.Four,
            _ => GumRuntimes.GameScreenGumRuntime.HealthPercent.Zero
        };
    }
}