using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pevensie;

public class PlayerObject : PhysicsObject
{
    private readonly int _squareSize;
    public SpriteBatch defaultSpriteBatch;
    public PlayerObject(int squareSize)
    {
        _squareSize = squareSize;
    }

    // Methods
    public void InitTexture(ContentManager contentManager)
    {
        Texture = contentManager.Load<Texture2D>("textures/player/player");
    }
    public void DrawTexture(SpriteBatch _spriteBatch)
    {
        defaultSpriteBatch = _spriteBatch;
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
        // Leaving this blank for now
        // Texture.SetData(colorData);
    }
}