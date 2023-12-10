﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace test;
public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Setup gameState
    readonly GameState gameState = new();

    // Set the FPS
    private readonly int _fps = 60;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Set target frame rate to _fps
        TargetElapsedTime = TimeSpan.FromSeconds(1d / _fps);
    }

    protected override void Initialize()
    {
        // Initialize the player's position in the middle of the screen
        gameState.Player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                     _graphics.PreferredBackBufferHeight / 2);
        // Initialize default speed for player
        gameState.Player.Speed = 500f;

        // Initialize gamepadstate
        gameState.CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
        gameState.PreviousGamePadState = gameState.CurrentGamePadState;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a simple white texture for the player
        gameState.Player.Texture = new Texture2D(GraphicsDevice, 1, 1);
        gameState.Player.Texture.SetData(new[] { Color.Yellow });
    }

    protected override void Update(GameTime gameTime)
    {
        // Handle gamepad input
        gameState.CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
        Controls.Input(gameState, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        _spriteBatch.Draw(gameState.Player.Texture, new Rectangle((int)gameState.Player.Position.X, (int)gameState.Player.Position.Y, 50, 50), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

