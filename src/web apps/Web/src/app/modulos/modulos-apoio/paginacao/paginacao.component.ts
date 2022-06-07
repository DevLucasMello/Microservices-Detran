import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Paginacao } from 'src/app/modelos/paginacao';
import { Store } from '../../modulos-principais/condutor/todo.store';

@Component({
  selector: 'app-paginacao',
  templateUrl: './paginacao.component.html',
  styleUrls: ['./paginacao.component.css']
})
export class PaginacaoComponent implements OnInit {
  
  @Output() paginacaoEmitter: EventEmitter<any> = new EventEmitter();

  public paginacao: Paginacao;
  
  public pagina1 = 1;
  public pagina2 = 0;
  public pagina3 = 0;
  public paginaAtiva = 1;

  public numeroPaginas = 1;
  public paginaAnterior = false;
  public proximaPagina = false;

  constructor(private store: Store) { }

  ngOnInit() {
    this.store.getPaginacao().subscribe(page => {
      if(page){
        this.paginacao = page;
        this.gerarPaginacao();
      }
    })
  }

  gerarPaginacao(){
    this.zerarVariaveis();    
    this.numeroPaginas = this.informarNumeroPaginas(this.paginacao.totalResults, this.paginacao.pageSize);
    this.habilitaDesabilitaAnteriorProximo(this.paginacao.pageIndex, this.numeroPaginas);
    this.paginacaoTela(this.numeroPaginas, this.paginacao.pageIndex);
  }

  private informarNumeroPaginas(totalResultados: number, registrosPorPagina: number): number{
    let paginas = totalResultados / registrosPorPagina;
    return (paginas > 1) ? Math.ceil(paginas) : 1;
  }

  private habilitaDesabilitaAnteriorProximo(paginaAtual: number, numPaginas: number){
    if(paginaAtual > 1) this.paginaAnterior = true;
    else this.paginaAnterior = false;
    if(paginaAtual < numPaginas) this.proximaPagina = true;
    else this.proximaPagina = false;    
  }

  public paginacaoTela(pagina: number, paginaAtiva: number){
    if((pagina >= 3) || (pagina == 2 && this.numeroPaginas >= 3) || (pagina == 1 && this.numeroPaginas >= 3)){
      if(paginaAtiva == 1){
        this.pagina1 = paginaAtiva;
        this.pagina2 = paginaAtiva + 1;
        this.pagina3 = paginaAtiva + 2;
      }
      else if(paginaAtiva == 2){
        this.pagina1 = paginaAtiva - 1;
        this.pagina2 = paginaAtiva;
        this.pagina3 = paginaAtiva + 1;
      }
      else if(paginaAtiva >= 3){
        this.pagina1 = paginaAtiva - 2;
        this.pagina2 = paginaAtiva - 1;
        this.pagina3 = paginaAtiva;
      }      
      this.paginaAtiva = paginaAtiva;
    }
    else if((pagina == 2 && this.numeroPaginas == 2) || (pagina == 1 && this.numeroPaginas == 2)){
      this.pagina1 = pagina - 1;
      this.pagina2 = pagina;
      this.paginaAtiva = paginaAtiva;
    }    
  }

  private zerarVariaveis(){
    this.pagina1 = 1;
    this.pagina2 = 0;
    this.pagina3 = 0;
    this.paginaAtiva = 1;
    this.numeroPaginas = 1;
    this.paginaAnterior = false;
    this.proximaPagina = false;
  }

  public emitirPaginacao(pagina: number) {
    if(pagina <= this.numeroPaginas && pagina > 0){
      this.paginacaoEmitter.emit(pagina);
    }
  }

}
