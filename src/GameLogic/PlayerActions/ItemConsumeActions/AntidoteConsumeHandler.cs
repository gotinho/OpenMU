﻿// -----------------------------------------------------------------------
// <copyright file="AntidoteConsumeHandler.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace MUnique.OpenMU.GameLogic.PlayerActions.ItemConsumeActions;

/// <summary>
/// Consume handler for the antidote potion. It removes the poison effect from the player.
/// </summary>
public class AntidoteConsumeHandler : BaseConsumeHandler
{
    private const short PoisonEffectNumber = 0x37;

    /// <inheritdoc />
    public override bool ConsumeItem(Player player, Item item, Item? targetItem, FruitUsage fruitUsage)
    {
        if (base.ConsumeItem(player, item, targetItem, fruitUsage))
        {
            if (player.MagicEffectList.ActiveEffects.TryGetValue(PoisonEffectNumber, out var effect))
            {
                effect.Dispose();
            }

            return true;
        }

        return false;
    }
}