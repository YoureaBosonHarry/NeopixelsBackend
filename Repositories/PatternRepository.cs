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

        public async Task<IEnumerable<PatternDetails>> GetPatternGenerationByGuidAsync(Guid patternUUID)
        {
            using (var sql = new NpgsqlConnection(connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", patternUUID);
                var patternGenerator = await sql.QueryAsync<PatternDetails>("patterns_schema.get_pattern_generation", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return patternGenerator;
            }
        }
    }
}
