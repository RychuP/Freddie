using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using Freddie.Entities.Checkpoints;
using Freddie.Entities.Collectables;
using Freddie.Entities.Enemies;
using Freddie.Entities;
using Freddie.Entities.Projectiles;
using Freddie.Screens;
using Microsoft.Xna.Framework;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Entities;
using Freddie.Entities.Traps;
using System.Collections.Generic;
using FlatRedBall.TileCollisions;

namespace Freddie.Screens;

public partial class GameScreen
{
    void RegisterEventHandlers()
    {
        Player.CoinsChanged += Player_OnCoinsChanged;
        Player.ReactToDamageReceived += Player_OnDamageReceived;
        Player.Died += Player_OnDied; 
    }

    void Player_OnCoinsChanged(object o, CoinsEventArgs e)
    {
        GumScreen.CoinCounter.CounterText = e.NewCoins.ToString();
    }

    void Player_OnDamageReceived(decimal damage, IDamageArea damageArea)
    {
        UpdateLifeMeter();
        if (Player.CurrentHealth > 0)
            Player.Respawn(CurrentCheckpoint);
    }

    void Player_OnDied(decimal damage, IDamageArea damageArea)
    {
        RestartScreen();
    }

    void OnPlayerVsCheckpointCollided (Player player, Checkpoint checkpoint) 
    {
        CurrentCheckpoint?.MarkAsUnchecked();
        CurrentCheckpoint = checkpoint;
        CurrentCheckpoint.MarkAsChecked();
    }    
    
    void OnPlayerVsCollectableCollided (Player player, Collectable collectable) 
    {
        if (collectable is Coin)
        {
            player.Coins++;
        }
        collectable.Destroy();
    }

    void OnProjectileVsTrapCollided (Projectile projectile, Trap trap) 
    {
        if (trap is Pit)
        {
            projectile.Destroy();
        }
    }

    void OnEnemyVsSolidCollisionCollided (Enemy enemy, TileShapeCollection solidCollision) 
    {
        if (enemy is Ball ball)
        {
            ball.HandleSolidCollision(solidCollision);
        }
    }

}