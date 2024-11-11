using DNA_X_Men.Models;
using DNA_X_Men.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DNA_X_Men
{
    public class MutantService : IMutantService
    {
        private readonly IMutantRepository mutantRepository;
        public MutantService(IMutantRepository mutantRepository)
        {
            this.mutantRepository = mutantRepository;
        }

        public DNAStats GetDNAStats()
        {
            return this.mutantRepository.GetDNAStats();
        }

        public bool ValidarDNA(string[] dna)
        {
            var dnaBase = ValidarDNAEnBase(dna.ToList());

            if(dnaBase != null)
            {
                return dnaBase.IsMutant;
            }

            int rowsCount = dna.Length;
            int colsCount = dna[0].Length;
            int sequenceCount = 0;


            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < colsCount; j++)
                {
                    // Verificar secuencias en horizontal
                    if (j + 3 < colsCount && dna[i][j] == dna[i][j + 1] && dna[i][j] == dna[i][j + 2] && dna[i][j] == dna[i][j + 3])
                    {
                        sequenceCount++;
                    }

                    // Verificar secuencias en vertical
                    if (i + 3 < rowsCount && dna[i][j] == dna[i + 1][j] && dna[i][j] == dna[i + 2][j] && dna[i][j] == dna[i + 3][j])
                    {
                        sequenceCount++;
                    }

                    // Verificar secuencias diagonales (de izquierda a derecha)
                    if (i + 3 < rowsCount && j + 3 < colsCount &&
                        dna[i][j] == dna[i + 1][j + 1] &&
                        dna[i][j] == dna[i + 2][j + 2] &&
                        dna[i][j] == dna[i + 3][j + 3])
                    {
                        sequenceCount++;
                    }

                    // Verificar secuencias diagonales (de derecha a izquierda)
                    if (i + 3 < rowsCount && j - 3 >= 0 &&
                        dna[i][j] == dna[i + 1][j - 1] &&
                        dna[i][j] == dna[i + 2][j - 2] &&
                        dna[i][j] == dna[i + 3][j - 3])
                    {
                        sequenceCount++;
                    }
                }
            }

                DNA dnaRecord = new DNA
                {
                    DnaSequence = dna.ToList(),
                    IsMutant = sequenceCount >= 2
                };

                this.mutantRepository.AddDNA(dnaRecord);

            return sequenceCount >= 2;
        }

        public bool ValidarDNAValido(string[] dna)
        {
            if (dna == null || dna.Length != 6)
            {
                return false;
            }

            foreach (var item in dna)
            {
                if (item.Length != 6)
                {
                    return false;
                }
            }

            return true;
        }

        private DNA ValidarDNAEnBase(List<string> dna)
        {
            return this.mutantRepository.GetDNARecordBySequence(dna);
        }
    }
}