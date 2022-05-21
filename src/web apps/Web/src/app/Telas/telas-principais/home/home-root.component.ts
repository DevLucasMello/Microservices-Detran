import { Component } from "@angular/core";

@Component({
  selector: 'home-app-root',  
  template: '<app-menu-autenticado></app-menu-autenticado><router-outlet></router-outlet><app-rodape-autenticado></app-rodape-autenticado>'
})
export class HomeRootComponent { }