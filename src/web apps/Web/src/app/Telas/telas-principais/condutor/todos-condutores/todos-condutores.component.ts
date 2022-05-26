import { Component, OnInit } from '@angular/core';
import { ListaDados } from 'src/app/Models/lista-dados';
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
  
  public dados: ListaDados<Condutor>;

  constructor(private condutorService: CondutorService) { }

  ngOnInit() {
    this.obterTodosCondutores();
  }

  obterTodosCondutores(){
    this.condutorService.obterTodosCondutores()
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.numeroPaginas = this.informarNumeroPaginas(this.dados);
          this.habilitaDesabilitaAnterior(this.dados.pageIndex, this.numeroPaginas);
        }
      });    
  }

  private informarNumeroPaginas(dados: ListaDados<Condutor>): number{
    let paginas = dados.totalResults / dados.pageSize;
    return (paginas > 1) ? paginas : 1;
  }

  private habilitaDesabilitaAnterior(paginaAtual: number, numPaginas: number){
    if(paginaAtual > 1) this.paginaAnterior = true;
    if(paginaAtual < numPaginas) this.proximaPagina = true;
    if(numPaginas > 1) this.pagina2 = 2;
    if(numPaginas > 2) this.pagina3 = 3;
  }

  public mudarPagina(pagina: number){
    if((pagina >= 3) || (pagina == 2 && this.numeroPaginas >= 3) || (pagina == 1 && this.numeroPaginas >= 3)){
      this.pagina1 = pagina - 2;
      this.pagina2 = pagina - 1;
      this.pagina3 = pagina;
      this.paginaAtiva = pagina;
    }
    else if((pagina == 2 && this.numeroPaginas == 2) || (pagina == 1 && this.numeroPaginas == 2)){
      this.pagina1 = pagina - 1;
      this.pagina2 = pagina;
      this.paginaAtiva = pagina;
    }    
  }

}
