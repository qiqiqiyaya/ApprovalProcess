﻿namespace Ap.Core.Behaviours
{
    /// <summary>
    /// Go directly to the next state
    /// </summary>
    /// <param name="destination"></param>
    public class Direct(string destination) : BehaviourBase(ApCoreTriggers.Direct, destination)
    {

    }
}
