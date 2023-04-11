﻿
namespace OTS2023_GrupaA.Models
{

    public class Player
    {
        public Position Position { get; set; }
        public int AmountOfGold { get; set; }
        public int AmountOfHiddenGold { get; set; }
        public bool CanRevealHidden { get; set; }

        public Player()
        {
        }

        public Player(Position position)
        {
            Position = position;
        }


        public void MakeMove(Move move)
        {
            switch (move)
            {
                case Move.Up:
                    MoveUp();
                    break;
                case Move.Down:
                    MoveDown();
                    break;
                case Move.Left:
                    MoveLeft();
                    break;
                case Move.Right:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        public void MoveUp()
        {
            Position.Y--;
        }

        public void MoveDown()
        {
            Position.Y++;
        }

        public void MoveLeft()
        {
            Position.X--;
        }

        public void MoveRight()
        {
            Position.X++;
        }

        public Position GetPositionAfterMove(Move move)
        {
            int x = Position.X;
            int y = Position.Y;
            switch (move)
            {
                case Move.Up:
                    return new Position(x, y - 1);
                case Move.Down:
                        return new Position(x, y + 1);
                case Move.Left:
                    return new Position(x - 1, y);
                case Move.Right:
                    return new Position(x + 1, y);
                default:
                    return null;
            }
        }

    }
}
