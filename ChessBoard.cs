using System;

namespace Chess.Domain
{
    public class ChessBoard
    {
        public static readonly int MaxBoardWidth = 7;
        public static readonly int MaxBoardHeight = 7;
        private Pawn[,] pieces;

        public ChessBoard()
        {
            pieces = new Pawn[MaxBoardWidth, MaxBoardHeight];
        }

        public void Add(Pawn pawn, int xCoordinate, int yCoordinate, PieceColor pieceColor)
        {
            try
            {
                pawn.XCoordinate = xCoordinate;
                pawn.YCoordinate = yCoordinate;
                pieces[xCoordinate, yCoordinate] = pawn;
            }
            catch
            {
                throw;
            }
           // throw new NotImplementedException("Need to implement ChessBoard.Add()");
        }

        public bool IsLegalBoardPosition(int xCoordinate, int yCoordinate)
        {
            //set true as the default value
            //this will force the method to validate all potential future cases
            bool result = true;
            //if x fails, return it
            //since you are using a 0 index, adjust the max board width
            if (xCoordinate < 0 || xCoordinate > MaxBoardWidth-1)
            {
                result = false;
                return result;
            }
            //if y fails return it
            //since you are using a 0 index, adjust the max board height
            if (yCoordinate < 0 || yCoordinate > MaxBoardHeight)
            {
                result = false;
                return result;
            }
            return result;

            //throw new NotImplementedException("Need to implement ChessBoard.IsLegalBoardPosition()");
        }

    }
}
