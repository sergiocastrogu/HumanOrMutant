using HumanOrMutant.Application.Interfaces;
using HumanOrMutant.Domain.Dtos;
using HumanOrMutant.Persistence.Contracts;

namespace HumanOrMutant.Application.Services
{
    public class StatsService : IStatsService
    {
        private readonly IHumanDnaRepository _humanDnaRepository;
        public StatsService(IHumanDnaRepository humanDnaRepository)
        {
            _humanDnaRepository = humanDnaRepository;
        }
        public StatisticsDto getStats()
        {
            return new StatisticsDto
            {
                count_human_dna = _humanDnaRepository.getTotalRegisters(),
                count_mutant_dna = _humanDnaRepository.getMutantRegisters(),
            };
        }
    }
}
