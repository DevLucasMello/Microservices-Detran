import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Paginacao, PaginacaoGeral } from 'src/app/modelos/paginacao';
import { StorePageVeiculo } from 'src/app/modulos/modulos-principais/veiculo/services/veiculo-paginacao.store';
import { PaginacaoTela } from '../../../../modelos/paginacao-tela';

@Component({
  selector: 'app-paginacao-veiculo',
  templateUrl: './paginacao-veiculo.component.html',
  styleUrls: ['./paginacao-veiculo.component.css']
})
export class PaginacaoVeiculoComponent extends PaginacaoTela implements OnInit{

  @Output() paginacaoVeiculoEmitter: EventEmitter<any> = new EventEmitter();

  public paginacaoCondutor: Paginacao;

  public paginacaoGeral: PaginacaoGeral = new PaginacaoGeral();

  constructor(private store: StorePageVeiculo) { super() }

  ngOnInit() {
    this.store.getPaginacao().subscribe(page => {
      if(page){
        this.paginacaoCondutor = page;
        this.gerarPaginacao();
      }
    })
  }

  gerarPaginacao(){
    this.paginacaoGeral = super.zerarVariaveis();
    this.paginacaoGeral.numeroPaginas = super.informarNumeroPaginas(this.paginacaoCondutor.totalResults, this.paginacaoCondutor.pageSize);

    let page = super.habilitaDesabilitaAnteriorProximo(this.paginacaoCondutor.pageIndex, this.paginacaoGeral.numeroPaginas);
    this.paginacaoGeral.paginaAnterior = page.paginaAnterior;
    this.paginacaoGeral.proximaPagina = page.proximaPagina;
    
    let page2 = super.paginacaoTela(this.paginacaoGeral.numeroPaginas, this.paginacaoCondutor.pageIndex);
    if(page2.pagina1){
      this.paginacaoGeral.pagina1 = page2.pagina1;
      this.paginacaoGeral.pagina2 = page2.pagina2; 
      this.paginacaoGeral.pagina3 = page2.pagina3; 
      this.paginacaoGeral.paginaAtiva = page2.paginaAtiva;
    }         
  }

  public emitirPaginacao(pagina: number) {
    if(pagina <= this.paginacaoGeral.numeroPaginas && pagina > 0){
      this.paginacaoVeiculoEmitter.emit(pagina);
    }
  }

}
