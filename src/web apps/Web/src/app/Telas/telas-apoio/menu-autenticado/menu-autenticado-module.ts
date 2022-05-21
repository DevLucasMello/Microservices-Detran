import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MenuAutenticadoComponent } from "./menu-autenticado.component";

@NgModule({
  declarations: [MenuAutenticadoComponent],
  imports: [CommonModule],
  exports: [MenuAutenticadoComponent]
})
export class MenuAutenticadoModule { }