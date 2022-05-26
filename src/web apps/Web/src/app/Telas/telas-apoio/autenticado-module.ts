import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { MenuAutenticadoComponent } from "./menu-autenticado/menu-autenticado.component";
import { PaginacaoComponent } from "./paginacao/paginacao.component";
import { RodapeAutenticadoComponent } from "./rodape-autenticado/rodape-autenticado.component";

@NgModule({
  declarations: [
    MenuAutenticadoComponent, 
    RodapeAutenticadoComponent,
    PaginacaoComponent
  ],
  imports: [
    CommonModule, 
    CollapseModule.forRoot(),
    RouterModule
  ],
  exports: [
    MenuAutenticadoComponent, 
    RodapeAutenticadoComponent,
    PaginacaoComponent
  ]
})
export class AutenticadoModule { }