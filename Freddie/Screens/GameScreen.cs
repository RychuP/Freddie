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
using Freddie.DataTypes;
using FlatRedBall.Entities;
using System.Diagnostics;
using Freddie.Entities.Checkpoints;
using Freddie.Entities.Projectiles;
using Freddie.Entities;

namespace Freddie.Screens;

public partial class GameScreen
{
    Checkpoint CurrentCheckpoint;

    private void CustomInitialize()
    {
        RegisterEventHandlers();
        SetInitialValues();
        SetZLevels();
        InitializeTraps();
    }

    private void CustomActivity(bool firstTimeCalled)
    {
        
    }

    private void CustomDestroy()
    {
        
    }

    private static void CustomLoadStaticContent(string contentManagerName)
    {
        
    }

    void Debug()
    {
        FlatRedBall.Debugging.Debugger.TextBlue = 0;
        FlatRedBall.Debugging.Debugger.TextGreen = 0;
        FlatRedBall.Debugging.Debugger.TextRed = 0;
        FlatRedBall.Debugging.Debugger.CommandLineWrite($"Map.Z = {Map.Z}");
        FlatRedBall.Debugging.Debugger.CommandLineWrite($"Door.Z = {DoorList[0].Z}");
        FlatRedBall.Debugging.Debugger.CommandLineWrite($"Player.Z = {Player.Z}");
    }

    void SetInitialValues()
    {
        Player.Coins = 0;
    }

    void SetZLevels()
    {
        SetZ(DoorList, ZLevel.Buildings);
        SetZ(CollectableList, ZLevel.Collectables);
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

    void InitializeTraps()
    {
        foreach (var projectile in ProjectileList)
        {
            if (projectile is Fire fire)
            {
                fire.DelayAnimationStart();
            }
        }
    }
}