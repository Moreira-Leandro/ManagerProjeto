using Application.Interfaces;
using Domain.Enum;
using Domain.Models;
using FirebirdSql.Data.FirebirdClient;
using Infrastructure.Mappers;

namespace Infrastructure.DAO;

public class AlunoDAO : IAlunoRepository
{

    private readonly string _connectionString;

    public AlunoDAO(string connect)
    {
        this._connectionString = connect;
    }
    
    public async Task CreateAluno(Aluno aluno)
    {
        string query = 
            @"INSERT INTO ALUNO (NOME, DATA_NASCIMENTO, CPF, CIDADE_ID, SEXO) 
            VALUES(@NOME, @DATA_NASCIMENTO, @CPF, @CIDADE_ID, @SEXO)";

        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@NOME", aluno.NomeAluno);
        cmd.Parameters.AddWithValue("@DATA_NASCIMENTO", aluno.DataNascimentoAluno);
        cmd.Parameters.AddWithValue("@CPF", aluno.CpfAluno);
        cmd.Parameters.AddWithValue("@CIDADE_ID", aluno.CidadeIdAluno);
        cmd.Parameters.AddWithValue("@SEXO", aluno.SexoAluno);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAluno(int idAluno)
    {
        string query =
            @"DELETE FROM ALUNO WHERE ID=@ID;";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@ID", idAluno);

        await cmd.ExecuteNonQueryAsync();

    }

    public async Task UpdateAluno(Aluno aluno)
    {

        string query =
            @"UPDATE ALUNO SET NOME=@NOME, DATA_NASCIMENTO=@DATA_NASCIMENTO, CPF=@CPF, CIDADE_ID=@CIDADE_ID, SEXO=@SEXO 
             WHERE ID=@ID_ALUNO;";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@ID_ALUNO", aluno.IdAluno);
        cmd.Parameters.AddWithValue("@NOME", aluno.NomeAluno);
        cmd.Parameters.AddWithValue("@DATA_NASCIMENTO", aluno.DataNascimentoAluno);
        cmd.Parameters.AddWithValue("@CPF", aluno.CpfAluno);
        cmd.Parameters.AddWithValue("@CIDADE_ID", aluno.CidadeIdAluno);
        cmd.Parameters.AddWithValue("@SEXO", aluno.SexoAluno);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Aluno?> FindById(int idAluno)
    {
        string query =
            @"SELECT ID, NOME, DATA_NASCIMENTO, CPF, CIDADE_ID, SEXO
                FROM ALUNO WHERE ID=@ID";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();

        using var cmd = new FbCommand(query, conexão);
        cmd.Parameters.AddWithValue("@ID", idAluno);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
            return null;
        
        return AlunoMap.Map(reader);

    }

    public async Task<List<Aluno>> FindAll()
    {

        List<Aluno> alunos = new List<Aluno>();
        
        string query =
            @"SELECT ID, NOME, DATA_NASCIMENTO, CPF, CIDADE_ID, SEXO FROM ALUNO";
        
        using var conexão = new FbConnection(_connectionString);
        await conexão.OpenAsync();
        
        using var cmd = new FbCommand(query, conexão);
        using var reader = await cmd.ExecuteReaderAsync();


        while (reader.Read())
        {
             alunos.Add(AlunoMap.Map(reader));
        }

        return alunos;
    }
}