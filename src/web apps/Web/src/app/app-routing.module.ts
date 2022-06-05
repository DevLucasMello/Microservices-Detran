import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AcessoNegadoComponent } from './modulos/modulos-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from './modulos/modulos-apoio/not-found/not-found.component';
import { LoginComponent } from './modulos/modulos-principais/login/login/login.component';
import { RegistroComponent } from './modulos/modulos-principais/login/registro/registro.component';

const routes: Routes = [

  { path: '', component: LoginComponent},

  { path: 'login', component: LoginComponent},

  { path: 'registro', component: RegistroComponent},

  { path: 'home', loadChildren: () => import('./modulos/modulos-principais/home/home-module').then(m => m.HomeModule) },

  { path: 'condutor', loadChildren: () => import('./modulos/modulos-principais/condutor/condutor.module').then(m => m.CondutorModule) },

  { path: 'veiculo', loadChildren: () => import('./modulos/modulos-principais/veiculo/veiculo.module').then(m => m.VeiculoModule) },

  { path: 'acesso-negado', component: AcessoNegadoComponent },
  
  { path: 'nao-encontrado', component: NotFoundComponent },

  { path: '**', component: NotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
