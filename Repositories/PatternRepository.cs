using Dapper;
using NeopixelsBackend.Models;
using NeopixelsBackend.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeopixelsBackend.Repositories
{
    public class PatternRepository: IPatternRepository
    {
        private readonly string connectionString;
        public PatternRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<PatternList>> GetPatternsAsync()
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var patterns = await sql.QueryAsync<PatternList>("patterns_schema.get_pattern_list", commandType: System.Data.CommandType.StoredProcedure);
                return patterns;
            }
        }

        public async Task<PatternList> CreatePatternAsync(PatternList pattern)
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", pattern.PatternUUID);
                sqlParams.Add("_pattern_name", pattern.PatternName);
                await sql.ExecuteAsync("patterns_schema.add_pattern", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return pattern;
            }
        }

        public async Task<Guid> DeletePatternAsync(Guid patternUUID)
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", patternUUID);
                await sql.ExecuteAsync("patterns_schema.delete_pattern", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return patternUUID;
            }
        }

        public async Task<IEnumerable<PatternDetails>> GetPatternGenerationByGuidAsync(Guid patternUUID)
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", patternUUID);
                var patternGenerator = await sql.QueryAsync<PatternDetails>("patterns_schema.get_pattern_generation", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return patternGenerator;
            }
        }

        public async Task<PatternDetails> AddPatternDetailsAsync(PatternDetails patternDetails)
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", patternDetails.PatternUUID);
                sqlParams.Add("_pattern_name", patternDetails.PatternName);
                sqlParams.Add("_sequence_number", patternDetails.SequenceNumber);
                sqlParams.Add("_sequence_description", patternDetails.SequenceDescription);
                sqlParams.Add("_pattern_metadata", patternDetails.SequenceMetadata);
                await sql.ExecuteAsync("patterns_schema.add_pattern_sequence", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return patternDetails;
            }
        }

        public async Task<PatternDetails> UpdatePatternDetails(PatternDetails patternDetails)
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", patternDetails.PatternUUID);
                sqlParams.Add("_sequence_number", patternDetails.SequenceNumber);
                sqlParams.Add("_sequence_description", patternDetails.SequenceDescription);
                await sql.ExecuteAsync("patterns_schema.update_pattern_sequence", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return patternDetails;
            }
        }
    }
}
