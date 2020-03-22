using System;
using System.Collections.Generic;
using System.Linq;

namespace chessmaster
{
    public class Master
    {
        private readonly Knight _knight;
        private readonly ChessBoard _board;

        public Master(Knight knight, ChessBoard board)
        {
            _knight = knight;
            _board = board;
            _board.Visit(_knight.GetPosition());
        }

        private void MoveKnight(Position p)
        {
            _board.Visit(p);
            _knight.ChangePosition(p);
        }
        
        private void MoveKnightBack(Position to, Position from)
        {
            _board.UnVisit(from);
            _knight.ChangePosition(to);
        }

        public List<Position> Traverse()
        {
            var length = _board.Board.GetLength(0);
            return MakeNMoves(length * length - 1);
        }

        private List<Position> MakeNMoves(int movesToDo)
        {
            if ( movesToDo == 0 ) 
                return new List<Position>();

            var possiblePositions = GetPossiblePositions(movesToDo);
            
            if (possiblePositions.Count() < 1) 
                return null;
            
            var currentPosition = _knight.GetPosition();

            foreach (var possiblePosition in possiblePositions)
            {
                var successfulPath = TryPosition(possiblePosition, movesToDo);
                if (successfulPath != null)
                    return successfulPath;
            }
            
            return null;
        }

        private List<Position> GetPossiblePositions(int movesToDo)
        {
            var nextPositions = _knight.GetNextPositions(_board)
                .Where(p =>!p.Visited);

            return nextPositions.ToList();
        }

        private List<Position> TryPosition(Position newPosition, int movesToDo)
        {
            var knightPosition = _knight.GetPosition();
            MoveKnight(newPosition);
            var nextMoves = MakeNMoves(movesToDo - 1);
            if (nextMoves != null)
            {
                var successfulPath = nextMoves;
                successfulPath.Insert(0, newPosition);
                return successfulPath;
            }
            else
            {
                MoveKnightBack(knightPosition, newPosition);
                return null;
            }
        }
    }
}