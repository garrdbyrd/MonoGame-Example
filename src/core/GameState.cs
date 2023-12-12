using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pevensie;
public class GameState
{
    // Graphics
    public GraphicsDevice CurrentGraphicsDevice;

    // GamePad
    public GamePadState CurrentGamePadState { get; set; }
    public GamePadState PreviousGamePadState { get; set; }

    // KeyboardMouse
    public KeyboardState CurrentKeyboardState { get; set; }
    public KeyboardState PreviousKeyboardState { get; set; }

    // Player
    private readonly static int _playerSize = 48;
    public PlayerClass Player { get; set; } = new(_playerSize);
}