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
import { AutenticadoModule } from "../../telas-apoio/autenticado-module";
import { CondutorComponent } from "./condutor.component";
import { CondutorRoutingModule } from "./condutor.route";
import { CondutorGuard } from "./services/condutor.guard";
import { CondutorService } from "./services/condutor.service";
import { TodosCondutoresComponent } from "./todos-condutores/todos-condutores.component";

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];

@NgModule({
  declarations: [
    CondutorComponent,
    TodosCondutoresComponent    
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
    AutenticadoModule
  ],
  providers: [
    httpInterceptorProviders, 
    CondutorGuard, 
    CondutorService, 
    HttpClient, 
    {provide: LOCALE_ID, useValue: 'pt-br'}
  ]
})
export class CondutorModule { }