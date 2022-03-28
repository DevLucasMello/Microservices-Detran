namespace TP.Condutores.Application.Messages
{
    public static class CondutorCommandErrorMessages
    {
        public static string IdNuloErroMsg => "O Id não foi informado";
        public static string CondutorIdNuloErroMsg => "Id do condutor inválido";
        public static string VeiculoIdNuloErroMsg => "Id do veículo inválido";
        public static string PlacaNuloErroMsg => "A placa deve ser informada";
        public static string PlacaInvalidaErroMsg => "A placa informada é inválida";
        public static string PrimeiroNomeNuloErroMsg => "O primeiro nome não foi informado";
        public static string UltimoNomeNuloErroMsg => "O último nome não foi informado";
        public static string CPFNuloErroMsg => "O CPF deve ser informado";
        public static string TelefoneNuloErroMsg => "O Telefone deve ser preenchido";
        public static string EmailNuloErroMsg => "O Email não foi informado";
        public static string CNHNuloErroMsg => "A CNH deve ser informada";
        public static string DataNascimentoNuloErroMsg => "A Data de Nascimento deve ser informada";
        public static string PrimeiroNomeQtdErroMsg => "O campo nome deve ter entre 3 e 150 caracteres";
        public static string UltimoNomeQtdErroMsg => "O campo nome deve ter entre 3 e 150 caracteres";
        public static string CPFInvalidoErroMsg => "O CPF informado é inválido";
        public static string TelefoneQtdErroMsg => "Informe o telefone com no mínimo 9 digitos";
        public static string EmailInvalidoErroMsg => "Endereço de E-mail inválido";
        public static string DataNascimentoMenor18ErroMsg => "O Condutor deve ter no mínimo 18 anos";
    }
}