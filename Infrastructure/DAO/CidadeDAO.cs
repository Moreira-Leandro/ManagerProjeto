using Domain.Interfaces;
using Domain.Models;
using FirebirdSql.Data.FirebirdClient;
using Infrastructure.Mappers;

namespace Infrastructure.DAO;

public class CidadeDAO : ICidadeRepositorio
{

    private readonly string _connectionString;

    public CidadeDAO(string connect)
    {
        this._connectionString = connect;
    }
    
    public async Task Crie(Cidade cidade)
    {
        string query = 
            @"INSERT INTO CIDADE (NOME, UF) 
              VALUES(@NOME, @UF);";

        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@NOME", cidade.Nome);
        comando.Parameters.AddWithValue("@UF", cidade.Uf);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task Delete(int idCidade)
    {
        string query =
            @"DELETE FROM CIDADE WHERE ID=@ID;";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@ID", idCidade);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task Atualize(Cidade cidade)
    {
        string query =
            @"UPDATE CIDADE SET NOME=@NOME, UF=@UF WHERE ID=@ID_CIDADE;";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@ID_CIDADE", cidade.Id);
        comando.Parameters.AddWithValue("@NOME", cidade.Nome);
        comando.Parameters.AddWithValue("@UF", cidade.Uf);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task<Cidade?> BusquePorId(int idCidade)
    {
        string query =
            @"SELECT ID, NOME, UF FROM CIDADE WHERE ID=@ID";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@ID", idCidade);

        using FbDataReader reader = await comando.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;
        
        return CidadeMap.Map(reader);
    }

    public async Task<List<Cidade>> BusqueTodos()
    {
        List<Cidade> cidades = new List<Cidade>();
        
        string query =
            @"SELECT ID, NOME, UF FROM CIDADE ";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();
        
        using FbCommand comando = new FbCommand(query, conexao);
        using FbDataReader reader = await comando.ExecuteReaderAsync();


        while (await reader.ReadAsync())
        {
            cidades.Add(CidadeMap.Map(reader));
        }

        return cidades;
    }
}