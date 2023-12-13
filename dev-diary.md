# Dev Diary
### 2023-12-12
This is the first diary entry, but I think this project started two days ago. Building a game has been something I've been interested in a long time, and I guess it's finally decide if it's something that I am actually interested in. There's a lot of math going on under the hood that I would eventually like to understand; graphics programming and rendering is immensely interesting, but complicated and not appropriate for my current skill level.

The term *game engine* is pretty loose, as is basically every term in tech (no one really knows what middleware is). Of course, MonoGame is an engine in the sense that it handles all the rendering as implements a bunch of basic, necessary classes like `Vector2`. However, the upfront work has been building up my own framework to handle physics, and perhaps later, things like level generation, etc.

Like everyone else, I have installed Unity about 5 times throughout my life, launched it once following each install, and never touched it again. At one point, I had done so similarly for Unreal, Cry (I still have a softspot for Crytek), and Godot (yes, following the Unity events of 2023). Something about the GUI of these was intimidating to me, perhaps because it emphasized my lack of artistic ability. Also, these are all mainly in 3D, which is a whole dimension scarier than 2D. I feel at home in VS Code, and MonoGame plays to that.

More specific thoughts:

Making `physicsObject` was a bit like creating a "block" class for MineCraft. It will be used for basically everything. The commit *Complete physics overhaul* definitely should have been developed as its own branch, had I not implemented the whole thing in a continuous few hours.

"Physcs overhaul" is also not super descriptive, since the primary purpose of the update was to abstract a lot that was being done in `Main.cs`. E.g., I think this commit is also when I added the `PlayerObject` class. Instead of loading in the player texture in `Main.cs`, this is all mostly done in `gameState.Player` (I think).

ChatGPT has been extremely helpful for this project. By any reasonable metric, I do not know C-Sharp. Also, the docs for Mono/MonoGame/MicrosoftXNA are pretty bad IMO. It's convenient to just type "How the hell do I do XYZ?" and get a working response. Likewise, it's nice to ask questions of the form "Does Monogame have XYZ inherently, or do I need to build it?".

Take a shot for every commit that just says "Updated README"

I am wondering if the following should be a function overload of the former (in PhysicsObject):
```
public void UpdatePosition(Vector2 velocity, float dt)
{
    Position += velocity * dt;
}
public void UpdatePosition(Vector2 newPosition)
{
    Position = newPosition;
}
```

It would be convenient as an overload, but I can also foresee myself fucking up in the future and forgetting to pass a `dt` argument. I.e., I'd be re-assigning some object's position as an arbitrary velocity `Vector2`. I think it would be too complicated to define new classes `PositionVector` and `VelocityVector`... For example, this would be annoying in the first function given above; you'd have to handle type conversions and whatnot.

Is it confusing to have `Speed`, `BaseSpeed`, `SpeedScalar`, and `MovementSpeed` all properties of `PhysicsObject`? I don't think so. `BaseSpeed` is basically never going to be changed. `SpeedScalar` is sufficiently descriptive. `Speed` and `MovementSpeed` are the most confusing. Still, not bad.

### 2023-12-13
For new I am imagine each level to be constructed of a grid of tiles. Levels will definitely not be rectangular. They could be divided into chunks; to accomodate this, I will need to write a method that stitches two chunks together. Something like:
```
public LevelObject Stitch(LevelObject A, LevelObject B, Vector2 offset)
{
    // By default, Stitch places B adjacent to the top right corner of A.
    // I.e., C.Shape = [max(A.X, B.X), A.Y + B.Y]
    // The unused components are set to be empty somehow
    //
    // Ex: If A is 3x3 and B is 2x2, C will be 3x5
    // C would look like this:
    // a a a b b
    // a a a b b
    // a a a 0 0 
}
```

I really didn't want to make a custom IntVector2 class. I suppose I could just cast `Shape` as a typical (float) `Vector2` and just be careful when using it. I think this is what I will do.

At this point, I am renaming `LevelObject` to `ChunkObject`. This better indicates that `ChunkObject`s are just chunks of `TileObject`s. Later I will redefine `LevelObject` to be a class that inherits `ChunkObject`, and also has some other cursory information.

Looks like I will need to write a `Matrix` class of my own, since the MonoGame `Matrix` class is always 4x4. This makes sense since 4x4 matrices are used for 3D transformations. I am failing to remember what they are called, but there is a particular type of Vector4 where the last component is 1. I am having flashbacks to working through Lengyel's book. Great book.

I hate writing operator overloads. Although I don't think I will need to (besides [][] for indexing) for `MatrixAny`, since it accepts any type. I think I will also need to for "=".

I'm so bad at bash.

