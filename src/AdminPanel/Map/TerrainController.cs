﻿// <copyright file="TerrainController.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.AdminPanel.Map;

using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUnique.OpenMU.GameLogic;
using MUnique.OpenMU.Interfaces;
using SixLabors.ImageSharp;

/// <summary>
/// Controller for all map related functions.
/// </summary>
[Route("[controller]")]
[ApiController]
public class TerrainController : Controller
{
    private readonly IList<IManageableServer> _servers;
    private readonly ILogger<TerrainController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TerrainController"/> class.
    /// </summary>
    /// <param name="servers">The servers.</param>
    /// <param name="logger">The logger.</param>
    public TerrainController(IList<IManageableServer> servers, ILogger<TerrainController> logger)
    {
        this._servers = servers;
        this._logger = logger;
    }

    /// <summary>
    /// Renders and returns the terrain of the specified server and map identifier.
    /// </summary>
    /// <param name="serverId">The server identifier.</param>
    /// <param name="mapId">The map identifier.</param>
    /// <returns>The rendered terrain.</returns>
    [HttpGet("{serverId}/{mapId}")]
    public async Task<IActionResult> Terrain(int serverId, int mapId)
    {
        var gameServer = this._servers.OfType<IGameServer>().FirstOrDefault(s => s.Id == serverId);

        var map = gameServer?.ServerInfo.Maps.FirstOrDefault(m => m.MapNumber == mapId);
        if (map is null)
        {
            this._logger.LogWarning($"requested map not available. map number: {mapId}; server id: {serverId}");
            return this.NotFound();
        }

        this.Response.ContentType = "image/png";
        await using var mapStream = this.RenderMap(map);

        // we need to set the length before writing the data into the body,
        // otherwise it gets "chunked".
        this.Response.ContentLength = mapStream.Length;
        await mapStream.CopyToAsync(this.Response.Body).ConfigureAwait(false);
        return this.Ok();
    }

    private Stream RenderMap(IGameMapInfo map)
    {
        var terrain = new GameMapTerrain(map.TerrainData);
        using var bitmap = terrain.ToImage();
        var memoryStream = new MemoryStream();
        bitmap.SaveAsPng(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }
}