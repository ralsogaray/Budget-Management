using Budget_Management.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Budget_Management.Servicios
{


    /*Interface*/
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
    }


    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {

        private readonly string connectionString;

        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task Crear(TipoCuenta tipoCuenta)
        {
            /* control . para importar sqlClient*/
            using SqlConnection connection = new SqlConnection(connectionString);

            /*control . para dapper*/

            /*  SELECT SCOPE_IDENTITY() me trae el Id del registro recien creado */
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Type_Accounts(Name, UserId, [Order])
                                                Values (@Nombre, @UsuarioId, 0);
                                                SELECT SCOPE_IDENTITY()", tipoCuenta);

            tipoCuenta.ID = id;
        }
     
        
        /* retorna un booleano*/
        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using SqlConnection connection = new SqlConnection(connectionString);


            /* queryFirstOrDefault retorna el primer registro o uno por defecto*/
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM Type_Accounts 
                                                                        WHERE name = @Nombre AND UserId = @UsuarioId;", 
                                                                        new {nombre, usuarioId});

            return existe == 1;
        }


        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            // QueryAsync me permite hacer un SELECT y retorna un resultado mapeado a un tipo de dato específico --> tipocuenta
            return await connection.QueryAsync<TipoCuenta>(@"SELECT [id], name AS Nombre, [Order] AS Orden FROM Type_Accounts WHERE UserId = @UsuarioId;", new {usuarioId});
        }
    }
}
