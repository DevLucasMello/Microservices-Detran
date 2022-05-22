import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { MenuAutenticadoComponent } from "./menu-autenticado/menu-autenticado.component";
import { RodapeAutenticadoComponent } from "./rodape-autenticado/rodape-autenticado.component";

@NgModule({
  declarations: [
    MenuAutenticadoComponent, 
    RodapeAutenticadoComponent
  ],
  imports: [
    CommonModule, 
    CollapseModule.forRoot(),
    RouterModule
  ],
  exports: [
    MenuAutenticadoComponent, 
    RodapeAutenticadoComponent
  ]
})
export class AutenticadoModule { }