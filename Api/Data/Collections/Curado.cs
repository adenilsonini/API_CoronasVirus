using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class Curado
    {

        public Curado(string cpf, string nome, DateTime dataNascimento, string sexo, string endereco, List<Telefone> Telefones)
        {
            this.cpf = cpf;
            this.Nome_completo = nome;
          //  this.Id = ObjectId.GenerateNewId();
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Endereco_completo = endereco;
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
        public List<Telefone> Telefones { get; set; }
    }
}