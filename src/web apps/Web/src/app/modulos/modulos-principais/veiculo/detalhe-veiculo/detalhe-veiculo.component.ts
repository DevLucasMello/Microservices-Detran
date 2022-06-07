import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, merge, Observable } from 'rxjs';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/util/generic-form-validator';
import { MASKS, NgBrazilValidators } from 'ng-brazil';
import { Veiculo } from '../models/veiculo';
import { VeiculoService } from '../services/veiculo.service';
import { CondutorService } from '../../condutor/services/condutor.service';

@Component({
  selector: 'app-detalhe-veiculo',
  templateUrl: './detalhe-veiculo.component.html',
  styleUrls: ['./detalhe-veiculo.component.css']
})
export class DetalheVeiculoComponent implements OnInit, AfterViewInit {

  public idVeiculo: string = '';
  public novoCadastro: boolean = true;
  public veiculo: Veiculo;

  public MASKS = MASKS;

  @ViewChildren(FormControlName, {read: ElementRef}) forInputElements: ElementRef[];

  cadastroVeiculoForm: FormGroup; 
  
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private veiculoService: VeiculoService,
    private condutorService: CondutorService,
    private toastr: ToastrService
    ) {
      this.validationMessages = {
        cpf: {
          required: 'Informe o Cpf',
          cpf: 'CPF em formato inválido',
        },
        placa: {
          required: 'A placa é requerida',
          minlength: 'A placa precisa ter no mínimo 7 caracteres',
          maxlength: 'A placa precisa ter no máximo 7 caracteres'
        },
        modelo: {
          required: 'O modelo é requerido',
          minlength: 'O modelo precisa ter no mínimo 2 caracteres',
          maxlength: 'O modelo precisa ter no máximo 150 caracteres'
        },                
        marca: {
          required: 'A marca é requerida',
          minlength: 'A marca precisa ter no mínimo 2 caracteres',
          maxlength: 'A marca precisa ter no máximo 150 caracteres'
        },
        cor: {
          required: 'A cor é requerida',
          minlength: 'A cor precisa ter no mínimo 2 caracteres',
          maxlength: 'A cor precisa ter no máximo 150 caracteres'
        },
        anoFabricacao: {
          required: 'O ano de fabricação é requerida',
          minlength: 'Ano de fabricação precisa ter no mínimo 4 caracteres',
          maxlength: 'Ano de fabricação precisa ter no máximo 4 caracteres'
        }
      };
      this.genericValidator = new GenericValidator(this.validationMessages);    
    }

  ngOnInit() {
    this.idVeiculo = this.route.snapshot.params['id'];

    if(this.idVeiculo !== '' && this.idVeiculo !== undefined){
      this.obterPorId(this.idVeiculo);      
      this.novoCadastro = false;
    }

    this.cadastroVeiculoForm = this.fb.group({
      cpf: ['', [Validators.required, NgBrazilValidators.cpf]],
      placa: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(7)]],
      modelo: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      marca: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      cor: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      anoFabricacao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(4)]]
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.forInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.cadastroVeiculoForm);
    });
  }

  voltarDashboard(){
    this.router.navigate(['veiculo']);
  }

  preencherForm(veiculo: Veiculo) {
    this.cadastroVeiculoForm.patchValue({
      cpf: veiculo.condutores[0].cpf,      
      placa: veiculo.placa,
      modelo: veiculo.modelo,
      marca: veiculo.marca,
      cor: veiculo.cor,
      anoFabricacao: veiculo.anoFabricacao
    });
    this.cadastroVeiculoForm.get('cpf').disable();
  }

  public obterPorId(id: string){
    this.veiculoService.obterPorId(id)
      .subscribe(response => {
        if (response){
          this.veiculo = response;          
          this.preencherForm(this.veiculo);
        }
      })
  }

  public obterPorCpf(cpf: string){
    this.condutorService.obterPorCpf(cpf)
      .subscribe(response => {
        if (response){
          this.veiculo.condutorId = response.id;
          this.veiculo.cpf = response.cpf;
          this.cadastrarVeiculo(this.veiculo);
        }
        else{
          this.toastr.error('Erro ao obter condutor cadastrado');
        }
      })
  }

  public salvarVeiculo(){
    let cpf = this.cadastroVeiculoForm.get('cpf').value.replace(".", "").replace(".", "").replace("-", "");    

    if(this.cadastroVeiculoForm.dirty && this.cadastroVeiculoForm.valid){
      this.veiculo = Object.assign({}, this.veiculo, this.cadastroVeiculoForm.value);
      if (this.novoCadastro){
        this.obterPorCpf(cpf);
      }
      else{ this.atualizarVeiculo(this.veiculo); }
    }
  }

  public cadastrarVeiculo(veiculo: Veiculo){
    this.veiculoService.cadastrarVeiculo(veiculo)
      .subscribe(response => {
        if (response){                    
          this.voltarDashboard();
          this.toastr.success('Veículo cadastrado com sucesso!');
        }else{
          this.toastr.error('Erro ao cadastrar veículo');
        }
      })
  }

  public atualizarVeiculo(veiculo: Veiculo){
    this.veiculoService.atualizarVeiculo(veiculo, this.idVeiculo)
      .subscribe(response => {
        if (response){
          this.voltarDashboard();
          this.toastr.success('Veículo alterado com sucesso!');
        }else{
          this.toastr.error('Erro ao atualizar veículo');
        }
      })
  }

}
