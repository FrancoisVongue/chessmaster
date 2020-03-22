using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace chessmaster
{
    public struct Position
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool Visited { get; set; }
        public int Number { get; set; }

        public bool SamePositionAs(Position pos)
        {
            return pos.x == x && pos.y == y;
        }
    }
    
    public class ChessBoard
    {
        public Position[,] Board;

        public ChessBoard(int sideLength)
        {
            Board = new Position[sideLength, sideLength];

            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    Board[i, j] = new Position(){ Visited = false, x = i, y = j };
                }
            }
        }

        public void NumeratePositions(List<Position> positions)
        {
            if (positions == null) return;
            
            int n = 1;
            foreach (var p in positions)
            {
                Board[p.x, p.y].Number = n++;
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    var currentCell = Board[j, i];
                    if(currentCell.Visited)
                        Console.Write(" + ");
                    else
                        Console.Write(" - ");

                    if (currentCell.x == Board.GetLength(0) - 1)
                        Console.Write('\n');
                }
            }
        }

        public void PrintAnimated(List<Position> positions)
        {
            for (int i = 1; i < positions.Count(); i++)
            {
                var originalX = Console.CursorLeft;
                var originalY = Console.CursorTop;
                
                PrintPositions(positions.GetRange(0,i));
                Thread.Sleep(500);
                Console.SetCursorPosition(originalX, originalY);
            }

            PrintPositions(positions);
        }

        public void UnVisit(Position p)
        {
            Board[p.x, p.y].Visited = false;
        }

        public void Visit(Position p)
        {
            Board[p.x, p.y].Visited = true;
        }

        public void PrintPositions(List<Position> positions)
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    var currentCell = Board[j, i];
                    
                    if (positions.Any(p => p.SamePositionAs(currentCell)))
                        Console.Write(" {0:D2}", currentCell.Number);
                    else if(currentCell.Visited)
                        Console.Write(" + ");
                    else
                        Console.Write(" - ");

                    if (currentCell.x == Board.GetLength(0) - 1)
                        Console.Write('\n');
                }
            }
        }
    }
}