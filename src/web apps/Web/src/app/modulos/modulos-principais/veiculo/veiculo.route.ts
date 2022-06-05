import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AcessoNegadoComponent } from '../../modulos-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from '../../modulos-apoio/not-found/not-found.component';
import { VeiculoComponent } from './veiculo.component';
import { TodosVeiculosComponent } from './todos-veiculos/todos-veiculos.component';
import { VeiculoGuard } from './services/veiculo.guard';

const condutorRouterConfig: Routes = [
  {
    path: '', component: VeiculoComponent,
    children: [

      { path: '', component: TodosVeiculosComponent, canActivate: [VeiculoGuard] },

      { path: 'veiculo', component: TodosVeiculosComponent, canActivate: [VeiculoGuard] },

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
export class VeiculoRoutingModule { }