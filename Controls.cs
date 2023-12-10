using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace test;
public static class Controls
{
    // Controls
    // call this in main
    public static void Input(GameState gameState, GameTime gameTime)
    {
        // Input actions
        void ChangeColor()
        {
            Random random = new();
            Color tempColor = new(random.Next(256), random.Next(256), random.Next(256));
            gameState.Player.Texture.SetData(new[] { tempColor });
        }
        // Gamepad controls
        if (GamePad.GetState(PlayerIndex.One).IsConnected)
        {
            // Left analog stick
            Vector2 leftAnalogStickInput = new(gameState.CurrentGamePadState.ThumbSticks.Left.X, -gameState.CurrentGamePadState.ThumbSticks.Left.Y);
            gameState.Player.Position += leftAnalogStickInput * gameState.Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // A Button
            if (gameState.CurrentGamePadState.Buttons.A == ButtonState.Pressed && gameState.PreviousGamePadState.Buttons.A == ButtonState.Released)
            {
                ChangeColor();
            }
            gameState.PreviousGamePadState = gameState.CurrentGamePadState;
        }
        else
        {
            // KeyboardMouse controls
        }
    }
}
