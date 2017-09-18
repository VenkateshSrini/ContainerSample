using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ContainerDASample.Database.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ContainerDASample.Database.Configuration;
using Npgsql;
using System.Data.Common;
using System.Data;

namespace ContainerDASample.Database.Repository
{
    public class EmployeeRepository : IEmpRepository
    {
        private ILogger<EmployeeRepository> _logger;
        private string _connectionString;
        private DbConnection createNewConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
        public EmployeeRepository(ILoggerFactory loggerfactory, IOptions<DatabaseOptions> dbOptions)
        {
            _logger = loggerfactory.CreateLogger<EmployeeRepository>();
            var currentSelected = dbOptions.Value.SelectedOptions;
            _connectionString = dbOptions.Value.CredentialSet.FirstOrDefault(lov => lov.Key.CompareTo(currentSelected) == 0).Credential;

        }
        public async Task<int> AddEmployee(Employees emp)
        {
            using (var connection = createNewConnection())
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO container.\"Employees\"(\"Name\") VALUES(@Name); ";
                    DbParameter param = command.CreateParameter();
                    param.ParameterName = "@Name";
                    param.DbType = DbType.String;
                    param.Value = emp.EmployeeName;
                    command.Parameters.Add(param);
                    await connection.OpenAsync();
                    var empID = await command.ExecuteNonQueryAsync();
                    return empID;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"Error while adding employee {ex.StackTrace}");
                    return -1;
                }

            }
        }

        public async Task<bool> DeleteEmployee(int employeeID)
        {
            try
            {
                using (var connection = createNewConnection())
                {
                    var commandString = "DELETE FROM container.\"Employees\" WHERE \"Id\"=@EmpID";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = commandString;
                    DbParameter param = command.CreateParameter();
                    param.ParameterName = "@EmpID";
                    param.DbType = DbType.Int32;
                    param.Value = employeeID;
                    command.Parameters.Add(param);
                    await connection.OpenAsync();
                    var resultantRows = await command.ExecuteNonQueryAsync();
                    return (resultantRows > 0) ? true : false;

                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Error while deleting employee {ex.StackTrace}");
                return false;
            }
        }

        public async Task<List<Employees>> GetAllEmployees()
        {
            using (var connection = createNewConnection())
            {
                try
                {
                    var commandString = "SELECT \"Id\", \"Name\" FROM container.\"Employees\"; ";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = commandString;
                    await connection.OpenAsync();
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            List<Employees> employees = new List<Employees>();
                            while (await reader.ReadAsync())
                            {
                                employees.Add(new Employees
                                {
                                    EmployeeID = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                                    EmployeeName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
                                });
                            }
                            return employees;
                        }
                        else
                            return null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"Error while Getting employee {ex.StackTrace}");
                    return null;
                }
            }
        }
        public async Task<bool> UpdateEmployee(Employees emp)
        {
            try
            {
                using (var connection = createNewConnection())
                {
                    var commandString = "UPDATE container.\"Employees\" SET  \"Name\" =@EmpName WHERE \"Id\" = @EmpId";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = commandString;
                    DbParameter param = command.CreateParameter();
                    param.ParameterName = "@EmpID";
                    param.DbType = DbType.Int32;
                    param.Value = emp.EmployeeID;
                    DbParameter param1 = command.CreateParameter();
                    param1.ParameterName = "@EmpName";
                    param1.DbType = DbType.String;
                    param1.Value = emp.EmployeeName;

                    command.Parameters.Add(param);
                    command.Parameters.Add(param1);

                    await connection.OpenAsync();
                    var resultantRows = await command.ExecuteNonQueryAsync();
                    return (resultantRows > 0) ? true : false;

                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Error while updating employee {ex.StackTrace}");
                return false;
            }
        }
    }
    
}

        
    
