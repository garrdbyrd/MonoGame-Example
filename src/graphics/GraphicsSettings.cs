using Microsoft.Xna.Framework;

namespace Pevensie;
public class GraphicsSettings
{
    private readonly GraphicsDeviceManager _graphics;

    public GraphicsSettings(GraphicsDeviceManager _graphics)
    {
        this._graphics = _graphics;
    }

    public class GraphicsObject
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public bool Fullscreen { get; set; }
        public bool Vsync { get; set; }

        public GraphicsObject(int screenWidth, int screenHeight, bool fullscreen, bool vsync)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Fullscreen = fullscreen;
            Vsync = vsync;
        }
    }

    public void SetGraphics(GraphicsObject Settings)
    {
        _graphics.PreferredBackBufferWidth = Settings.ScreenWidth;
        _graphics.PreferredBackBufferHeight = Settings.ScreenHeight;
        _graphics.IsFullScreen = Settings.Fullscreen;
        _graphics.SynchronizeWithVerticalRetrace = Settings.Vsync;
        _graphics.ApplyChanges();
    }
}
