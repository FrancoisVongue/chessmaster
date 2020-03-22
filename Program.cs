using System;

namespace chessmaster
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new ChessBoard(8);
            var knight = new Knight(new Position(){x = 2, y = 0});
            var master = new Master(knight, board);

            var positions = master.Traverse();
            if (positions != null)
            {
                board.NumeratePositions(positions);
                board.PrintAnimated(positions);
            }
            else
            {
                Console.WriteLine("Master couldn't solve the problem :(");
            }
        }
    }
}