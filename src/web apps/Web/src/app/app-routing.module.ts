import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AcessoNegadoComponent } from './Telas/telas-apoio/acesso-negado/acesso-negado.component';
import { LoginComponent } from './Telas/telas-principais/login/login/login.component';
import { RegistroComponent } from './Telas/telas-principais/login/registro/registro.component';
import { NotFoundComponent } from './Telas/telas-apoio/not-found/not-found.component';

const routes: Routes = [

  { path: '', component: LoginComponent},

  { path: 'login', component: LoginComponent},

  { path: 'registro', component: RegistroComponent},

  { path: 'home', loadChildren: () => import('./Telas/telas-principais/home/home-module').then(m => m.HomeModule) },

  { path: 'acesso-negado', component: AcessoNegadoComponent },
  
  { path: 'nao-encontrado', component: NotFoundComponent },

  { path: '**', component: NotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
