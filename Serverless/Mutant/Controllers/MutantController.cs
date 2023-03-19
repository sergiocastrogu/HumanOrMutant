using HumanOrMutant.Application.Interfaces;
using HumanOrMutant.Domain.Dtos;
using HumanOrMutant.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mutant.Controllers;

/// <summary>
/// Mutant test
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class MutantController : ControllerBase
{

    private readonly IHumanService _humanService;
    private readonly IStatsService _statsService;
    public MutantController(IHumanService humanService, IStatsService statsService)
    {
        _humanService = humanService;
        _statsService = statsService;
    }


    /// <summary>
    /// Validar si la secuencia de ADN es de un humano o de un mutante
    /// </summary>
    /// <param name="dnaSecuence">Arreglo de sencuencias de ADN</param>
    /// <returns>Retorna el status code 200 cuando es mutante 403 cuando no es mutante</returns>
    /// <response code="200">Retorna cuando es mutante</response>
    /// <response code="403">Retorna cuando no es mutante</response>
    [HttpPost("mutant", Name = "mutant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult Mutant([FromBody] DnaSecuenceEntity dnaSecuence)
    {

        bool result = _humanService.isMutant(dnaSecuence);

        return new JsonResult(null)
        {
            StatusCode = result? 200:403
        };
    }


    /// <summary>
    /// Generar estadisticas de los humanos ingresados, los humanos mutantes y el porcentaje de mutantes.
    /// </summary>
    /// <returns>Retorna el status code 200 con las estadisticas</returns>
    /// <response code="200">Retorna junto con las estadisticas</response>
    [HttpGet("stats", Name = "stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Stats()
    {
        StatisticsDto statisticsDto = _statsService.getStats();
        return Ok(statisticsDto);
    }
}