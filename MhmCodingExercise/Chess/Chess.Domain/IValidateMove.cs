﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
    public interface IValidateMove
    {
        bool VerifyXMove(int newXCoordinate);

        bool VerifyYMove(int newYCoordinate);
    }
}
