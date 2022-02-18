using System;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static bool IsPlaqueValid(string placa)
        {
            bool placaValida = true;

            if (placa.Length > 0)
            {
                if (placa.Length < 7)
                {
                    placaValida = false;
                }
                else
                {
                    var regex = new Regex("[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}|[A-Z]{3}[0-9]{4}");
                    var match = regex.Match(placa);

                    if (!match.Success)
                    {
                        placaValida = false;
                    }
                }
            }

            return placaValida;
        }
    }
}
