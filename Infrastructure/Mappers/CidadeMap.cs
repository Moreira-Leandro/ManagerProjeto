using System.Data;
using Domain.Enum;
using Domain.Models;

namespace Infrastructure.Mappers;

public class CidadeMap
{

    public static Cidade Map(IDataRecord reader)
    {
        
        var uf = reader.GetString(reader.GetOrdinal("UF"));
        
        return new Cidade(
            idCidade: reader.GetInt32(reader.GetOrdinal("ID")),
            nomeCidade: reader.GetString(reader.GetOrdinal("NOME")),
            ufCidade: Enum.Parse<UFCidade>(uf)
        );
    }
    
}