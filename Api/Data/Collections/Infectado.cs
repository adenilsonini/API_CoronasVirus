using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class Infectado
    {
        public Infectado(string cpf, string nome, DateTime dataNascimento, string sexo, string endereco, double latitude, double longitude, List<Telefone> Telefones)
        {
            this.cpf = cpf;
            this.Nome_completo = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Endereco_completo = endereco;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
            this.Telefones = Telefones;
        }

        [BsonRequired]
        public string cpf { get; set; }

        [BsonRequired]
        public string Nome_completo { get; set; }

        [BsonRequired]
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        [BsonRequired]
        public string Endereco_completo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
        public List<Telefone> Telefones { get; set; }
    }
}