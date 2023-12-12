using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pevensie;

public class PlayerObject : PhysicsObject
{
    private readonly int _squareSize;
    public PlayerObject(int squareSize)
    {
        _squareSize = squareSize;
    }

    // Methods
    public void InitTexture(GraphicsDevice graphicsDevice)
    {
        Texture = new Texture2D(graphicsDevice, _squareSize, _squareSize);
        Color[] colorData = new Color[_squareSize * _squareSize];
        for (int i = 0; i < colorData.Length; i++)
        {
            colorData[i] = Color.Yellow;
        }
        Texture.SetData(colorData);
    }
    public void DrawTexture(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, _squareSize, _squareSize), Color.White);
    }
    public void ChangeColor()
    {
        Random random = new();
        Color tempColor = new(random.Next(256), random.Next(256), random.Next(256));
        Color[] colorData = new Color[_squareSize * _squareSize];
        for (int i = 0; i < colorData.Length; i++)
        {
            colorData[i] = tempColor;
        }
        Texture.SetData(colorData);
    }
}