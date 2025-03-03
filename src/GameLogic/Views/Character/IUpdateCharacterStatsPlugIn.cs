﻿// <copyright file="IUpdateCharacterStatsPlugIn.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.GameLogic.Views.Character;

/// <summary>
/// Interface of a view whose implementation informs about changed character stats.
/// </summary>
public interface IUpdateCharacterStatsPlugIn : IViewPlugIn
{
    /// <summary>
    /// Updates the character stats.
    /// </summary>
    void UpdateCharacterStats();
}