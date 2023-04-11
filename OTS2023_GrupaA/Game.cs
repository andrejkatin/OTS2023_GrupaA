using OTS2023_GrupaA.Exceptions;
using OTS2023_GrupaA.Models;


namespace OTS2023_GrupaA
{
    public enum Move
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Score
    {
        Bad,
        Average,
        Good
    }

    public class Game
    {
        public Player Player { get; set; }
        public Map Map { get; set; }


        public Game(Position playerPosition, Position revealHiddenItemPosition)
        {
            Map = new Map();
            Map.InitializeMap();

            if (!ValidatePositionInsideMap(playerPosition) || !ValidatePositionInsideMap(revealHiddenItemPosition))
            {
                throw new PositionOutsideOfMapException("Positions must be valid!");
            }

            int itemX = revealHiddenItemPosition.X;
            int itemY = revealHiddenItemPosition.Y;

            Map.Tiles[itemX, itemY].Content = TileContent.RevealHiddenItem;
            Player = new Player(playerPosition);
        }

        public void MovePlayer(Move move)
        {
            Position playerPositionAfterMove = Player.GetPositionAfterMove(move);
            bool positionIsValid = ValidatePosition(playerPositionAfterMove);
            if (positionIsValid)
            {
                Player.MakeMove(move);
            }
        }

        public bool ValidatePosition(Position position)
        {
            int x = position.X;
            int y = position.Y;

            if (!ValidatePositionInsideMap(position))
            {
                return false;
            }
            if(Map.Tiles[x, y].Type.Equals(TileType.Hidden))
            {
                return Player.CanRevealHidden;
            }
            else
            {
                return true;
            }
        }

        private bool ValidatePositionInsideMap(Position position)
        {
            int x = position.X;
            int y = position.Y;

            if (x < 0 || x >= Map.MapSize || y < 0 || y >= Map.MapSize)
            {
                return false;
            }
            if (Map.Tiles[x, y].Type.Equals(TileType.MapBarrier))
            {
                return false;
            }
            return true;
        }

        public void CollectItems()
        {
            int x = Player.Position.X;
            int y = Player.Position.Y;

            if (Map.Tiles[x, y].Content.Equals(TileContent.Gold))
            {
                if (Map.Tiles[x, y].Type.Equals(TileType.Hidden))
                    Player.AmountOfHiddenGold++;
                else
                    Player.AmountOfGold++;
            }
            else if(Map.Tiles[x, y].Content.Equals(TileContent.RevealHiddenItem))
            {
                Player.CanRevealHidden = true;
            }

            Map.EmptyTileOnPosition(Player.Position);
        }


        public Score CalculateScore()
        {
            if(Player.AmountOfHiddenGold > 10)
            {
                return Score.Good;
            }
            if(Player.AmountOfGold > 15 && Player.CanRevealHidden)
            {
                if (Player.AmountOfHiddenGold < 5)
                {
                    return Score.Average;
                }
                else
                {
                    return Score.Good;
                }
            }
            return Score.Bad;
        }

    }
}
