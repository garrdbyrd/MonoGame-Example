using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace test;
public static class Controls
{
    // Controls
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
        // KeyboardMouse controls
        else
        {
            // WASD movement
            Vector2 keyboardInput = Vector2.Zero;
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.W)) // Move up
            {
                keyboardInput.Y -= 1;
            }
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.S)) // Move down
            {
                keyboardInput.Y += 1;
            }
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.A)) // Move left
            {
                keyboardInput.X -= 1;
            }
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.D)) // Move right
            {
                keyboardInput.X += 1;
            }
            if (keyboardInput.Length() > 0)
            {
                keyboardInput.Normalize();
            }
            gameState.Player.Position += keyboardInput * gameState.Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // E (change color)
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.E) && !gameState.PreviousKeyboardState.IsKeyDown(Keys.E))
            {
                ChangeColor();
            }
            gameState.PreviousKeyboardState = gameState.CurrentKeyboardState;
        }
    }
}
