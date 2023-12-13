using System;
using Microsoft.Xna.Framework.Graphics;

namespace Pevensie;

public class TileObject : PhysicsObject
{
    public TileObject(int xCoordinate, int yCoordinate, Texture2D texture, bool textureDimensionOverride = false)
    {
        if ((texture.Width != 16 || texture.Height != 16) && !textureDimensionOverride)
        {
            throw new ArgumentException("Texture must have 16x16 resolution.");
        }

        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
        Texture = texture;
    }

    // These are coordinates, NOT actual X/Y positions
    public int XCoordinate;
    public int YCoordinate;
}