using System;
using System.Linq;

namespace TP.Core.Utils
{
    public static class MethodsUtils
    {
        public static string ApenasNumeros(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

        public static bool CondutorMaiorDeIdade(DateTime dataNascimento)
        {
            return dataNascimento <= DateTime.Now.AddYears(-18);
        }

        public static bool IsCpfValid(string cpf)
        {
            return DomainObjects.Cpf.Validar(cpf);
        }

        public static bool IsEmailValid(string email)
        {
            return DomainObjects.Email.Validar(email);
        }
    }
}
