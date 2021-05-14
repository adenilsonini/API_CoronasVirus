using Api.Data.Collections;
using Api.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service
{
    public class CuradoService
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Curado> _curado;

        public CuradoService(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _curado = _mongoDB.DB.GetCollection<Curado>(typeof(Curado).Name.ToLower());
        }

        public async Task<IEnumerable<Curado>> GetAllInfectados()
        {
            var documents = await _curado.Find(_ => true).ToListAsync();
            return documents;
        }

        public async Task<Curado> GetByCPF(string CPF)
        {
            try
            {
               return await _curado
                                .Find(dto => dto.cpf == CPF)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        
        public async Task<bool> Atualizar(string CPF, Curado item)
        {
           
                var filter = Builders<Curado>.Filter.Eq(dto => dto.cpf, CPF);
                ReplaceOneResult actionResult = await _curado.ReplaceOneAsync(filter, item);
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
           
        }

        public async Task<bool> UpdateEnd(string CPF, string endereco)
        {
            var filter = Builders<Curado>.Filter.Eq(dto => dto.cpf, CPF);
            var update = Builders<Curado>.Update
                            .Set(dto => dto.Endereco_completo, endereco);

            try
            {
                UpdateResult actionResult = await _curado.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<string> Inserir(Curado dto)
        {
            try
            {
                await _curado.InsertOneAsync(dto);

                return "ok";
            }
           
           catch (Exception ex)
           {
                // log or manage the exception
               return ex.Message;
           }
        }

        public async Task<int> Remove(string CPF)
        {
            var ret =  await _curado.DeleteOneAsync(dto => dto.cpf == CPF);
            return ret != null ? (int)ret.DeletedCount : 0;
        }
            
    }
}
