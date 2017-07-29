using System;

namespace Chess.Domain
{
    public class Pawn
    {
        private ChessBoard _chessBoard;
        private int _xCoordinate;
        private int _yCoordinate;
        private PieceColor _pieceColor;
        
        public ChessBoard ChessBoard
        {
            get { return _chessBoard; }
            set { _chessBoard = value; }
        }

        public int XCoordinate
        {
            get { return _xCoordinate; }
            set { _xCoordinate = value; }
        }
        
        public int YCoordinate
        {
            get { return _yCoordinate; }
            set { _yCoordinate = value; }
        }

        public PieceColor PieceColor
        {
            get { return _pieceColor; }
            private set { _pieceColor = value; }
        }

        public Pawn(PieceColor pieceColor)
        {
            _pieceColor = pieceColor;
        }


        /// <summary>
        /// Constructor for Pawns, containing the board and the piece color. 
        /// </summary>
        /// <param name="pieceColor">White or black</param>
        /// <param name="chessboard">Chessboard is needed for when Pawns are trying to
        /// determine their legal moves</param>
        /// <remarks>I had to add this constructor, in my logic to validate the correct moves and determine 
        /// legal positions</remarks>
        public Pawn(PieceColor pieceColor, ChessBoard chessboard)
        {
            _pieceColor = pieceColor;
            _chessBoard = chessboard;
        }

        /// <summary>
        /// Moves the piece in the Chess board 
        /// </summary>
        /// <param name="movementType"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        public void Move(MovementType movementType, int newX, int newY)
        {
            //first determine if the new position is valid
            //I am using the chessboard 
            if (_chessBoard.IsLegalBoardPosition(newX, newY))
            {
                //verify that the x move is correct
                if(!VerifyXMove(newX, this.XCoordinate))
                {
                    //don't do the move
                    return; 
                }

                //verify that the Y move is correct
                if (!VerifyYMove(newY, this.YCoordinate))
                {
                    //don't do the move
                    return;
                }

                this.XCoordinate = newX;
                this.YCoordinate = newY;


                _chessBoard.Add(this, this.XCoordinate, this.YCoordinate, this.PieceColor);
            }
            else
            {
                string result = string.Format("Not an acceptable move X: {0} Y: {1}", newX, newY);
            }
            
           // throw new NotImplementedException("Need to implement Pawn.Move()");
        }

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

        protected string CurrentPositionAsString()
        {
            return string.Format("Current X: {1}{0}Current Y: {2}{0}Piece Color: {3}", Environment.NewLine, XCoordinate, YCoordinate, PieceColor);
        }

        private bool VerifyXMove(int newX, int OldX)
        {
            if (newX < OldX)
            {
                //can't move backwards
                return false;
            }

            if ((newX - OldX) > 1)
            {
                //the pawn can't jump 2 spaces
                return false;
            }
            return true;
        }

        private bool VerifyYMove(int newY, int oldY)
        {
            //if (newY < oldY)
            //{
            //    //can't move sideways
            //    return false;
            //}

            if ((newY - oldY) > 1)
            {
                //the pawn can't jump 2 spaces
                return false;
            }
            return true;
        }

    }
}
