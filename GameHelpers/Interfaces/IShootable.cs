using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameHelpers.Interfaces
{
    public interface IShootable
    {

        int MaxDistance { get; set; }
        Vector2 Speed { get; set; }
        Vector2 StartingPosition { get; set; }
    }
}
