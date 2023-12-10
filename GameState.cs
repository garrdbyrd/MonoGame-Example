using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameState
{
    // GamePad
    public GamePadState CurrentGamePadState { get; set; }
    public GamePadState PreviousGamePadState { get; set; }

    // Player
    public class _player
    {
        // Physics and important stuff
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        // Aesthetic stuff
        public Texture2D Texture { get; set; }
    }
    public _player Player { get; set; }
    public GameState()
    {
        Player = new _player();
    }
}