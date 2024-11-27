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
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Freddie.Entities;

public partial class Player
{
    public event EventHandler<CoinsEventArgs> CoinsChanged;

    IPressableInput RunInput;

    public bool UpInputIsApplied => VerticalInput.Value > 0;

    public bool DownInputIsApplied => VerticalInput.Value < 0;

    public bool IsSkidding => Math.Sign(XVelocity) != Math.Sign(HorizontalInput.Value) &&
                RunInput.IsDown;

    int _coins;
    public int Coins
    {
        get => _coins;
        set
        {
            if (value < 0) value = 0;
            if (_coins == value) return;
            var prevCoins = _coins;
            _coins = value;
            OnCoinsChanged(prevCoins, _coins);
        }
    }

    private void CustomInitialize()
    {
        
    }

    partial void CustomInitializePlatformerInput()
    {
        if (InputDevice is Keyboard keyboard)
        {
            RunInput = keyboard.GetKey(Keys.LeftShift);
        }
        else if (InputDevice is Xbox360GamePad gamepad)
        {
            RunInput = gamepad.GetButton(Xbox360GamePad.Button.RightTrigger);
        }
    }

    private void CustomActivity()
    {
        InputActivity();
    }

    private void CustomDestroy()
    {
        
    }

    private static void CustomLoadStaticContent(string contentManagerName)
    {
        
    }

    void InputActivity()
    {
        if (!CurrentMovement.CanClimb)
        {
            if (DownInputIsApplied)
            {
                GroundMovement = PlatformerValuesStatic["Ducking"];
            }
            else if (RunInput.IsDown)
            {
                GroundMovement = PlatformerValuesStatic["Running"];
                AirMovement = PlatformerValuesStatic["RunningAir"];
            }
            else
            {
                GroundMovement = PlatformerValuesStatic["Ground"];
                AirMovement = PlatformerValuesStatic["Air"];
            }
        }
        else
        {
            if (DownInputIsApplied && IsOnGround)
            {
                GroundMovement = PlatformerValuesStatic["Ground"];
            }
        }
    }

    void OnCoinsChanged(int prevCoins, int newCoins)
    {
        var args = new CoinsEventArgs()
        {
            PrevCoins = prevCoins,
            NewCoins = newCoins
        };
        CoinsChanged?.Invoke(this, args);
    }
}

public class CoinsEventArgs : EventArgs
{
    public int PrevCoins { get; init; }
    public int NewCoins { get; init; }
}