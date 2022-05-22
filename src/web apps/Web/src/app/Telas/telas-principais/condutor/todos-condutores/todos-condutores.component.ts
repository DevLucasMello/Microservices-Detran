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

  public dados: ListaDados<Condutor[]>;

  constructor(private condutorService: CondutorService) { }

  ngOnInit() {
    this.obterTodosCondutores();
  }

  obterTodosCondutores(){
    this.condutorService.obterTodosCondutores()
      .subscribe(response => {
        if (response){
          this.dados = response;
        }
      });    
  }

}
