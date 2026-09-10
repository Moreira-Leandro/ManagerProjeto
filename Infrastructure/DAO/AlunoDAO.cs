using Domain.Enum;
using Domain.Interfaces;
using Domain.Models;
using FirebirdSql.Data.FirebirdClient;
using Infrastructure.Mappers;

namespace Infrastructure.DAO;

public class AlunoDAO : IAlunoRepositorio
{

    private readonly string _connectionString;

    public AlunoDAO(string connect)
    {
        this._connectionString = connect;
    }
    
    public async Task Crie(Aluno aluno)
    {
        string query = 
            @"INSERT INTO ALUNO (NOME, DATA_NASCIMENTO, CPF, CIDADE_ID, SEXO) 
            VALUES(@NOME, @DATA_NASCIMENTO, @CPF, @CIDADE_ID, @SEXO)";

        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@NOME", aluno.Nome);
        comando.Parameters.AddWithValue("@DATA_NASCIMENTO", aluno.DataNascimento);
        comando.Parameters.AddWithValue("@CPF", (object?)aluno.Cpf ?? DBNull.Value);
        comando.Parameters.AddWithValue("@CIDADE_ID", aluno.CidadeId);
        comando.Parameters.AddWithValue("@SEXO", aluno.Sexo);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task Delete(int matriculaAluno)
    {
        string query =
            @"DELETE FROM ALUNO WHERE MATRICULA=@MATRICULA;";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@MATRICULA", matriculaAluno);

        await comando.ExecuteNonQueryAsync();

    }

    public async Task Atualize(Aluno aluno)
    {

        string query =
            @"UPDATE ALUNO SET NOME=@NOME, DATA_NASCIMENTO=@DATA_NASCIMENTO, CPF=@CPF, CIDADE_ID=@CIDADE_ID, SEXO=@SEXO 
             WHERE MATRICULA=@MATRICULA;";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);
        comando.Parameters.AddWithValue("@NOME", aluno.Nome);
        comando.Parameters.AddWithValue("@DATA_NASCIMENTO", aluno.DataNascimento);
        comando.Parameters.AddWithValue("@CPF", (object?)aluno.Cpf ?? DBNull.Value);
        comando.Parameters.AddWithValue("@CIDADE_ID", aluno.CidadeId);
        comando.Parameters.AddWithValue("@SEXO", aluno.Sexo);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task<Aluno?> BusquePorId(int matriculaAluno)
    {
        string query =
            @"SELECT MATRICULA, NOME, DATA_NASCIMENTO, CPF, CIDADE_ID, SEXO
                FROM ALUNO WHERE MATRICULA=@MATRICULA";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();

        using FbCommand comando = new FbCommand(query, conexao);
        comando.Parameters.AddWithValue("@MATRICULA", matriculaAluno);

        using FbDataReader reader = await comando.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;
        
        return AlunoMap.Map(reader);

    }

    public async Task<List<Aluno>> BusqueTodos()
    {

        List<Aluno> alunos = new List<Aluno>();
        
        string query =
            @"SELECT MATRICULA, NOME, DATA_NASCIMENTO, CPF, CIDADE_ID, SEXO FROM ALUNO";
        
        using FbConnection conexao = new FbConnection(_connectionString);
        await conexao.OpenAsync();
        
        using FbCommand comando = new FbCommand(query, conexao);
        using FbDataReader reader = await comando.ExecuteReaderAsync();


        while (await reader.ReadAsync())
        {
             alunos.Add(AlunoMap.Map(reader));
        }

        return alunos;
    }
}