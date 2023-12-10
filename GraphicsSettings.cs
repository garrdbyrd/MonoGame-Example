using Microsoft.Xna.Framework;

public class GraphicsSettings
{
    private GraphicsDeviceManager _graphics;

    public GraphicsSettings(GraphicsDeviceManager _graphics)
    {
        this._graphics = _graphics;
    }

    public class GraphicsObject
    {
        public int screenWidth { get; set; }
        public int screenHeight { get; set; }
        public bool fullscreen { get; set; }
        public bool vsync { get; set; }

        public GraphicsObject(int screenWidth, int screenHeight, bool fullscreen, bool vsync)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.fullscreen = fullscreen;
            this.vsync = vsync;
        }
    }

    public void SetGraphics(GraphicsObject Settings)
    {
        this._graphics.PreferredBackBufferWidth = Settings.screenWidth;
        this._graphics.PreferredBackBufferHeight = Settings.screenHeight;
        this._graphics.IsFullScreen = Settings.fullscreen;
        this._graphics.SynchronizeWithVerticalRetrace = Settings.vsync;
        this._graphics.ApplyChanges();
    }
}
