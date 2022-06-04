using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResgistrationAPI.Models;
using ResgistrationAPI.Services;
using System.Linq;

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
        public async Task<List<Customer>> GetCustomers() =>
            await _customerServices.GetAsync();

        //Get - Pesquisar por cpf

        [HttpGet("{cpf}")]
        public async Task<Customer> GetCpf(string cpf) =>
            await _customerServices.GetAsync(cpf);


       

        //Post - craindo e retornando o cliente
        [HttpPost]
        public async Task<Customer> PostCustomer(Customer customer)
        {
            await _customerServices.CreateAsync(customer);
            
            return customer;
        }

    }
}
