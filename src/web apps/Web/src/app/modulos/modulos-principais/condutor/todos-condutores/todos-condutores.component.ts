import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ListaDados } from 'src/app/modelos/lista-dados';
import { Paginacao } from 'src/app/modelos/paginacao';
import { Veiculo } from '../../veiculo/models/veiculo';
import { VeiculoService } from '../../veiculo/services/veiculo.service';
import { Condutor } from '../models/condutor';
import { CondutorService } from '../services/condutor.service';

@Component({
  selector: 'app-todos-condutores',
  templateUrl: './todos-condutores.component.html',
  styleUrls: ['./todos-condutores.component.css']
})
export class TodosCondutoresComponent implements OnInit {  

  public paginacao: Paginacao = new Paginacao();
  public paginacaoVeiculo: Paginacao = new Paginacao();
  
  public dados: ListaDados<Condutor>;
  public veiculos: ListaDados<Veiculo>
  public condutorSelecionado: Condutor;
  public filtroNome: string;
  public filtroPlaca: string;
  public cpfSelecionado: string;

  @ViewChild('mdExcluir', { static: true })
  public mdExcluir: any;

  @ViewChild('mdVeiculos', { static: true })
  public mdVeiculos: any;

  constructor(
    private condutorService: CondutorService,
    private veiculoService: VeiculoService,
    private router: Router, 
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit() {    
    this.paginacao.pageIndex = 0;
    this.paginacaoVeiculo.pageIndex = 0;
    this.carregarRegistros();
  }

  obterTodosCondutores(page: number = 1, take: number = 8, query: string = ''){
    this.condutorService.obterTodosCondutores(page, take, query)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);                   
        }
        else{
          this.semCondutores();
        }
      });    
  }

  public obterCondutoresPorPlaca(page: number = 1, take: number = 8){
    this.condutorService.obterCondutoresPorPlaca(page, take, this.filtroPlaca)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);          
        }
        else{
          this.semCondutores();
        }
      });
  }

  public carregarRegistros(event: any = 1){
    let page = event;
    if(this.verificaNullUndefined(this.filtroPlaca)){
      this.obterCondutoresPorPlaca(page);
    }
    else if(this.verificaNullUndefined(this.filtroNome)){
      this.obterTodosCondutores(page, 8, this.filtroNome);
    }
    else{
      this.obterTodosCondutores(page);
    }        
  }

  private semCondutores(){
    this.paginacao.pageIndex = 0;
    this.dados.list = [];
  }
  
  private verificaNullUndefined(dado: string): boolean{
    if(dado !== undefined && dado !== '') return true;
    else return false
  }

  private variaveisPaginacao(dados: ListaDados<Condutor>){    
    this.paginacao.pageIndex = dados.pageIndex;
    this.paginacao.pageSize = dados.pageSize;
    this.paginacao.totalResults = dados.totalResults;
  }

  private paginacaoVeiculos(dados: ListaDados<Veiculo>){    
    this.paginacaoVeiculo.pageIndex = dados.pageIndex;
    this.paginacaoVeiculo.pageSize = dados.pageSize;
    this.paginacaoVeiculo.totalResults = dados.totalResults;
  }

  public cadastrarCondutor(){
    this.router.navigate(['cadastrar'], { relativeTo: this.route});
  }

  public editarCondutor(id: string){
    this.router.navigate(['editar', id], { relativeTo: this.route});
  }

  abrirExclusao(id: string): void{
    this.paginacaoVeiculo.pageIndex = 0;
    this.dados.list.forEach(x => {
      if(x.id == id) {
        this.preencherDadoCondutorSelecionado(x);
        this.obterVeiculosPorCpf(x.cpf, 1);
      }
    });    
    this.mdExcluir.show();
  }

  preencherDadoCondutorSelecionado(x: Condutor){
    this.condutorSelecionado = new Condutor();
    this.condutorSelecionado.id = x.id;
    this.condutorSelecionado.primeiroNome = x.primeiroNome;
    this.condutorSelecionado.ultimoNome = x.ultimoNome;
    this.condutorSelecionado.cpf = x.cpf;
    this.condutorSelecionado.telefone = x.telefone;
    this.condutorSelecionado.email = x.email;
    this.condutorSelecionado.cnh = x.cnh;
    this.condutorSelecionado.dataNascimento = this.converterDataNascimento(x.dataNascimento);
    this.condutorSelecionado.veiculos = x.veiculos === null ? [] : x.veiculos;
  }

  public converterDataNascimento(data: string): string{
    return data.substring(0, 10);    
  }

  excluirCondutor(){    
    this.condutorService.excluirCondutor(this.condutorSelecionado.id)
      .subscribe(response => {
        if (response){
          this.mdExcluir.hide();
          setTimeout(() => {document.location.reload();}, 3000);
          this.toastr.success('Condutor excluído com sucesso!');          
        }
        else{
          this.toastr.error('Erro ao excluir condutor');
        }
      })
  }

  abrirVeiculos(cpf: string, page: number = 1, take: number = 4){
    this.veiculos = new ListaDados<Veiculo>();
    this.paginacaoVeiculo.pageIndex = 0;
    this.cpfSelecionado = cpf;
    this.obterVeiculosPorCpf(cpf, page, take);
    this.mdVeiculos.show();
  }

  private obterVeiculosPorCpf(cpf: string, page: number, take: number = 4){
    this.veiculoService.obterVeiculosPorCpf(page, take, cpf)
      .subscribe(response => {
        if (response){
          this.veiculos = response;
          this.paginacaoVeiculos(this.veiculos);
        }
      });
  }

  public carregarVeiculos(event: any = 1){
    let page = event;
    this.obterVeiculosPorCpf(this.cpfSelecionado, page);
  }
}
