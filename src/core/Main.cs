using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pevensie;
public class Peter : Game
{
    // Setup graphics
    private readonly GraphicsDeviceManager _graphics;
    private readonly GraphicsSettings _graphicsSettings;
    private SpriteBatch _spriteBatch;

    // Setup gameState
    readonly GameState gameState = new();

    // Set the FPS
    private readonly int _fps = 60;

    public Peter()
    {
        Content.RootDirectory = "Content";

        // General graphics settings
        IsMouseVisible = false;
        _graphics = new GraphicsDeviceManager(this);
        _graphicsSettings = new GraphicsSettings(_graphics);
        GraphicsSettings.GraphicsObject defaultGraphics = new(1920, 1080, true, true);
        _graphicsSettings.SetGraphics(defaultGraphics);

        // Set target frame rate to _fps
        TargetElapsedTime = TimeSpan.FromSeconds(1d / _fps);
    }

    protected override void Initialize()
    {
        // Initialize the player's default position in the middle of the screen
        gameState.Player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                     _graphics.PreferredBackBufferHeight / 2);

        // Initialize default speed for player
        gameState.Player.Speed = 500f;

        // Initialize gamepadstate
        gameState.CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
        gameState.PreviousGamePadState = gameState.CurrentGamePadState;

        // Initialize keyboardstate
        gameState.CurrentKeyboardState = Keyboard.GetState();
        gameState.PreviousKeyboardState = gameState.CurrentKeyboardState;

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
        gameState.CurrentKeyboardState = Keyboard.GetState();
        Controls.Input(gameState, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        // Draw Background

        // Draw character
        _spriteBatch.Draw(gameState.Player.Texture, new Rectangle((int)gameState.Player.Position.X, (int)gameState.Player.Position.Y, 50, 50), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

