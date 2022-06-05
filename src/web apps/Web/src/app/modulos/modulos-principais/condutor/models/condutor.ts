export class Condutor
{
    public id: string;
    public primeiroNome: string;
    public ultimoNome: string;
    public cpf: string;
    public telefone: string;
    public email: string;
    public cnh: string;
    public dataNascimento: string;
    public veiculos: VeiculoCondutor[] = [];
}

class VeiculoCondutor
{
    public condutorId: string;
    public veiculoId: string;
    public placa: string;
}