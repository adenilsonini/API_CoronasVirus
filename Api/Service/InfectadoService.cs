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
    public class InfectadoService
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectados;

        public InfectadoService(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectados = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        public async Task<IEnumerable<Infectado>> GetAllInfectados()
        {
            var documents = await _infectados.Find(_ => true).ToListAsync();
            return documents;
        }

        public async Task<Infectado> GetByCPF(string CPF)
        {
            try
            {
               return await _infectados
                                .Find(dto => dto.cpf == CPF)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        
        public async Task<bool> Atualizar(string CPF, Infectado item)
        {
            try
            {
                var filter = Builders<Infectado>.Filter.Eq(dto => dto.cpf, CPF);
                ReplaceOneResult actionResult = await _infectados.ReplaceOneAsync(filter, item);
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                return false;
            }
        }

        public async Task<bool> UpdateEnd(string CPF, string endereco)
        {
            var filter = Builders<Infectado>.Filter.Eq(dto => dto.cpf, CPF);
            var update = Builders<Infectado>.Update
                            .Set(dto => dto.Endereco_completo, endereco);

            try
            {
                UpdateResult actionResult = await _infectados.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<string> Inserir(Infectado dto)
        {
            try
            {
                await _infectados.InsertOneAsync(dto);

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
            var ret =  await _infectados.DeleteOneAsync(dto => dto.cpf == CPF);
            return ret != null ? (int)ret.DeletedCount : 0;
        }
            
    }
}
