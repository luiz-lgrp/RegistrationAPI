using Microsoft.AspNetCore.Mvc;
using ResgistrationAPI.Models;
using ResgistrationAPI.Services;

namespace ResgistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerServices _customerServices;

        public CustomersController(CustomerServices customerServices) =>
             _customerServices = customerServices;



        //Agora vem as APi's


        //Get - lista dos clientes
        [HttpGet]
        public async Task<List<Customer>> Get() =>
            await _customerServices.GetAsync();


        //Get - Pesquisar por cpf
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Customer>> Get(string cpf) 
        {
            
            var customer = await _customerServices.GetAsync(cpf);

            if (customer is null)
            {
                return NotFound();
            }

           return customer;
        }
       

        //Post - criando e retornando o cliente com Status Code 201
        [HttpPost]
        public async Task<IActionResult> Post(Customer newCustomer)
        {
            await _customerServices.CreateAsync(newCustomer);

            return CreatedAtAction(nameof(Get), new { cpf = newCustomer.Cpf }, newCustomer);
        }

        //Put FALTA CORRIGIR ERRO ?
        [HttpPut("{cpf}")]
        public async Task<IActionResult> Put(string cpf, Customer updatedCustomer)
        {

            var customer = await _customerServices.GetAsync(cpf);

            if (customer is null)
            {
                return NotFound();
            }

            updatedCustomer.Cpf = customer.Cpf;

            await _customerServices.UpdateAsync(cpf, updatedCustomer);

            return NoContent();

        }

        //Delete 
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf)
        {
            var customer = await _customerServices.GetAsync(cpf);

            if (customer is null)
            {
                return NotFound();
            }

            await _customerServices.RemoveAsync(cpf);

            return NoContent();
        }


    }
}
