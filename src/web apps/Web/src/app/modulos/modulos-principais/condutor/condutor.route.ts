import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AcessoNegadoComponent } from '../../modulos-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from '../../modulos-apoio/not-found/not-found.component';
import { CondutorComponent } from './condutor.component';
import { TodosCondutoresComponent } from './todos-condutores/todos-condutores.component';
import { CondutorGuard } from './services/condutor.guard';
import { DetalheCondutorComponent } from './detalhe-condutor/detalhe-condutor.component';

const condutorRouterConfig: Routes = [
  {
    path: '', component: CondutorComponent,
    children: [

      { path: '', component: TodosCondutoresComponent, canActivate: [CondutorGuard] },

      { path: 'condutor', component: TodosCondutoresComponent, canActivate: [CondutorGuard] },

      { path: 'cadastrar', component: DetalheCondutorComponent, canActivate: [CondutorGuard] },

      { path: 'editar/:id', component: DetalheCondutorComponent, canActivate: [CondutorGuard] },      

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