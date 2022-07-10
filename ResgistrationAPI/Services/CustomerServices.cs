using Microsoft.Extensions.Options;
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

        //Get Retorna Lista
        public async Task<List<Customer>> GetAsync() =>
            await _customerCollection.Find(x => true).ToListAsync();

        //Get Retorna um
        public async Task<Customer?> GetAsync(string cpf) =>
            await _customerCollection.Find(x => x.Cpf == cpf).FirstOrDefaultAsync();

        //Post
        public async Task CreateAsync(Customer newCustomer)
        {
            //Validação Cpf e Email Duplicado
            var dbCustomer = await _customerCollection
                .Find(f => f.Cpf == newCustomer.Cpf || f.Email == newCustomer.Email)
                .FirstOrDefaultAsync();

            if (dbCustomer != null)
                throw new Exception("Já possui um cliente com este cpf ou e-mail");

            await _customerCollection.InsertOneAsync(newCustomer);
            
        }
            


        //Put
        public async Task UpdateAsync(string cpf, Customer updateCustomer) =>
            await _customerCollection.ReplaceOneAsync(x => x.Cpf == cpf, updateCustomer);

        

        //Delete
        public async Task RemoveAsync(string cpf) =>
            await _customerCollection.DeleteOneAsync(x => x.Cpf == cpf);

        

    }
}
