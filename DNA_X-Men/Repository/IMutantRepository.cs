using DNA_X_Men.Models;
using System.Collections.Generic;

namespace DNA_X_Men.Repository
{
    public interface IMutantRepository
    {
        void AddDNA(DNA record);

        DNA GetDNARecordBySequence(List<string> dnaSequence);

        DNAStats GetDNAStats();
    }
}