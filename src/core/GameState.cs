using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pevensie;
public class GameState
{
    // GamePad
    public GamePadState CurrentGamePadState { get; set; }
    public GamePadState PreviousGamePadState { get; set; }

    // KeyboardMouse
    public KeyboardState CurrentKeyboardState { get; set; }
    public KeyboardState PreviousKeyboardState { get; set; }

    // Player
    public class PlayerClass
    {
        // Physics and important stuff
        public Vector2 Position { get; set; }
        public float BaseSpeed { get; set; }
        public float Speed { get; set; }
        // Speed: pixels/second at 1920x1080p (agnostic to framerate)
        public float SpeedScalar { get; set; }

        // Aesthetic stuff
        public Texture2D Texture { get; set; }
    }
    public PlayerClass Player { get; set; } = new();
}