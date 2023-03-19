using HumanOrMutant.Domain.Entities;

namespace HumanOrMutant.Application.Interfaces
{
    public interface IHumanService
    {
        bool isMutant(DnaSecuenceEntity dnaSecuence);
        bool validateOtherCaracters(string[] ndaSecuence);
        int horizontalValidationDna(string[] ndaSecuence);
        int verticalValidationDna(string[] ndaSecuence);
        int diagonalValidationDna(string[] ndaSecuence);
        int saveHumanDna(string[] ndaSecuence, bool isMutant);
    }
}
