﻿namespace Ap.Core.Definitions.States;

public class EndState(string builderId) : StateBase(EndStateName + builderId)
{
    private const string EndStateName = "End_";
}
