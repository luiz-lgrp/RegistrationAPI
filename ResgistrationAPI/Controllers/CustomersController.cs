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


        
        [HttpGet]
        public async Task<List<Customer>> Get() =>
            await _customerServices.GetAsync();


        
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
       

       
        [HttpPost]
        public async Task<IActionResult> Post(Customer newCustomer)
        {
           
            await _customerServices.CreateAsync(newCustomer);

            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);

        }


        [HttpPut("{cpf}")]
        public async Task<IActionResult> Update(string cpf, Customer updatedCustomer)
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
