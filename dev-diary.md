# Dev Diary
### 2023-12-12
This is the first diary entry, but I think this project started two days ago. Building a game has been something I've been interested in a long time, and I guess it's finally decide if it's something that I am actually interested in. There's a lot of math going on under the hood that I would eventually like to understand; graphics programming and rendering is immensely interesting, but complicated and not appropriate for my current skill level.

The term *game engine* is pretty loose, as is basically every term in tech (no one really knows what middleware is). Of course, MonoGame is an engine in the sense that it handles all the rendering as implements a bunch of basic, necessary classes like `Vector2`. However, the upfront work has been building up my own framework to handle physics, and perhaps later, things like level generation, etc.

Like everyone else, I have installed Unity about 5 times throughout my life, launched it once following each install, and never touched it again. At one point, I had done so similarly for Unreal, Cry (I still have a softspot for Crytek), and Godot (yes, following the Unity events of 2023). Something about the GUI of these was intimidating to me, perhaps because it emphasized my lack of artistic ability. Also, these are all mainly in 3D, which is a whole dimension scarier than 2D. I feel at home in VS Code, and MonoGame plays to that.

More specific thoughts:

Making `physicsObject` was a bit like creating a "block" class for MineCraft. It will be used for basically everything. The commit *Complete physics overhaul* definitely should have been developed as its own branch, had I not implemented the whole thing in a continuous few hours.

"Physcs overhaul" is also not super descriptive, since the primary purpose of the update was to abstract a lot that was being done in `Main.cs`. E.g., I think this commit is also when I added the `PlayerObject` class. Instead of loading in the player texture in `Main.cs`, this is all mostly done in `gameState.Player` (I think).

ChatGPT has been extremely helpful for this project. By any reasonable metric, I do not know C-Sharp. Also, the docs for Mono/MonoGame/MicrosoftXNA are pretty bad IMO. It's convenient to just type "How the hell do I do XYZ?" and get a working response. Likewise, it's nice to ask questions of the form "Does Monogame have XYZ inherently, or do I need to build it?".
