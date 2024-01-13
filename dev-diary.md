# Dev Diary
### 2023-12-12
This is the first diary entry, but I think this project started two days ago. Building a game has been something I've been interested in a long time, and I guess it's finally decide if it's something that I am actually interested in. There's a lot of math going on under the hood that I would eventually like to understand; graphics programming and rendering is immensely interesting, but complicated and not appropriate for my current skill level.

The term *game engine* is pretty loose, as is basically every term in tech (no one really knows what middleware is). Of course, MonoGame is an engine in the sense that it handles all the rendering as implements a bunch of basic, necessary classes like `Vector2`. However, the upfront work has been building up my own framework to handle physics, and perhaps later, things like level generation, etc.

Like everyone else, I have installed Unity about 5 times throughout my life, launched it once following each install, and never touched it again. At one point, I had done so similarly for Unreal, Cry (I still have a soft spot for Crytek), and Godot (yes, following the Unity events of 2023). Something about the GUI of these was intimidating to me, perhaps because it emphasized my lack of artistic ability. Also, these are all mainly in 3D, which is a whole dimension scarier than 2D. I feel at home in VS Code, and MonoGame plays to that.

More specific thoughts:

Making `physicsObject` was a bit like creating a "block" class for MineCraft. It will be used for basically everything. The commit *Complete physics overhaul* definitely should have been developed as its own branch, had I not implemented the whole thing in a continuous few hours.

"Physics overhaul" is also not super descriptive, since the primary purpose of the update was to abstract a lot that was being done in `Main.cs`. E.g., I think this commit is also when I added the `PlayerObject` class. Instead of loading in the player texture in `Main.cs`, this is all mostly done in `gameState.Player` (I think).

ChatGPT has been extremely helpful for this project. By any reasonable metric, I do not know C-Sharp. Also, the docs for Mono/MonoGame/MicrosoftXNA are pretty bad IMO. It's convenient to just type "How the hell do I do XYZ?" and get a working response. Likewise, it's nice to ask questions of the form "Does MonoGame have XYZ inherently, or do I need to build it?".

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
For new I am imagine each level to be constructed of a grid of tiles. Levels will definitely not be rectangular. They could be divided into chunks; to accommodate this, I will need to write a method that stitches two chunks together. Something like:
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

I'm so bad at bash. Python is easier.

I'm kind of tempted to write a VS Code extension that just launches `mgcb-editor` at the click of a button because I can never remember what it's called. Maybe putting it in this file will help me remember, even though it's already in the README.

I was thinking that each chunk would have its own background for some reason. Obviously it just makes way more sense to add a `Texture2D Background` to `LevelObject`. Parallax will be much easier.

### 2023-12-14
No real progress today but breaks are nice. Here is a transcription for some calculations I did earlier.

Regarding graphics/resolution/etc. all calculations are for 1920/1080. These are done to calculate the relative pixel size of various games.

**Celeste:**
- 6x6 pixels
- 320x180 resolution
- player: 13x17 pixels (roughly) [78x102 real pixels]

**Blasphemous**
- 3x3 pixels
- 640x360 resolution
- Traffic cone head is too big for a reasonable calculation

**FEZ**
Standard:
- 3x3 pixels
- 320x180 resolution
- player: 13x19 pixels (roughly) [39x57 real pixels]

Zoomed-In Rooms;
- 6x6 pixels
- 320x180 resolution
- player: 13x19 pixels (roughly) [78x114 real pixels]

**The Binding of Isaac: Rebirth**
- 4x4 pixels
- 480x270 resolution
- Forget to measure, player size could be easily found

So far I'm going with 3x3 pixels. I will have to figure out how to make 3x3 pixels behave as such. That is, so there are no "in-between pixels". I am very tired.

### 2023-12-30
I have had a nice little Christmas break. I purchased a bunch of new monitors (after destroying the HDMI port on one like an idiot) and also a power drill and impact driver (irrelevant).

I have been considering how to handle level construction. There are two parts to this, which I will attempt to describe"
1. The underlying data structure for levels, chunks, tiles, etc.
2. How to actually construct levels

For the former, it seems that I have two slightly annoying options:
1. JSON
2. proprietary binary file

Before I get into the details of each of these, I will outline how I expect what a prototype of the level filetype to contain:
**meta information:**
- Level title
- Associated tracks (music)
- level height (in tiles)
- level width  (in tiles)

**tile-specific information:**
- texture (not the texture itself encoded in the file, but some texture identity to map the right tile texture)
- collision (as well as collision behaviors, if it changes)
- physics effects (slippery/ice, slow/"sticky", etc)
- coordinates (e.g., tile is at [12, 14])

This is an extremely rough outline but gets the idea across.

