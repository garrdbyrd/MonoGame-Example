using System;

namespace Pevensie;

public class TileMatrixClass
{
    private TileObject[,] _matrix;

    public int Rows { get; }
    public int Columns { get; }

    public TileMatrixClass(int rows, int columns)
    {
        if (rows <= 0)
        {
            throw new ArgumentException("Argument \"rows\" must be greater than zero.");
        }
        if (columns <= 0)
        {
            throw new ArgumentException("Argument \"columns\" must be greater than zero.");
        }

        Rows = rows;
        Columns = columns;
        _matrix = new TileObject[rows, columns];
    }
}

public class ChunkObject
{
    public TileMatrixClass TileMatrix;
}