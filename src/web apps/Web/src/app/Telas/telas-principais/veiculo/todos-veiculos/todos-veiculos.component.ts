import { Component, OnInit } from '@angular/core';
import { ListaDados } from 'src/app/Models/lista-dados';
import { Veiculo } from '../models/veiculo';
import { VeiculoService } from '../services/veiculo.service';

@Component({
  selector: 'app-todos-veiculos',
  templateUrl: './todos-veiculos.component.html',
  styleUrls: ['./todos-veiculos.component.css']
})
export class TodosVeiculosComponent implements OnInit {

  public dados: ListaDados<Veiculo[]>;

  constructor(private veiculoService: VeiculoService) { }

  ngOnInit() {
    this.obterTodosVeiculos();
  }

  obterTodosVeiculos(){
    this.veiculoService.obterTodosVeiculos()
      .subscribe(response => {
        if (response){
          this.dados = response;
        }
      });    
  }

}