If parsed as a JSON, the outline is fairly straight forward. Two main sections `meta` and `tiles` or something similar, along with the appropriate subsections for the meta data, and a potentially long subsection for each individual tile in the `tiles` section. (Note: I'm not really sure what the different layers of JSON are called. I know they are name/value pairs, but think I have always just called them layers or sections or keys, as in python dicts. Googling did not help really.)

Alternatively, (the fun way) is to just write all the raw information into a proprietary binary file. 

Once in academia, I had a non-coding-savvy friend who desperately needed to read some data that was written in a proprietary format. Luckily, we had a previous student's script to go off of, but unluckily, it was written the the language which should not be named (MATLAB). I think the format ended up being as follows:
- The first 8 bytes are reserved for two (unsigned?) 32-bit integers, which describe the shape of the data (we were parsing a 2D-array, call these ints N and M, respectively)
- All remaining data were float64, and could be read in as a NxM array.
- There might have been some metadata prepended, I can't really remember

I could do a similar thing for my level data. I would like to reserve the first arbitrary amount of data (maybe a kilobyte, maybe several kilobyte, maybe <1) for metadata. Included in this would be the dimensions of the level's tile grid. For every level, starting after the kilobyte, the remaining data could be read in as a 2D array of tiles.

I will have to figure out how much memory to reserve for a tile. It probably won't be much, but a tile could probably be reduced to a short sequence of uint32's.

This filetype I could reserve as `.level` or something similar. A version without metadata could be `.chunk`, which could be loaded into a level during level design.

This segues me into a dangerous, dreadful thought. I need an external GUI to create and edit levels. Two options immediately stick out: build one with PyQt, or with Qt/C++. I do not have a knee-jerk reaction as to which one I prefer. (I could also build something in Electron, but fuck that.) I think I will try my hand at classic Qt. We will see how frustrating it is. I just need to build a bare-bones version of something similar to the HexManiac editor for the GBA Pokemon games.

Historically, say in the GBA days, the tilesets for a specific level of a game would be loaded in dynamically, since the GBA had only so much RAM. E.g., Pallet Town loads in its tileset, which is different from the Route 1 tileset of Pokemon FireRed. We see this today even in 3D games, since those kinds of textures tend to be high-res. E.g., Firelink Shrine loads in its textures, Blighttown loads in its texture, etc for Dark Souls. (This analogy isn't perfect; I'm sure there is some more nuances stuff going on.) Anyway, since my tile textures are 1. used repetitively (many tiles will share a texture) and 2. are low-res, FOR NOW, I am sticking to the belief that I can get away with having one tileset be loaded into RAM at all times. This is likely a naive thought, but I will address my mistakes when they arise.

What this implies for `.level` and `.chunk` files: instead of encoding many tile textures in my proprietary file, I can assign each tile texture an integer value. E.g., 1:grass, 2:stone, etc. (these are primitive examples). The above assumption about global texture values means that "1" will always point to "grass". To revert to the Pokemon analogy, a building tile in Pallet Town might point to a rock tile in the Mount Moon tileset, since a different tileset is loaded.

### 2024-01-01
Happy new year. 

Qt is hard. Also Qt Foundation makes it very difficult to find/install the FOSS version, so here it is: https://www.qt.io/download-qt-installer

The fetal version of my level editor can be found here: https://github.com/garrdbyrd/MonoGame-Level-Editor

Qt Creator is interesting. The *Design* tool (which is basically just Qt Designer) is not very intuitive. I should be able to drag/drop within the object list, such as in Inkscape, Roblox, etc. It's coming along though. But it's not very fun.

### 2024-01-05
Qt fucking sucks. I'm trying PyQt.

Update: PyQt sucks too. This is whiny, but I hate being forced into their shitty proprietary IDE. Using documented methods for converting .ui to .py files just doesn't work. What a headache. Back to Qt/C++.

### 2024-01-06
Qt is till pretty frustrating, but I am getting the hang of it. I ran into my first segfault (and fixed it), so I must be doing something right. The robot has been an immense help. 

I am making a pleasant amount of progress. That probably means things will suck when I start on the main graphics viewer.

### 2024-01-10
Things are coming along. Slowly.

<br/>
Premature optimization is the root of all evil.
<br/>
Premature optimization is the root of all evil.
<br/>
Premature optimization is the root of all evil.
<br/><br/>
When writing the `.level` and `.chunk` formats, I do not need to worry about optimizing integer data types.
<br/>
When writing the `.level` and `.chunk` formats, I do not need to worry about optimizing integer data types.
<br/>
When writing the `.level` and `.chunk` formats, I do not need to worry about optimizing integer data types.
<br/><br/>
I will just use shorts until memory performance issues become apparent.
<br/>
I will just use shorts until memory performance issues become apparent.
<br/>
I will just use shorts until memory performance issues become apparent.
<br/>
<br/>

\- A poem inspired by Daniil Kharms

### 2024-01-11

For 3x3 pixels: 20x11.25 (16x16 px tiles)

For 6x6 pixels: 40x22.5 (16x16 px tiles)

### 2024-01-12
The very next thing I need to do in caspian is to create toolbar buttons for undo/redo. Then that will be my first checkmark.

My memory management is very, very bad. Currently the undoStack and redoStack are saving **every single texture** that is changed between each state. For what I am doing, this is not *too bad* since my textures are only 16x16. However, in a stress test, I bumped up the rendered textures to 1080x1080. This led to several GBs of RAM being used, compared to the 50~100MB in my typical usage.

A better system would be to create a texture reference manager whenever the textures are loaded in, and just store the pointers to these on the stack. I might fix this later, but for now I don't really care. This is a personal project, and I will not be using massive textures.