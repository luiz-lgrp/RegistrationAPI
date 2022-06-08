using FluentValidation;
using ResgistrationAPI.Models;

//Estou usando a bibliotece FluentValidation
namespace ResgistrationAPI.Validators
{
    public class AddCustomerValidator : AbstractValidator<Customer>
    {
        // No construtor estou colocando as regras para validação
        public AddCustomerValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                    .WithMessage("O campo nome não pode estar vazio")
                .MaximumLength(30)
                    .WithMessage("O campo nome não pode passar de 30 caracteres")
                .MinimumLength(3)
                    .WithMessage("O campo nome não pode ser menor que 03 caracteres");

            //Olhar o método criado para validar o CPF
            RuleFor(m => m.Cpf)
                .Must(c => c.IsValidCpf())
                    .WithMessage("CPF inválido");

        }
    }
}
