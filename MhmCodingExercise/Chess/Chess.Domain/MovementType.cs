namespace Chess.Domain
{
    public enum MovementType
    {
        Move = 0,
        Capture = 1,
        Special = 2 // used for special moves, like castling, or en passant, or 
            //any other special moves
    }
}
