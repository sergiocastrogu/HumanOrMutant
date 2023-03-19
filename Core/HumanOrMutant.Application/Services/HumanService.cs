using HumanOrMutant.Application.Interfaces;
using HumanOrMutant.Domain.Entities;
using HumanOrMutant.Persistence.Contracts;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HumanOrMutant.Application.Services
{
    public class HumanService : IHumanService
    {

        private readonly IHumanDnaRepository _humanDnaRepository;

        public HumanService(IHumanDnaRepository humanDnaRepository)
        {
            _humanDnaRepository = humanDnaRepository;
        }

        public bool isMutant(DnaSecuenceEntity dnaSecuence)
        {

            string[]? humanDna = dnaSecuence.dna;

            bool result = true;

            /*Valida si dna inicialmente es null*/
            if (humanDna == null) return false;

            /*Valida si contiene otros caracteres para rechazar la petición*/
            if (!validateOtherCaracters(humanDna)) return false;


            /*Valida si ya existe el registro*/
            HumanDnaEntity dnaEntity = _humanDnaRepository.getHumanDnaEntity(JsonConvert.SerializeObject(humanDna));

            /*Si existe el registro devuelve el resultado previamente calculado*/
            if (!string.IsNullOrWhiteSpace(dnaEntity.DnaSecuence))
            {
                return dnaEntity.IsMutant;
            }

            /*Realiza validaciones del arreglo de secuencias*/
            int countCompleteSecuences;
            countCompleteSecuences = horizontalValidationDna(humanDna);
            countCompleteSecuences += verticalValidationDna(humanDna);
            countCompleteSecuences += diagonalValidationDna(humanDna);

            /*Si no completa mas de dos secuencuas iguales guarda el valor y devuelve false*/
            if (countCompleteSecuences < 2)
            {
                result = false;
            }
            
            /*Si completa mas de dos secuencias iguales guarda el valor y devuelve true*/
            saveHumanDna(humanDna, result);
            return result;
        }

        public int saveHumanDna(string[] ndaSecuence, bool isMutant)
        {
            return _humanDnaRepository.setHumanDna(
                new HumanDnaEntity 
                { 
                    DnaSecuence = JsonConvert.SerializeObject(ndaSecuence), 
                    IsMutant = isMutant 
                });
        }

        public int diagonalValidationDna(string[] ndaSecuence)
        {
            int diagonalValidation = 0;

            /*Recorre las secuencias*/
            for (int row = 0; row < ndaSecuence.Length - 3; row++)
            {
                /*Recorre las columnas*/
                for (int column = 0; column < ndaSecuence[0].Length - 3; column++)
                {
                    /*Validar si hay cuatro letras iguales en la diagonal sumando de 
                     * a una posicion en horizontal y vertical por cada condicion*/
                    if (ndaSecuence[row][column] == ndaSecuence[row + 1][column + 1] && 
                        ndaSecuence[row][column] == ndaSecuence[row + 2][column + 2] && 
                        ndaSecuence[row][column] == ndaSecuence[row + 3][column + 3])
                    {
                        diagonalValidation++;
                    }
                }
            }
            return diagonalValidation;
        }

        public int horizontalValidationDna(string[] ndaSecuence)
        {
            int horizontalValidation = 0;
            /*Recorre el arreglo de secuencias*/
            for(int row = 0; row < ndaSecuence.Length ; row++ )
            {
                /*Recorre las columnas del arreglo*/
                for (int column = 0; column < ndaSecuence[row].Length - 3; column++)
                {
                    /*Si el caracter es igual en las siguientes 3 columnas de la misma fila*/
                    if (ndaSecuence[row][column] == ndaSecuence[row][column + 1 ] &&
                        ndaSecuence[row][column] == ndaSecuence[row][column + 2] &&
                        ndaSecuence[row][column] == ndaSecuence[row][column + 3])
                    {
                        horizontalValidation++;
                        break;
                    }
                }
            }
            return horizontalValidation;
        }

        public bool validateOtherCaracters(string[] ndaSecuence)
        {
            bool isPermited = true;
            /*Expresion regular para validar letras permitidas*/
            Regex validaton = new Regex("[ATCG]");

            /*Recorre arreglo de secuencias*/
            foreach (var human in ndaSecuence)
            {
                /*Reccorre letras de la secuencia*/
                foreach (char caracter in human)
                {
                    /*Valida si no hace match con la expresion regular retorna que no es permitido el arreglo*/
                    if (!validaton.Match(caracter.ToString()).Success)
                    {
                        return false;
                    };

                }
            }
            return isPermited;
        }

        public int verticalValidationDna(string[] ndaSecuence)
        {
            int verticalValidation = 0;
            /*Recorrido por columnas*/
            for (int column = 0; column < ndaSecuence[0].Length; column++)
            {
                /*Recorre las filas del arreglo*/
                for (int row = 0; row < ndaSecuence[0].Length - 3; row++)
                {
                    /*Si el caracter es igual en las siguientes 3 filas de la misma columna*/
                    if (ndaSecuence[row][column] == ndaSecuence[row + 1][column] &&
                        ndaSecuence[row][column] == ndaSecuence[row + 2][column] &&
                        ndaSecuence[row][column] == ndaSecuence[row + 3][column])
                    {
                        verticalValidation++;
                        break;
                    }
                }
            }
            return verticalValidation;
        }
    }
}
