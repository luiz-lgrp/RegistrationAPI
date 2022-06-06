using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResgistrationAPI.Models;

namespace ResgistrationAPI.Services
{
    //Serviço para ligar minha tabela customer com o MongoDB
    public class CustomerServices
    {
        private readonly IMongoCollection<customer> _customerCollection;

        //Configurações do meu BD
        public CustomerServices(IOptions<CustomerDatabaseSettings> customerServices)
        {
            var mongoClient = new MongoClient(customerServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(customerServices.Value.DatabaseName);

            //A ligação da tabela acontece aqui
            _customerCollection = mongoDatabase.GetCollection<customer>
                (customerServices.Value.CustomerCollectionName);
        }

        //Metodos de serviço para se usar na API

        //Get Todos
        public async Task<List<customer>> GetAsync() =>
            await _customerCollection.Find(x => true).ToListAsync();

        //Get Um
        public async Task<customer> GetAsync(string cpf) =>
            await _customerCollection.Find(x => x.Cpf == cpf).FirstOrDefaultAsync();

        //Post
        public async Task CreateAsync(customer customer) =>
            await _customerCollection.InsertOneAsync(customer);

        //Put
        public async Task UpdateAsync(string cpf, customer customer) =>
            await _customerCollection.ReplaceOneAsync(x => x.Cpf == cpf, customer);

        //Delete
        public async Task RemoveAsync(string cpf) =>
            await _customerCollection.DeleteOneAsync(x => x.Cpf == cpf);

        internal Task<customer> SaveChangeAsync()
        {
            throw new NotImplementedException("Bora testar");
        }
    }
}
