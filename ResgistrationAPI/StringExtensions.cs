using System.Text.RegularExpressions;

//Método Usado para fazer a validação do CPF
namespace ResgistrationAPI
{
    public static class StringExtensions
    {
        //No caso o metodo aqui só valida se o CPF está na estrutura requerida
        public static bool IsValidCpf(this string cpf)
        {
            var expression = "[0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}-?[0-9]{2}";

            return Regex.Match(cpf, expression).Success;
        }
    }
}
