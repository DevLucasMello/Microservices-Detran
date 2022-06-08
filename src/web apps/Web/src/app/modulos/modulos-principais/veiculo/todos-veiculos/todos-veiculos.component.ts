import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ListaDados } from 'src/app/modelos/lista-dados';
import { Paginacao } from 'src/app/modelos/paginacao';
import { Veiculo } from '../models/veiculo';
import { VeiculoService } from '../services/veiculo.service';
import { Condutor } from '../../condutor/models/condutor';
import { CondutorService } from '../../condutor/services/condutor.service';

@Component({
  selector: 'app-todos-veiculos',
  templateUrl: './todos-veiculos.component.html',
  styleUrls: ['./todos-veiculos.component.css']
})
export class TodosVeiculosComponent implements OnInit {

  public paginacao: Paginacao = new Paginacao();
  public paginacaoCondutor: Paginacao = new Paginacao();

  public dados: ListaDados<Veiculo>;
  public condutores: ListaDados<Condutor>
  public veiculoSelecionado: Veiculo;
  public filtroModelo: string;
  public filtroCpf: string;
  public placaSelecionada: string;

  @ViewChild('mdExcluir', { static: true })
  public mdExcluir: any;

  @ViewChild('mdCondutores', { static: true })
  public mdCondutores: any;

  constructor(
    private condutorService: CondutorService,
    private veiculoService: VeiculoService,
    private router: Router, 
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.paginacao.pageIndex = 0;
    this.paginacaoCondutor.pageIndex = 0;
    this.carregarRegistros();
  }

  obterTodosVeiculos(page: number = 1, take: number = 8, query: string = ''){
    this.veiculoService.obterTodosVeiculos(page, take, query)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);
        }
        else{
          this.semVeiculos();
        }
      });    
  }

  public obterVeiculosPorCpf(page: number = 1, take: number = 8){
    this.veiculoService.obterVeiculosPorCpf(page, take, this.filtroCpf)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);
        }
        else{
          this.semVeiculos();
        }
      });
  }

  public carregarRegistros(event: any = 1){
    let page = event;
    if(this.verificaNullUndefined(this.filtroCpf)){
      this.obterVeiculosPorCpf(page);
    }
    else if(this.verificaNullUndefined(this.filtroModelo)){
      this.obterTodosVeiculos(page, 8, this.filtroModelo);
    }
    else{
      this.obterTodosVeiculos(page);
    }        
  }

  private semVeiculos(){
    this.paginacao.pageIndex = 0;
    this.dados.list = [];
  }

  private verificaNullUndefined(dado: string): boolean{
    if(dado !== undefined && dado !== '') return true;
    else return false
  }

  private variaveisPaginacao(dados: ListaDados<Veiculo>){
    this.paginacao.pageIndex = dados.pageIndex;
    this.paginacao.pageSize = dados.pageSize;
    this.paginacao.totalResults = dados.totalResults;
  }
  
  private paginacaoCondutores(dados: ListaDados<Condutor>){    
    this.paginacaoCondutor.pageIndex = dados.pageIndex;
    this.paginacaoCondutor.pageSize = dados.pageSize;
    this.paginacaoCondutor.totalResults = dados.totalResults;
  }

  public cadastrarVeiculo(){
    this.router.navigate(['cadastrar'], { relativeTo: this.route});
  }

  public editarVeiculo(id: string){
    this.router.navigate(['editar', id], { relativeTo: this.route});
  }

  abrirExclusao(id: string): void{
    this.paginacaoCondutor.pageIndex = 0;
    this.dados.list.forEach(x => {
      if(x.id == id) {
        this.preencherDadoVeiculoSelecionado(x);
        this.obterCondutoresPorPlaca(x.placa, 1);
      }
    });    
    this.mdExcluir.show();
  }

  preencherDadoVeiculoSelecionado(x: Veiculo){
    this.veiculoSelecionado = new Veiculo();
    this.veiculoSelecionado.id = x.id;
    this.veiculoSelecionado.placa = x.placa;
    this.veiculoSelecionado.modelo = x.modelo;
    this.veiculoSelecionado.marca = x.marca;
    this.veiculoSelecionado.cor = x.cor;
    this.veiculoSelecionado.anoFabricacao = x.anoFabricacao;
    this.veiculoSelecionado.condutores = x.condutores === null ? [] : x.condutores;
  }

  public converterDataNascimento(data: string): string{
    return data.substring(0, 10);    
  }

  excluirVeiculo(){    
    this.veiculoService.excluirVeiculo(this.veiculoSelecionado.id)
      .subscribe(response => {
        if (response){
          this.mdExcluir.hide();
          setTimeout(() => {document.location.reload();}, 3000);
          this.toastr.success('Veículo excluído com sucesso!');          
        }
        else{
          this.toastr.error('Erro ao excluir veículo');
        }
      })
  }

  abrirCondutores(placa: string, page: number = 1, take: number = 4){
    this.condutores = new ListaDados<Condutor>();
    this.paginacaoCondutor.pageIndex = 0;
    this.placaSelecionada = placa;
    this.obterCondutoresPorPlaca(placa, page, take);
    this.mdCondutores.show();
  }

  private obterCondutoresPorPlaca(placa: string, page: number, take: number = 4){
    this.condutorService.obterCondutoresPorPlaca(page, take, placa)
      .subscribe(response => {
        if (response){
          this.condutores = response;
          this.paginacaoCondutores(this.condutores);
        }
      });
  }

  public carregarCondutores(event: any = 1){
    let page = event;
    this.obterCondutoresPorPlaca(this.placaSelecionada, page);
  }
}