using DNA_X_Men.Models;

namespace DNA_X_Men
{
    public interface IMutantService
    {
        bool ValidarDNAValido(string[] dna);

        bool ValidarDNA(string[] dna);

        DNAStats GetDNAStats();
    }
}