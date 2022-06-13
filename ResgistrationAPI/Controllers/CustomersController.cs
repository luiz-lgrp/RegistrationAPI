using Microsoft.AspNetCore.Mvc;
using ResgistrationAPI.Models;
using ResgistrationAPI.Services;
using MongoDB.Driver;

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
            //var dbCustomer = await _customerServices.Customer
            //    .Find(f => f.Cpf == newCustomer.Cpf || f.Email == newCustomer.Email)
            //    .FirstOrDefaultAsync();

            //if (newCustomer != null)
            //{
            //    throw new Exception("teste");
            //}


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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Status(string id, Customer updateCustomer)
        //{

        //    var customer = await _customerServices.GetAsync(id);

        //    if (customer is null)
        //    {
        //        return NotFound();
        //    }

        //    if (updateCustomer.Active == true)
        //    {
        //        updateCustomer.Active = false;
        //    }
        //    else
        //    {
        //        updateCustomer.Active = true;
        //    }

        //    updateCustomer.Id = customer.Id;

        //    await _customerServices.UpdateAsync(id, updateCustomer);

        //    return NoContent();

        //}


    }
}
