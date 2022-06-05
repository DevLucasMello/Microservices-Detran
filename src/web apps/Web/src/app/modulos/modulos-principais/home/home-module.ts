import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeRootComponent } from './home-root.component';
import { HomeRoutingModule } from './home-route';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { NgBrazil } from 'ng-brazil';
import { TextMaskModule } from 'angular2-text-mask';
import { CustomFormsModule } from 'ng2-validation';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { AngularDraggableModule } from 'angular2-draggable';
import { ModalModule } from 'ngx-bootstrap/modal';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from 'src/app/Validacoes/error.handler.service';
import { HomeGuard } from './services/home.guard';
import { AutenticadoModule } from '../../modulos-apoio/autenticado-module';

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];

@NgModule({
  declarations: [
    HomeRootComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    FormsModule,
    CollapseModule.forRoot(),
    HomeRoutingModule,
    ReactiveFormsModule,
    NgBrazil,
    CustomFormsModule,
    AngularDraggableModule,
    ModalModule.forRoot(),
    AutenticadoModule
  ],  
  providers: [
    httpInterceptorProviders,
    HomeGuard
  ]
})
export class HomeModule { }
