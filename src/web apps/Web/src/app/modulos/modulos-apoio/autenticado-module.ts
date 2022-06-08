import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { MenuAutenticadoComponent } from "./menu-autenticado/menu-autenticado.component";
import { PaginacaoCondutorComponent } from "./paginacao/paginacao-condutor/paginacao-condutor.component";
import { PaginacaoVeiculoComponent } from "./paginacao/paginacao-veiculo/paginacao-veiculo.component";
import { RodapeAutenticadoComponent } from "./rodape-autenticado/rodape-autenticado.component";

@NgModule({
  declarations: [
    MenuAutenticadoComponent, 
    RodapeAutenticadoComponent,
    PaginacaoCondutorComponent,
    PaginacaoVeiculoComponent
  ],
  imports: [
    CommonModule, 
    CollapseModule.forRoot(),
    RouterModule
  ],
  exports: [
    MenuAutenticadoComponent, 
    RodapeAutenticadoComponent,
    PaginacaoCondutorComponent,
    PaginacaoVeiculoComponent
  ]
})
export class AutenticadoModule { }