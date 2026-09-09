using Application.Interfaces;
using Domain.Models;
using FirebirdSql.Data.FirebirdClient;
using Infrastructure.Mappers;

namespace Infrastructure.DAO;

public class CidadeDAO : ICidadeRepository
{

    private readonly string _connectionString;

    public CidadeDAO(string connect)
    {
        this._connectionString = connect;
    }
    
    public async Task CreateCidade(Cidade cidade)
    {
        string query = 
            @"INSERT INTO CIDADE (NOME, UF) 
              VALUES(@NOME, @UF);";

        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@NOME", cidade.NomeCidade);
        cmd.Parameters.AddWithValue("@UF", cidade.UfCidade);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteCidade(int idCidade)
    {
        string query =
            @"DELETE FROM CIDADE WHERE ID=@ID;";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@ID", idCidade);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateCidade(Cidade cidade)
    {
        string query =
            @"UPDATE CIDADE SET NOME=@NOME, UF=@UF WHERE ID=@ID_CIDADE;";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@ID_CIDADE", cidade.IdCidade);
        cmd.Parameters.AddWithValue("@NOME", cidade.NomeCidade);
        cmd.Parameters.AddWithValue("@UF", cidade.UfCidade);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Cidade?> FindById(int idCidade)
    {
        string query =
            @"SELECT ID, NOME, UF FROM CIDADE WHERE ID=@ID";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@ID", idCidade);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
            return null;
        
        return CidadeMap.Map(reader);
    }

    public async Task<List<Cidade>> FindAll()
    {
        List<Cidade> cidades = new List<Cidade>();
        
        string query =
            @"SELECT ID, NOME, UF FROM CIDADE ";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();
        
        using var cmd = new FbCommand(query, conexão);
        using var reader = await cmd.ExecuteReaderAsync();


        while (reader.Read())
        {
            cidades.Add(CidadeMap.Map(reader));
        }

        return cidades;
    }
}