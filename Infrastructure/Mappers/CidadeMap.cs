using System.Data;
using Domain.Enum;
using Domain.Models;

namespace Infrastructure.Mappers;

public class CidadeMap
{

    public static Cidade Map(IDataRecord reader)
    {
        return new Cidade(
            nomeCidade: reader.GetString(reader.GetOrdinal("NOME")),
            ufCidade: (UFCidade)reader.GetInt32(reader.GetOrdinal("UF"))
        )
        {
            IdCidade = reader.GetInt32(reader.GetOrdinal("ID"))
        };
    }
    
}