using System.Data;
using Domain.Enum;
using Domain.Models;

namespace Infrastructure.Mappers;

public class AlunoMap
{
    
    public static Aluno Map(IDataRecord reader)
    {
        var posCpf = reader.GetOrdinal("CPF");

        return new Aluno(
            idAluno: reader.GetInt32(reader.GetOrdinal("ID")),
            nomeAluno: reader.GetString(reader.GetOrdinal("NOME")),
            dataNascimentoAluno: DateOnly.FromDateTime(
                reader.GetDateTime(reader.GetOrdinal("DATA_NASCIMENTO"))
            ),
            cpfAluno: reader.IsDBNull(posCpf)
                ? null
                : reader.GetString(posCpf),
            cidadeIdAluno: reader.GetInt32(reader.GetOrdinal("CIDADE_ID")),
            sexoAluno: (Sexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
        );
    }
    
}