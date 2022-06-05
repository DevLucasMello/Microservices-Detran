import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-condutor',  
  template: '<app-menu-autenticado></app-menu-autenticado><router-outlet></router-outlet><app-rodape-autenticado></app-rodape-autenticado>'
})
export class CondutorComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}