using HumanOrMutant.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanOrMutant.Application.Interfaces
{
    public interface IStatsService
    {
        StatisticsDto getStats();
    }
}
