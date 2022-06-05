import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeRootComponent } from './home-root.component';
import { HomeComponent } from './home/home.component';
import { HomeGuard } from './services/home.guard';
import { AcessoNegadoComponent } from '../../modulos-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from '../../modulos-apoio/not-found/not-found.component';

const homeRouterConfig: Routes = [
  {
    path: '', component: HomeRootComponent,
    children: [

      { path: '', component: HomeComponent, canActivate: [HomeGuard] },

      { path: 'home', component: HomeComponent, canActivate: [HomeGuard] },

      { path: 'acesso-negado', component: AcessoNegadoComponent },
      
      { path: '**', component: NotFoundComponent }
    ]
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(homeRouterConfig)
  ],
  exports: [RouterModule]
})
export class HomeRoutingModule { }