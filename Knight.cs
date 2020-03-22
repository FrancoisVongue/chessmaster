using System;
using System.Collections;
using System.Collections.Generic;

namespace chessmaster
{
    public class Knight
    {
        private Position _position;

        public Knight(Position startPosition)
        {
            _position = startPosition;
        }

        public void ChangePosition(Position p)
        {
            _position = p;
        }

        public Position GetPosition()
        {
            return _position;
        }

        public IEnumerable<Position> GetNextPositions(ChessBoard cb)
        {
            List<Position> nextPositions = new List<Position>();
            
            foreach (var position in cb.Board)
            {
                var shiftX = Math.Abs(position.x - _position.x);
                var shiftY = Math.Abs(position.y - _position.y);
                bool allowedMove = shiftX + shiftY == 3 && (shiftX == 1 || shiftY == 1);
                
                if(allowedMove) 
                    nextPositions.Add(position); 
            }

            return nextPositions;
        }
    }
}