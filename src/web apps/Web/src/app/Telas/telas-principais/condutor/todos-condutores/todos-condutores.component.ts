import { Component, OnInit } from '@angular/core';
import { ListaDados } from 'src/app/Models/lista-dados';
import { Paginacao } from 'src/app/Models/paginacao';
import { Condutor } from '../models/condutor';
import { CondutorService } from '../services/condutor.service';

@Component({
  selector: 'app-todos-condutores',
  templateUrl: './todos-condutores.component.html',
  styleUrls: ['./todos-condutores.component.css']
})
export class TodosCondutoresComponent implements OnInit {

  public pagina1 = 1;
  public pagina2 = 0;
  public pagina3 = 0;
  public paginaAtiva = 1;

  public numeroPaginas = 1;
  public paginaAnterior = false;
  public proximaPagina = false;

  public paginacao: Paginacao;
  
  public dados: ListaDados<Condutor[]>;
  public filtroPlaca: string;

  constructor(private condutorService: CondutorService) { }

  ngOnInit() {
    this.carregarRegistros();
  }

  obterTodosCondutores(page: number = 1, take: number = 8){
    this.condutorService.obterTodosCondutores(page, take)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);
          this.numeroPaginas = this.informarNumeroPaginas(this.dados.totalResults, this.dados.pageSize);
          this.habilitaDesabilitaAnteriorProximo(this.dados.pageIndex, this.numeroPaginas);
          this.paginacaoTela(this.numeroPaginas, this.dados.pageIndex);
        }
      });    
  }

  public obterCondutoresPorPlaca(page: number = 1, take: number = 8){
    this.condutorService.obterCondutoresPorPlaca(page, take, this.filtroPlaca)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);
          this.numeroPaginas = this.informarNumeroPaginas(this.dados.totalResults, this.dados.pageSize);
          this.habilitaDesabilitaAnteriorProximo(this.dados.pageIndex, this.numeroPaginas);
          this.paginacaoTela(this.numeroPaginas, this.dados.pageIndex);
        }
      });
  }

  public carregarRegistros(page: number = 1, take: number = 8){
    this.zerarVariaveis();
    if(this.filtroPlaca !== undefined && this.filtroPlaca !== ''){
      this.obterCondutoresPorPlaca(page, take);
    }
    else{
      this.obterTodosCondutores(page, take);
    }        
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

  private variaveisPaginacao(dados: ListaDados<Condutor[]>){
    this.paginacao.pageIndex = dados.pageIndex;
    this.paginacao.pageSize = dados.pageSize;
    this.paginacao.totalResults = dados.totalResults;
  }

}
