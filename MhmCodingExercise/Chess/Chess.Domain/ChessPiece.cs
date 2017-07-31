using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
    /// <summary>
    /// this is the base class of all chess pieces
    /// </summary>
    public class ChessPiece
    {

        private int _xCoordinate;
        private int _yCoordinate;
        protected PieceColor _pieceColor;

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


        /// <summary>
        /// //provides the template for all chess pieces to move  
        /// </summary>
        /// <param name="moveType">Move types are either move or capture</param>
        /// <param name="XCoordinate">X coordinate on the chess board</param>
        /// <param name="YCoordinate">Y coordinate on the chess board</param>
        public virtual void Move (MovementType moveType, int XCoordinate, int YCoordinate)
        {
            this.XCoordinate = XCoordinate;
            this.YCoordinate = YCoordinate;
        }

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

        protected string CurrentPositionAsString()
        {
            return string.Format("Current X: {1}{0}Current Y: {2}{0}Piece Color: {3}", Environment.NewLine, XCoordinate, YCoordinate, PieceColor);
        }
    }
}
