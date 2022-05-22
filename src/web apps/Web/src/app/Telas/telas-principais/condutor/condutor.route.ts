import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AcessoNegadoComponent } from '../../telas-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from '../../telas-apoio/not-found/not-found.component';
import { CondutorComponent } from './condutor.component';
import { TodosCondutoresComponent } from './todos-condutores/todos-condutores.component';
import { CondutorGuard } from './services/condutor.guard';

const condutorRouterConfig: Routes = [
  {
    path: '', component: CondutorComponent,
    children: [

      { path: '', component: TodosCondutoresComponent, canActivate: [CondutorGuard] },

      { path: 'condutor', component: TodosCondutoresComponent, canActivate: [CondutorGuard] },

      { path: 'acesso-negado', component: AcessoNegadoComponent },
      
      { path: '**', component: NotFoundComponent }
    ]
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(condutorRouterConfig)
  ],
  exports: [RouterModule]
})
export class CondutorRoutingModule { }