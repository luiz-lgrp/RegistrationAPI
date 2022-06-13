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
            //Regras do Nome
            RuleFor(m => m.Name)
                .NotEmpty().NotNull()
                    .WithMessage("Por favor digite um nome")
                .MaximumLength(30)
                    .WithMessage("O campo nome não pode passar de 30 caracteres")
                .MinimumLength(3)
                    .WithMessage("O campo nome não pode ser menor que 03 caracteres");
            

            //Regras do Cpf 
            RuleFor(m => m.Cpf)
                .Matches("[0-9]{11}")
                    .WithMessage("Cpf inválido ou com pontos e traços");

            //Regras do Email
            RuleFor(m => m.Email)
                .NotEmpty().NotNull()
                    .WithMessage("Por favor digite um e-mail")
                
                .EmailAddress()
                    .WithMessage("Formato de E-mail inválido");

            //Regras do Phone
            RuleFor(m => m.Phone)
                .NotEmpty().NotNull()
                    .WithMessage("Por favor digite um telefone")
                .Matches("[1-9][0-9]{10}")
                    .WithMessage("Formato de telefone inválido")
                .MaximumLength(11)
                    .WithMessage("telefone não pode ter mais de 9 dígitos");



        }
    }
}
