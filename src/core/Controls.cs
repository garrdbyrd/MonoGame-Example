using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pevensie;
public static class Controls
{
    // Controls
    public static void Input(GameState gameState, GameTime gameTime)
    {
        // Input actions
        void ChangeColor()
        {
            gameState.Player.ChangeColor();
        }
        void Walk()
        {
            gameState.Player.SpeedScalar = 1f;
        }
        void Run()
        {
            gameState.Player.SpeedScalar = 1.5f;
        }
        /////////////
        // GAMEPAD //
        /////////////
        if (GamePad.GetState(PlayerIndex.One).IsConnected)
        {
            // Left analog stick
            Vector2 leftAnalogStickInput = new(gameState.CurrentGamePadState.ThumbSticks.Left.X, -gameState.CurrentGamePadState.ThumbSticks.Left.Y);
            gameState.Player.UpdateVelocity(leftAnalogStickInput * gameState.Player.MovementSpeed);
            gameState.Player.UpdatePosition(gameState.Player.Velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);

            // A Button (change color)
            if (gameState.CurrentGamePadState.Buttons.A == ButtonState.Pressed && gameState.PreviousGamePadState.Buttons.A == ButtonState.Released)
            {
                ChangeColor();
            }
            // X Button (run)
            if (gameState.CurrentGamePadState.Buttons.X == ButtonState.Pressed)
            {
                Run();
            }
            else
            {
                Walk();
            }
            // Update PreviousGamePadState
            gameState.PreviousGamePadState = gameState.CurrentGamePadState;
        }
        //////////////
        // KEYBOARD //
        //////////////
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

            Vector2 keyboardDirectionInput = keyboardInput * gameState.Player.Speed;
            gameState.Player.UpdateVelocity(keyboardDirectionInput * gameState.Player.Speed);
            gameState.Player.UpdatePosition(keyboardDirectionInput * gameState.Player.Speed, (float)gameTime.ElapsedGameTime.TotalSeconds);
            // E (change color)
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.E) && !gameState.PreviousKeyboardState.IsKeyDown(Keys.E))
            {
                ChangeColor();
            }
            // X Button (run)
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                Run();
            }
            else
            {
                Walk();
            }
            // Update PreviousKeyboardState
            gameState.PreviousKeyboardState = gameState.CurrentKeyboardState;
        }
    }
}
