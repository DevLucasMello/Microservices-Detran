import { CommonModule } from "@angular/common";
import { HttpClient, HTTP_INTERCEPTORS } from "@angular/common/http";
import { LOCALE_ID, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AngularDraggableModule } from "angular2-draggable";
import { NgBrazil } from "ng-brazil";
import { CustomFormsModule } from "ng2-validation";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { ErrorInterceptor } from "src/app/Validacoes/error.handler.service";
import { AutenticadoModule } from "../../modulos-apoio/autenticado-module";
import { VeiculoGuard } from "./services/veiculo.guard";
import { VeiculoService } from "./services/veiculo.service";
import { TodosVeiculosComponent } from "./todos-veiculos/todos-veiculos.component";
import { VeiculoComponent } from "./veiculo.component";
import { VeiculoRoutingModule } from "./veiculo.route";

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];

@NgModule({
  declarations: [
    VeiculoComponent,
    TodosVeiculosComponent    
  ],
  imports: [
    CommonModule, 
    CollapseModule.forRoot(),
    RouterModule,
    NgbModule,
    FormsModule,
    VeiculoRoutingModule,
    ReactiveFormsModule,
    NgBrazil,
    CustomFormsModule,
    AngularDraggableModule,
    ModalModule.forRoot(),
    AutenticadoModule
  ],
  providers: [
    httpInterceptorProviders, 
    VeiculoGuard, 
    VeiculoService, 
    HttpClient, 
    {provide: LOCALE_ID, useValue: 'pt-br'}
  ]
  
})
export class VeiculoModule { }