using Microsoft.Xna.Framework;

namespace GameHelpers.Interfaces
{
    public interface IMover
    {
        Vector2 Speed { get; set; }
        Vector2 Direction { get; set; }
    }
}
