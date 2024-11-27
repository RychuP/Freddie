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

namespace Freddie.Screens;

public partial class GameScreen
{
    void RegisterEventHandlers()
    {
        Player.CoinsChanged += Player_OnCoinsChanged;
    }

    void Player_OnCoinsChanged(object o, CoinsEventArgs e)
    {
        GumScreen.CoinCounter.CounterText = e.NewCoins.ToString();
    }

    void OnTestCollisionVsProjectileCollided (TestCollision testCollision, Projectile projectile) 
    {

    }

    void OnPlayerVsCheckpointCollided (Player player, Checkpoint checkpoint) 
    {
        CurrentCheckpoint?.MarkAsUnchecked();
        CurrentCheckpoint = checkpoint;
        CurrentCheckpoint.MarkAsChecked();
    }    
    
    void OnPlayerVsCollectableCollided (Player player, Collectable collectable) 
    {
        if (collectable is Coin coin)
        {
            player.Coins++;
        }
        collectable.Destroy();
    }

}