

namespace OTS2023_GrupaA.Models
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }
        public static readonly int MapSize = 30;

        public Map()
        {
            Tiles = new Tile[MapSize, MapSize];    
        }

        public void InitializeMap()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Tiles[i, j] = new Tile();
                }
            }

            CreateRectangleBarrier(5, 5, 5, 20);
            CreateRectangleBarrier(20, 5, 5, 20);
        }

        public void CreateRectangleBarrier(int upperLeftCornerX, int upperLeftCornerY, int width, int height)
        {
            for(int i=upperLeftCornerX; i< upperLeftCornerX + width; i++)
            {
                for(int j=upperLeftCornerY; j< upperLeftCornerY + height; j++)
                {
                    Tiles[i, j].Type = TileType.MapBarrier;
                }
            }
        }

        public void AddTile(TileType type, TileContent content, int x, int y)
        {
            if (!type.Equals(TileType.MapBarrier))
            {
                Tiles[x, y].Type = type;
                Tiles[x, y].Content = content;
            }
        }

        public void EmptyTileOnPosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            Tiles[x, y].Content = TileContent.Empty;
        }

    }
}
