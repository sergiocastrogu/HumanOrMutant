using HumanOrMutant.Domain.Dtos;
using HumanOrMutant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanOrMutant.Persistence.Contracts
{
    public interface IHumanDnaRepository
    {
        int setHumanDna(HumanDnaEntity humanDnaEntity);

        int getTotalRegisters();
        int getMutantRegisters();

        HumanDnaEntity getHumanDnaEntity(string dnaSequence);

    }
}
