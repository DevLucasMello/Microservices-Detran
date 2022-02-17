namespace TP.Core.DomainObjects
{
    public class Nome
    {
        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }

        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
        }

        // EF Rel.
        protected Nome() { }

        public string NomeCompleto()
        {
            return $"{PrimeiroNome} {UltimoNome}";
        }

        public override string ToString()
        {
            return NomeCompleto();
        }
    }
}
