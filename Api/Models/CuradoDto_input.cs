using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class CuradoDtoinput
    {

        [BsonRequired]
        public string cpf { get; set; }

        [BsonRequired]
        public string Nome_completo { get; set; }

        [BsonRequired]
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        [BsonRequired]
        public string Endereco_completo { get; set; }
        public List<Telefone> Telefones { get; set; }
    }
}