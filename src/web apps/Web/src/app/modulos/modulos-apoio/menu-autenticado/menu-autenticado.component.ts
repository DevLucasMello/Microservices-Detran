import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/Validacoes/localStorage';
import { Nav } from './nav';

@Component({
  selector: 'app-menu-autenticado',
  templateUrl: './menu-autenticado.component.html',
  styleUrls: ['./menu-autenticado.component.css']
})
export class MenuAutenticadoComponent implements OnInit {

  public localStorage: LocalStorageUtils = new LocalStorageUtils();

  isCollapsed = true;

  nav: Nav[] = [
    {
      link: '/condutor',
      name: 'Condutores',
      exact: true,
      home: false
    },
    {
      link: '/veiculo',
      name: 'Veículos',
      exact: true,
      home: false
    }
  ];

  constructor(private router: Router) { }

  ngOnInit() {
  }

  logout() {
    this.localStorage.limparDadosLocais();
    this.router.navigate(['']);
  }

}