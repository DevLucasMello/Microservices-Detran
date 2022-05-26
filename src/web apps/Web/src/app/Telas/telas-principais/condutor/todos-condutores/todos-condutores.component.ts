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

  public paginacao: Paginacao = new Paginacao();
  
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
        }
      });    
  }

  public obterCondutoresPorPlaca(page: number = 1, take: number = 8){
    this.condutorService.obterCondutoresPorPlaca(page, take, this.filtroPlaca)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);          
        }
      });
  }

  public carregarRegistros(event: any = 1){
    let page = event;
    if(this.filtroPlaca !== undefined && this.filtroPlaca !== ''){
      this.obterCondutoresPorPlaca(page);
    }
    else{
      this.obterTodosCondutores(page);
    }        
  }  

  private variaveisPaginacao(dados: ListaDados<Condutor[]>){    
    this.paginacao.pageIndex = dados.pageIndex;
    this.paginacao.pageSize = dados.pageSize;
    this.paginacao.totalResults = dados.totalResults;
  }

}
