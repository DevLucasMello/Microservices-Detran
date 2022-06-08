import { PaginacaoGeral } from "src/app/modelos/paginacao";

export abstract class PaginacaoTela{

    protected informarNumeroPaginas(totalResultados: number, registrosPorPagina: number): number{
        let paginas = totalResultados / registrosPorPagina;
        return (paginas > 1) ? Math.ceil(paginas) : 1;
    }

    protected habilitaDesabilitaAnteriorProximo(paginaAtual: number, numPaginas: number): PaginacaoGeral{
        let page = new PaginacaoGeral();
        if(paginaAtual > 1) page.paginaAnterior = true;
        else page.paginaAnterior = false;
        if(paginaAtual < numPaginas) page.proximaPagina = true;
        else page.proximaPagina = false;
        
        return page;
    }

    protected paginacaoTela(pagina: number, paginaAtiva: number): PaginacaoGeral{
        let page = new PaginacaoGeral();
        if(pagina >= 3){
            if(paginaAtiva == 1){
            page.pagina1 = paginaAtiva;
            page.pagina2 = paginaAtiva + 1;
            page.pagina3 = paginaAtiva + 2;
            }
            else if(paginaAtiva == 2){
            page.pagina1 = paginaAtiva - 1;
            page.pagina2 = paginaAtiva;
            page.pagina3 = paginaAtiva + 1;
            }
            else if(paginaAtiva >= 3){
            page.pagina1 = paginaAtiva - 2;
            page.pagina2 = paginaAtiva - 1;
            page.pagina3 = paginaAtiva;
            }      
            page.paginaAtiva = paginaAtiva;
        }
        else if((pagina == 2) || (pagina == 1)){
            page.pagina1 = pagina - 1;
            page.pagina2 = pagina;
            page.paginaAtiva = paginaAtiva;
        }
        return page;    
    }

    protected zerarVariaveis(): PaginacaoGeral{
        let page = new PaginacaoGeral();
        page.pagina1 = 1;
        page.pagina2 = 0;
        page.pagina3 = 0;
        page.paginaAtiva = 1;
        page.numeroPaginas = 1;
        page.paginaAnterior = false;
        page.proximaPagina = false;
        return page;
    }
}