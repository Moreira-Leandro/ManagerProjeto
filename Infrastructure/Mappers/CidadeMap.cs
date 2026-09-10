using System.Data;
using Domain.Enum;
using Domain.Models;

namespace Infrastructure.Mappers;

public class CidadeMap
{

    public static Cidade Map(IDataRecord reader)
    {
        
        string uf = reader.GetString(reader.GetOrdinal("UF"));
        
        return new Cidade(
            id: reader.GetInt32(reader.GetOrdinal("ID")),
            nome: reader.GetString(reader.GetOrdinal("NOME")),
            uf: Enum.Parse<UFCidade>(uf)
        );
    }
    
}