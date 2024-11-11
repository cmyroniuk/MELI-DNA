using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DNA_X_Men.Models;
using Newtonsoft.Json;

namespace DNA_X_Men.Repository
{
    public class MutantRepository : IMutantRepository
    {
        private readonly string _connectionString;

        public MutantRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DNAConnection"].ConnectionString;
        }

        public void AddDNA(DNA record)
        {
            string dnaSequenceJson = JsonConvert.SerializeObject(record.DnaSequence);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO DNA (DnaSequence, IsMutant) VALUES (@DnaSequence, @IsMutant);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DnaSequence", dnaSequenceJson);
                    cmd.Parameters.AddWithValue("@IsMutant", record.IsMutant);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DNA GetDNARecordBySequence(List<string> dnaSequence)
        {
            string dnaSequenceJson = JsonConvert.SerializeObject(dnaSequence);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, DnaSequence, IsMutant FROM DNA WHERE DnaSequence = @DnaSequence";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DnaSequence", dnaSequenceJson);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())

                            return new DNA
                            {
                                Id = reader.GetInt32(0),
                                DnaSequence = JsonConvert.DeserializeObject<List<string>>(reader.GetString(1)),
                                IsMutant = reader.GetBoolean(2)
                            };
                    }
                }
            }
            return null;
        }

        public DNAStats GetDNAStats()
        {
            int countMutantDna = 0;
            int countHumanDna = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT IsMutant, COUNT(*) AS Count FROM DNA GROUP BY IsMutant";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool isMutant = reader.GetBoolean(0);
                            int count = reader.GetInt32(1);

                            if (isMutant)
                            {
                                countMutantDna = count;
                            }
                            else
                            {
                                countHumanDna = count;
                            }
                        }
                    }
                }
            }

            double ratio = countHumanDna > 0 ? (double)countMutantDna / countHumanDna : 0;

            return new DNAStats
            {
                CountMutantDna = countMutantDna,
                CountHumanDna = countHumanDna,
                Ratio = ratio
            };
        }
    }
}
