using Softdesign.API.Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;

namespace Softdesign.Api.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private string connectionString = string.Empty;
        IConfiguration configure;
        public ApplicationRepository(IConfiguration _config) {
            configure = _config;
        }

        public async Task<List<Application>> Get()
        {
            try
            {
                connectionString = configure.GetSection("ConnectionStrings").GetSection("SoftdesignDB").Value;

                using (var dbConnection = new SqlConnection(connectionString))
                {
                    var query = "SELECT Application as ApplicationId, Url, PathLocal, DebuggingMode " +
                                "FROM Application " +
                                "ORDER BY Application ";

                    var applicationList = await dbConnection.QueryAsync<Application>(query);

                    return applicationList.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Application> GetById(int id)
        {
            try
            {
                connectionString = configure.GetSection("ConnectionStrings").GetSection("SoftdesignDB").Value;

                using (var dbConnection = new SqlConnection(connectionString))
                {
                    var query = "SELECT Application as ApplicationId, Url, PathLocal, DebuggingMode " +
                                "FROM Application " +
                                $"WHERE Application = {id} ";

                    var application = await dbConnection.QueryAsync<Application>(query);

                    return application.Count() > 0 ? application.FirstOrDefault() : null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Post(Application application)
        {
            try
            {
                connectionString = configure.GetSection("ConnectionStrings").GetSection("SoftdesignDB").Value;
                var debbugMode = application.DebuggingMode == true ? 1 : 0;

                using (var dbConnection = new SqlConnection(connectionString))
                {
                    var query = "INSERT INTO Application " +
                                "(Application, Url, PathLocal, DebuggingMode) VALUES " +
                                $"({application.ApplicationId}, '{application.Url}', '{application.PathLocal}', {debbugMode}) ";

                    var posted = await dbConnection.ExecuteAsync(query);

                    return posted == 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Path(Application application)
        {
            try
            {
                connectionString = configure.GetSection("ConnectionStrings").GetSection("SoftdesignDB").Value;
                var debbugMode = application.DebuggingMode == true ? 1 : 0;

                using (var dbConnection = new SqlConnection(connectionString))
                {
                    var query = "UPDATE Application " +
                                $"SET Url = '{application.Url}', PathLocal = '{application.PathLocal}', DebuggingMode = {debbugMode} " +
                                $"WHERE Application = {application.ApplicationId} ";

                    var patched = await dbConnection.ExecuteAsync(query);

                    return patched == 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                connectionString = configure.GetSection("ConnectionStrings").GetSection("SoftdesignDB").Value;

                using (var dbConnection = new SqlConnection(connectionString))
                {
                    var query = "DELETE from Application " +
                                $"WHERE Application = {id} ";

                    var deleted = await dbConnection.ExecuteAsync(query);

                    return deleted == 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
