using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pevensie;

public class Wardrobe : Game
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

    // Game Constructor
    public Wardrobe()
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

    // Game.Initialize
    protected override void Initialize()
    {
        // Initialize the player's default position in the middle of the screen
        gameState.Player.UpdatePosition(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 1);

        // Initialize gamepadstate
        gameState.CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
        gameState.PreviousGamePadState = gameState.CurrentGamePadState;

        // Initialize keyboardstate
        gameState.CurrentKeyboardState = Keyboard.GetState();
        gameState.PreviousKeyboardState = gameState.CurrentKeyboardState;

        // Initialize background graphics
        Parallax.Layer background = new(
            "background",
            Content.Load<Texture2D>("textures/test/background"),
            0
         );
        parallaxScene.AddLayer(background);

        base.Initialize();
    }

    // Game.LoadContent (graphics init)
    protected override void LoadContent()
    {
        // Init GraphicsDevice
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Draw Player
        gameState.Player.InitTexture(Content);
    }

    // Update (every frame)
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

    // Game.Draw
    // Draw sprites, etc.
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

        // Draw Background
        foreach (var layer in parallaxScene.Layers)
        {
            _spriteBatch.Draw(layer.Texture, layer.Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer.Depth);
        }

        // Draw character
        gameState.Player.DrawTexture(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
