import { CommonModule } from "@angular/common";
import { HttpClient, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AngularDraggableModule } from "angular2-draggable";
import { TextMaskModule } from "angular2-text-mask";
import { NgBrazil } from "ng-brazil";
import { CustomFormsModule } from "ng2-validation";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { ErrorInterceptor } from "src/app/util/error.handler.service";
import { AutenticadoModule } from "../../modulos-apoio/autenticado-module";
import { VeiculoService } from "../veiculo/services/veiculo.service";
import { CondutorComponent } from "./condutor.component";
import { CondutorRoutingModule } from "./condutor.route";
import { DetalheCondutorComponent } from "./detalhe-condutor/detalhe-condutor.component";
import { CondutorGuard } from "./services/condutor.guard";
import { CondutorService } from "./services/condutor.service";
import { StorePageCondutor } from "./services/condutor-paginacao.store";
import { TodosCondutoresComponent } from "./todos-condutores/todos-condutores.component";
import { StorePageVeiculo } from "../veiculo/services/veiculo-paginacao.store";

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];

@NgModule({
  declarations: [
    CondutorComponent,
    TodosCondutoresComponent,
    DetalheCondutorComponent    
  ],
  imports: [
    CommonModule, 
    CollapseModule.forRoot(),
    RouterModule,
    NgbModule,
    FormsModule,
    CondutorRoutingModule,
    ReactiveFormsModule,
    NgBrazil,
    CustomFormsModule,
    AngularDraggableModule,
    ModalModule.forRoot(),
    AutenticadoModule,
    TextMaskModule
  ],
  providers: [
    httpInterceptorProviders, 
    CondutorGuard, 
    CondutorService,
    VeiculoService, 
    HttpClient,
    StorePageCondutor,
    StorePageVeiculo
  ]
})
export class CondutorModule { }