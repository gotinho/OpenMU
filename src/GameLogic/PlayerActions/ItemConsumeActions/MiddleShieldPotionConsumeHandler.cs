﻿// -----------------------------------------------------------------------
// <copyright file="MiddleShieldPotionConsumeHandler.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace MUnique.OpenMU.GameLogic.PlayerActions.ItemConsumeActions;

/// <summary>
/// Consume handler for middle shield potions.
/// </summary>
public class MiddleShieldPotionConsumeHandler : ShieldPotionConsumeHandler
{
    /// <inheritdoc/>
    protected override int RecoverPercent
    {
        get { return 40; }
    }
}