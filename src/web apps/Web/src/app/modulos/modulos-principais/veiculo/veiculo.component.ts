import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-veiculo',  
  template: '<app-menu-autenticado></app-menu-autenticado><router-outlet></router-outlet><app-rodape-autenticado></app-rodape-autenticado>'
})
export class VeiculoComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}