using Microsoft.AspNetCore.Mvc;
using ResgistrationAPI.Models;
using ResgistrationAPI.Services;



namespace ResgistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerServices _customerServices;

        public CustomersController(CustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        //Agora vem as APi's

        //Get - lista dos clientes
        [HttpGet]
        public async Task<List<customer>> GetCustomers() =>
            await _customerServices.GetAsync();


        //Get - Pesquisar por cpf

        [HttpGet("{cpf}")]
        public async Task<customer> GetCpf(string cpf) =>
            await _customerServices.GetAsync(cpf);


       

        //Post - craindo e retornando o cliente
        [HttpPost]
        public async Task<customer> PostCustomer(customer customer)
        {
            await _customerServices.CreateAsync(customer);
            
            return customer;
        }

        //Put FALTA CORRIGIR ERRO 500
        [HttpPut("{cpf}")]
        public async Task<customer> PutCpf(string cpf, customer customer)
        {
            var dbCustomar = await _customerServices.GetAsync(cpf);

            dbCustomar.Name = customer.Name;
            dbCustomar.Cpf = customer.Cpf;
            dbCustomar.Email = customer.Email;
            dbCustomar.Adress = customer.Adress;
            dbCustomar.Phone = customer.Phone;

            _customerServices.DeleteAsync(cpf, dbCustomar);


            return await _customerServices.SaveChangeAsync();
           
        }

        //Delete
        [HttpDelete("{cpf}")]
        public async Task<customer> DelCpf(string cpf, customer customer)
        {
            var dbCustomar = await _customerServices.GetAsync(cpf);


             _customerServices.DeleteAsync(cpf, dbCustomar);


            return await _customerServices.SaveChangeAsync();

        }


    }
}
