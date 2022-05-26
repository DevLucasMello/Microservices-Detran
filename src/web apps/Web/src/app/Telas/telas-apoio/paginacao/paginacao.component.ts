import { Component, Input, OnInit } from '@angular/core';
import { Paginacao } from 'src/app/Models/paginacao';

@Component({
  selector: 'app-paginacao',
  templateUrl: './paginacao.component.html',
  styleUrls: ['./paginacao.component.css']
})
export class PaginacaoComponent implements OnInit {

  @Input() paginacao: Paginacao;

  constructor() { }

  ngOnInit() {
  }

}
