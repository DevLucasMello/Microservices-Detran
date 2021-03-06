export class Veiculo
{
    public id: string;
    public condutorId: string;
    public placa: string;
    public modelo: string;
    public marca: string;
    public cor: string;
    public anoFabricacao: number;
    public cpf: string;
    public condutores: CondutorVeiculo[] = []
}    

class CondutorVeiculo
{
    public veiculoId: string;
    public condutorId: string;
    public cpf: string;
}