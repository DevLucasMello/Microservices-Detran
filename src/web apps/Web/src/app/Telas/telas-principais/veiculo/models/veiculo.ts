export class Veiculo
{
    public Id: string;
    public Placa: string;
    public Modelo: string;
    public Marca: string;
    public Cor: string;
    public AnoFabricacao: number;
    public Condutores: CondutorVeiculo[] = []
}    

class CondutorVeiculo
{
    public VeiculoId: string;
    public CondutorId: string;
    public CPF: string;
}