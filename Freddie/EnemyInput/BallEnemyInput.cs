using FlatRedBall.Input;
using Freddie.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freddie.EnemyInput;

internal class BallEnemyInput : InputDeviceBase
{
    public HorizontalDirection DesiredDirection { get; set; }

    protected override float GetHorizontalValue()
    {
        if (DesiredDirection == HorizontalDirection.Left)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}