using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pevensie;

public class PhysicsObject
{

    ////////////////
    // KINEMATICS //
    ////////////////

    // Kinematics Properties
    public Vector2 Position { get; set; } = new Vector2(0, 0);
    public Vector2 Velocity { get; set; } = new Vector2(0, 0);

    // Speed: pixels/second at 1920x1080p (agnostic to framerate)
    // Speed: the actual current speed of the player
    public float Speed
    {
        get
        {
            return Velocity.Length();
        }
    }

    // 480 px/s @ 1920x1080p = 4s to traverse screen width, 2.25s to traverse screen height
    public float BaseSpeed { get; set; } = 320f;
    public float SpeedScalar { get; set; } = 1f;
    // Movement speed: the speed of the player if they invoke movement (with a gamepad or keyboard)
    public float MovementSpeed
    {
        get
        {
            return BaseSpeed * SpeedScalar;
        }
    }

    // Kinematics Methods
    public void UpdatePosition(Vector2 velocity, float dt)
    {
        Position += velocity * dt;
    }
    public void UpdateVelocity(Vector2 newVelocity)
    {
        Velocity = newVelocity;
    }
    public void UpdateSpeed(float newSpeed)
    {
        Velocity = newSpeed * Vector2.Normalize(Velocity);
    }

    //////////////
    // GRAPHICS //
    //////////////

    // Visuals
    public Texture2D Texture { get; set; }

}
