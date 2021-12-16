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

        private async Task InsertChristmasTree()
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var backgroundList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 108, 109, 110, 111, 112, 113, 114, 115, 116, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 140, 141, 142, 143, 144, 145, 146, 156, 157, 158, 159, 160, 161, 162, 163, 164, 172, 173, 174, 175, 176, 177, 178, 188, 189, 190, 191, 192, 193, 194, 206, 207, 208, 209, 210, 220, 221, 222, 223, 224, 225, 226, 227, 228, 236, 237, 238, 239 };
                var ground = new List<int>() { 240, 241, 242, 243, 244, 245, 249, 250, 251, 252, 253, 254, 255 };
                var tree = new List<int>() { 72, 87, 88, 102, 104, 105, 106, 118, 119, 120, 134, 135, 136, 137, 138, 148, 150, 152, 154, 166, 167, 168, 169, 170, 180, 181, 182, 183, 184, 185, 196, 197, 198, 199, 200, 201, 202, 203, 204, 213, 215, 216, 218, 232 };
                var ornaments = new List<int>() { 86, 103, 149, 151, 153, 186, 212, 214, 217 };
                var star = new List<int>() { 40 };
                var snow = new List<int>() { 17, 20, 30, 60, 65, 76, 81, 92, 98, 109, 114, 131, 143, 145, 158, 177, 189, 191, 193, 208, 226, 238 };
                var empty = new List<int>() { 55, 71, 73, 85, 89, 101, 107, 117, 121, 133, 139, 147, 155, 165, 171, 179, 187, 195, 205, 211, 219, 229, 230, 231, 233, 234, 235, 246, 247, 248 };
                var dictionary = new SortedDictionary<int, string>();
                foreach (var item in backgroundList)
                {
                    dictionary.Add(item, "#0c1e8c");
                }
                foreach (var item in ground)
                {
                    dictionary.Add(item, "#64645a");
                }
                foreach(var item in tree)
                {
                    dictionary.Add(item, "0c8c05");
                }
                foreach(var item in star)
                {
                    dictionary.Add(item, "646400");
                }
                foreach(var item in snow)
                {
                    dictionary[item] = "64645a";
                }
                foreach(var item in empty)
                {
                    dictionary.Add(item, "#000000");
                }
                dictionary[86] = "#646400";
                dictionary[103] = "#005a64";
                dictionary[149] = "#646400";
                dictionary[151] = "#640000";
                dictionary[153] = "#005a64";
                dictionary[186] = "#640064";
                dictionary[212] = "#005a64";
                dictionary[214] = "#005a64";
                dictionary[217] = "#96350c";
                dictionary[181] = "#557dff";
                dictionary[169] = "#96350c";
                dictionary[200] = "#ff3377";
                dictionary[118] = "#557dff";
                var serialized = System.Text.Json.JsonSerializer.Serialize(dictionary);

                var sqlParams = new DynamicParameters();
                sqlParams.Add("_pattern_uuid", Guid.Parse("ed7cea86-1cd1-452f-9f55-d5daed45f331"));
                sqlParams.Add("_pattern_name", "christmas tree");
                sqlParams.Add("_sequence_number", 15);
                sqlParams.Add("_sequence_description", serialized);
                sqlParams.Add("_pattern_metadata", null);
                sql.Execute("patterns_schema.add_pattern_sequence", sqlParams, commandType: System.Data.CommandType.StoredProcedure);

            }
        }
    }
}
