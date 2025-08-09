using Dapper;
using LosNeteros.Models;
using System.Data;

namespace LosNeteros.Datos
{
    public class ClienteBCPRepository
    {
        private readonly DapperContext _context;

        public ClienteBCPRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteBCP>> ObtenerTodos()
        {
            var query = "sp_ClienteBCP_ObtenerTodos";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<ClienteBCP>(query, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ClienteBCP> ObtenerPorId(int id)
        {
            var query = "sp_ClienteBCP_ObtenerPorId";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ClienteBCP>(query,
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Crear(ClienteBCP cliente)
        {
            var query = "sp_ClienteBCP_Insertar";
            var parameters = new DynamicParameters();
            parameters.Add("Nombres", cliente.Nombres);
            parameters.Add("Apellidos", cliente.Apellidos);
            parameters.Add("DNI", cliente.DNI);
            parameters.Add("Direccion", cliente.Direccion);
            parameters.Add("NumeroCuenta", cliente.NumeroCuenta);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Actualizar(ClienteBCP cliente)
        {
            var query = "sp_ClienteBCP_Actualizar";
            var parameters = new DynamicParameters();
            parameters.Add("Id", cliente.Id);
            parameters.Add("Nombres", cliente.Nombres);
            parameters.Add("Apellidos", cliente.Apellidos);
            parameters.Add("DNI", cliente.DNI);
            parameters.Add("Direccion", cliente.Direccion);
            parameters.Add("NumeroCuenta", cliente.NumeroCuenta);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Eliminar(int id)
        {
            var query = "sp_ClienteBCP_Eliminar";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}