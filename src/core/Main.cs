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
    readonly Parallax parallaxScene = new();

    // Set the FPS
    private readonly int _fps = 60;

    public Peter()
    {
        Content.RootDirectory = "resources";

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
        gameState.Player.SpeedScalar = 1f;
        gameState.Player.Speed = 480f; // 480 px/s @ 1920x1080p = 4s to traverse screen width, 2.25s to traverse screen height

        // Initialize gamepadstate
        gameState.CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
        gameState.PreviousGamePadState = gameState.CurrentGamePadState;

        // Initialize keyboardstate
        gameState.CurrentKeyboardState = Keyboard.GetState();
        gameState.PreviousKeyboardState = gameState.CurrentKeyboardState;

        // Initialize background graphics
        Parallax.Layer background = new(
            "background",
            Content.Load<Texture2D>("textures/background"),
            0
         );
        parallaxScene.AddLayer(background);

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

        // Update background
        foreach (var layer in parallaxScene.Layers)
        {
            // Fix this to update layer with player position
            // layer.Position += new Vector2(layer.SpeedScalar * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        // Draw Background
        foreach (var layer in parallaxScene.Layers)
        {
            _spriteBatch.Draw(layer.Texture, layer.Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer.Depth);
        }

        // Draw character
        _spriteBatch.Draw(gameState.Player.Texture, new Rectangle((int)gameState.Player.Position.X, (int)gameState.Player.Position.Y, 50, 50), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
