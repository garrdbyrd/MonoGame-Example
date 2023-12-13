using System;
using Microsoft.Xna.Framework.Graphics;

namespace Pevensie;

public class TileObject : PhysicsObject
{
    public TileObject(int xCoordinate, int yCoordinate, Texture2D texture, bool textureDimensionOverride = false)
    {
        if ((texture.Width != _textureSize || texture.Height != _textureSize) && !textureDimensionOverride)
        {
            throw new ArgumentException("Texture must have 16x16 resolution.");
        }

        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
        Texture = texture;
    }

    // Private properties
    private readonly int _textureSize = 48;

    // These are coordinates, NOT actual X/Y positions
    public int XCoordinate;
    public int YCoordinate;
}