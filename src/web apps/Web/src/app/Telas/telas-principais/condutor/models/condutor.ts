export class Condutor
{
    public Id: string;
    public PrimeiroNome: string;
    public UltimoNome: string;
    public CPF: string;
    public Telefone: string;
    public Email: string;
    public CNH: string;
    public DataNascimento: string;
    public Veiculos: VeiculoCondutor[] = [];
}

class VeiculoCondutor
{
    public CondutorId: string;
    public VeiculoId: string;
    public Placa: string;
}