//  Parallax
//  classes:
//    - Layer(graphics (.bmp, .png? .tif?), depth (float), speed (float))
//        - Init position (Vector2) to (0f,0f)
//  methods:
//    - void Add(Layer)
//    - void Remove(Layer)
using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pevensie;

public class Parallax
{
    public List<Layer> Layers = new List<Layer>();
    public class Layer
    {
        public string ID { get; set; }
        public Texture2D Texture { get; set; }
        public float Depth { get; set; }
        public Vector2 Position { get; set; } = new Vector2(0f, 0f);
        public float SpeedScalar { get; set; } = 1f;

        public Layer(string id, Texture2D texture, float depth, Vector2 position = default(Vector2), float speedScalar = 1f)
        {
            ID = id;
            Texture = texture;
            Depth = depth;
            Position = position;
            SpeedScalar = speedScalar;
        }

    }
    public void AddLayer(Parallax.Layer newLayer)
    {
        Layers.Add(newLayer);
    }
}


