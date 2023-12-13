# Pevensie: A MonoGame Example
This is a basic example of a game created using the **MonoGame Game Engine** ([link](https://monogame.net)). This is just a fun personal project, it is untelling when it will be updated. Still, I think it provides some basic examples for some useful things in MonoGame.

# Features
- Move a pumpkin around the screen
- ~~Change the color of the square~~
- GamePad support
- Keyboard support
- Travel at Walk and Run speeds

# Development Environment
## Software
- VS Code (technically [VSCodium](https://vscodium.com))
- [Arch Linux](https://archlinux.org) (I use Arch, btw)
- [.NET 7](https://wiki.archlinux.org/title/.NET) (will likely move to .NET 8 soon)
- [Mono](https://wiki.archlinux.org/title/Mono)

## Hardware
- NVIDIA GTX 1660
- Intel 13700KF
- 32GB DDR5 RAM

## Humanware
- Slacks/jeans/hoodie from Target
- Coffee from Dunkin
- Lots of eggnog (developed in-house, 2023 winter update)

# Notes
- You need to run `dotnet tool install --global dotnet-mgcb-editor ` to install the mgcb editor. run it with `mgcb-editor`
- I think all you need to build is dotnet7 and mono. For Arch (I use Arch, btw):
```
    sudo pacman -S dotnet-runtime-7.0 dotnet-sdk-7.0 mono
```
- And then just do `dotnet build` and then `dotnet run`
- Or, if you use VS Code with the C#/DOTNET extension, you can just build/launch with the debugger. Which you should do.

# To Do List
- `MetaGameState` as profile save \n keep track of jiggies collected, total deaths, which levels are opened, bosses killed, etc.
- `LevelObject` for levels
- ~~`TileObject` for tiles to construct levels~~
- Serialize everything as JSON (MetaGameState, LevelObject?)
- Read graphics settings from a config
- Add `Texture2D Background` property to `ChunkObject`
