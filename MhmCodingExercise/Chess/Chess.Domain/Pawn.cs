﻿using System;
using System.Text;

namespace Chess.Domain
{
    public class Pawn : ChessPiece, IValidateMove
    {
        private ChessBoard _chessBoard;



        public ChessBoard ChessBoard
        {
            get { return _chessBoard; }
            set { _chessBoard = value; }
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
        public override void Move(MovementType movementType, int newX, int newY)
        {
            Logger.LogMessage(string.Format("Pawn at current location of {0},{1} is doing a movement type of {2} to {3}, {4}", XCoordinate, YCoordinate, movementType.ToString(), newX, newY), LogLevel.info);

            try
            {
                //first determine if the new position is valid
                //I am using the chessboard method to determine if the new X and Y are located on the 
                //chess board
                if (_chessBoard.IsLegalBoardPosition(newX, newY))
                {

                    //determine legal moves for move vs capture
                    switch (movementType)
                    {
                        case MovementType.Move:

                            //verify that the x move is correct
                            if (!VerifyXMove(newX))
                            {
                                Logger.LogMessage("the new x coordinate is incorrect " + newX, LogLevel.error);
                                //don't do the move
                                return;
                            }

                            //verify that the Y move is correct
                            if (!VerifyYMove(newY))
                            {
                                Logger.LogMessage("the new y coordinate is incorrect " + newY, LogLevel.error);
                                //don't do the move
                                return;
                            }
                            break;

                        //in this case Pawns can only move diagonally
                        case MovementType.Capture:
                            if (!VerifyDiagonalMove(newX, newY))
                            {
                                Logger.LogMessage("the new x  " + newX + " or y coordinate is incorrect " + newY, LogLevel.error);
                                return;
                            }
                            break;

                        case MovementType.Special:
                            if(!VerifySpecialMove(newX, newY))
                            {
                                Logger.LogMessage("the new x  " + newX + " or y coordinate is incorrect " + newY, LogLevel.error);
                                return;
                            }
                            break;
                        default:

                            break;
                    }

                    this.XCoordinate = newX;
                    this.YCoordinate = newY;

                    _chessBoard.Add(this, this.XCoordinate, this.YCoordinate, this.PieceColor);
                }
                else
                {
                    string result = string.Format("Not an acceptable move X: {0} Y: {1}", newX, newY);
                    Logger.LogMessage(result, LogLevel.error);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage("An exception occurred" + ex.Message, LogLevel.error);
            }
        }

        public bool VerifyXMove(int newX)
        {
            if (!XMoveSideways(newX))
            {
                return false;
            }

            if (!XMoveOnlyOne(newX))
            {
                return false;
            }

            return true;
        }

        public bool VerifyYMove(int newY)
        {
            //if (newY < oldY)
            //{
            //    //can't move sideways
            //    return false;
            //}

            if ((newY - YCoordinate) > 1)
            {
                //the pawn can't jump 2 spaces
                return false;
            }
            return true;
        }

        /// <summary>
        /// Should only be used by the Capture Movement, validates that the pawn is attacking diagonally
        /// </summary>
        /// <param name="newX"></param>
        /// <returns></returns>
        public bool VerifyDiagonalMove(int newX, int newY)
        {
            //verify that new x Coordinate is either 1 greater or 1 less than the current x position
            if ((newX == (XCoordinate + 1) || newX == (XCoordinate - 1)))
            {
                //verify that the new Y position is 1 greater than the current Y position
                if (newY == YCoordinate + 1)
                {
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Verifies Pawns Special moves, like en passant, or the first move where they 
        /// can jump two spaces
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <returns></returns>
        public bool VerifySpecialMove(int newX, int newY)
        {
            StringBuilder message = new StringBuilder();
            message.AppendFormat("Verifying special move X: {0} Y: {1}", newX, newY);
            bool result = true;

            //check first if it is the pawns beginning position
            if (YCoordinate == 1)
            {
                if (newY == YCoordinate+2)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            //TODO figure out how to do the move for en passant.

            return result;
        }

        /// <summary>
        /// Verify that the Pawn cannot move sideways,
        /// this should only be called by the Movement Capture type
        /// </summary>
        /// <param name="newX">new x position</param>
        /// <param name="OldX">old x position</param>
        /// <returns></returns>
        private bool XMoveSideways(int newX)
        {
            if (newX != XCoordinate)
            {
                //can't move sideways
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verify that the pawn only moves one space
        /// </summary>
        /// <param name="newX"></param>
        /// <returns></returns>
        private bool XMoveOnlyOne(int newX)
        {
            if ((newX - XCoordinate) > 1)
            {
                //the pawn can't jump 2 spaces
                return false;
            }
            return true;
        }


    }
}
