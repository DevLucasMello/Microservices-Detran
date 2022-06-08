export class Paginacao{
    public totalResults: number;
    public pageIndex: number;
    public pageSize: number;
}

export class PaginacaoGeral{
    public pagina1: number;
    public pagina2: number;
    public pagina3: number;
    public paginaAtiva: number;
    public numeroPaginas: number;
    public paginaAnterior: boolean;
    public proximaPagina: boolean; 
}