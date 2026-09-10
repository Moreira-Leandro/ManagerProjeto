using System.Data;
using Domain.Enum;
using Domain.Models;

namespace Infrastructure.Mappers;

public class AlunoMap
{
    
    public static Aluno Map(IDataRecord reader)
    {
        int posCpf = reader.GetOrdinal("CPF");

        return new Aluno(
            matricula: reader.GetInt32(reader.GetOrdinal("MATRICULA")),
            nome: reader.GetString(reader.GetOrdinal("NOME")),
            dataNascimento: DateOnly.FromDateTime(
                reader.GetDateTime(reader.GetOrdinal("DATA_NASCIMENTO"))
            ),
            cpf: reader.IsDBNull(posCpf)
                ? null
                : reader.GetString(posCpf),
            cidadeId: reader.GetInt32(reader.GetOrdinal("CIDADE_ID")),
            sexo: (Sexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
        );
    }
    
}