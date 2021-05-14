using Api.Data.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class InfectadoDto
    {

        [BsonRequired]
        public string Nome_completo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }

        [BsonRequired]
        public string Endereco_completo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Telefone> Telefones { get; set; }
    }
}