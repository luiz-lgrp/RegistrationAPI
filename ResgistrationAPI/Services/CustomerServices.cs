﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResgistrationAPI.Models;


namespace ResgistrationAPI.Services
{
    //Serviço para ligar minha tabela customer com o MongoDB
    public class CustomerServices
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        //Configurações do meu BD
        public CustomerServices(IOptions<CustomerDatabaseSettings> customerServices)
        {
            var mongoClient = new MongoClient(customerServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(customerServices.Value.DatabaseName);

            //A ligação da tabela acontece aqui
            _customerCollection = mongoDatabase.GetCollection<Customer>
                (customerServices.Value.CustomerCollectionName);
        }

        //Metodos de serviço para se usar na API

        //Get Todos
        public async Task<List<Customer>> GetAsync() =>
            await _customerCollection.Find(x => true).ToListAsync();

        //Get Um
        public async Task<Customer> GetAsync(string cpf) =>
            await _customerCollection.Find(x => x.Cpf == cpf).FirstOrDefaultAsync();

        //Post
        public async Task CreateAsync(Customer customer) =>
            await _customerCollection.InsertOneAsync(customer);

        //Put
        public async Task UpdateAsync(string id, Customer customer) =>
            await _customerCollection.ReplaceOneAsync(x => x.Id == id, customer);

        //Delete
        public async Task RemoveAsync(string id) =>
            await _customerCollection.DeleteOneAsync(x => x.Id == id);

    }
}