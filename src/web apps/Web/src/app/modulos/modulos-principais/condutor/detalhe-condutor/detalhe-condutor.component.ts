import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, merge, Observable } from 'rxjs';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/Validacoes/generic-form-validator';
import { Condutor } from '../models/condutor';
import { MASKS, NgBrazilValidators } from 'ng-brazil';
import { CondutorService } from '../services/condutor.service';

@Component({
  selector: 'app-detalhe-condutor',
  templateUrl: './detalhe-condutor.component.html',
  styleUrls: ['./detalhe-condutor.component.css']
})
export class DetalheCondutorComponent implements OnInit, AfterViewInit {

  public idCondutor: string = '';
  public novoCadastro: boolean = true;
  public condutor: Condutor;

  public MASKS = MASKS;

  @ViewChildren(FormControlName, {read: ElementRef}) forInputElements: ElementRef[];

  cadastroCondutorForm: FormGroup; 
  
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private condutorService: CondutorService,
    private toastr: ToastrService
    ) {
      this.validationMessages = {
        primeiroNome: {
          required: 'O primeiro nome é requerido',
          minlength: 'O primeiro nome precisa ter no mínimo 2 caracteres',
          maxlength: 'O primeiro nome precisa ter no máximo 150 caracteres'
        },
        ultimoNome: {
          required: 'O ultimo nome é requerido',
          minlength: 'O ultimo nome precisa ter no mínimo 2 caracteres',
          maxlength: 'O ultimo nome precisa ter no máximo 150 caracteres'
        },
        cpf: {
          required: 'Informe o Cpf',
          cpf: 'CPF em formato inválido',
        },
        telefone: {
          required: 'Informe o Telefone',
          telefone: 'Telefone em formato inválido',
        },        
        email: {
          required: 'O email é requerido',
          minlength: 'O email precisa ter no mínimo 8 caracteres',
          maxlength: 'O email precisa ter no máximo 150 caracteres'
        },
        cnh: {
          required: 'A CNH é requerida',
          minlength: 'A CNH precisa ter no mínimo 11 caracteres',
          maxlength: 'A CNH precisa ter no máximo 11 caracteres'
        },
        dataNascimento: {
          required: 'A data de nascimento é requerida',
          date: 'Data de nascimento em formato inválido',
          minlength: 'Data de nascimento em formato inválido',
          maxlength: 'Data de nascimento em formato inválido'
        }
      };
      this.genericValidator = new GenericValidator(this.validationMessages);
    
    }

  ngOnInit() {
    this.idCondutor = this.route.snapshot.params['id'];

    if(this.idCondutor !== '' && this.idCondutor !== undefined){
      this.obterPorId(this.idCondutor);      
      this.novoCadastro = false;
    }

    this.cadastroCondutorForm = this.fb.group({
      primeiroNome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      ultimoNome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      cpf: ['', [Validators.required, NgBrazilValidators.cpf]],
      telefone: ['', [Validators.required, NgBrazilValidators.telefone]],      
      email: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(150)]],
      cnh: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      dataNascimento: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]]      
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.forInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.cadastroCondutorForm);
    });
  }

  voltarDashboard(){
    this.router.navigate(['condutor']);
  }

  preencherForm(condutor: Condutor) {
    this.cadastroCondutorForm.patchValue({      
      primeiroNome: condutor.primeiroNome,
      ultimoNome: condutor.ultimoNome,
      cpf: condutor.cpf,
      telefone: condutor.telefone,
      email: condutor.email,
      cnh: condutor.cnh,
      dataNascimento: condutor.dataNascimento
    });
  }

  public converterDataNascimento(data: string): string{
    return data.substring(0, 10);    
  }

  formatarDataNascimento(data: string): string{
    let dia = parseInt(data.substring(0,2));
    let mes = parseInt(data.substring(3,5));
    let ano = parseInt(data.substring(6,10));
    return ano + "-" + mes + "-" + dia;
  }

  public obterPorId(id: string){
    this.condutorService.obterPorId(id)
      .subscribe(response => {
        if (response){
          this.condutor = response;
          this.condutor.dataNascimento = this.converterDataNascimento(this.condutor.dataNascimento);          
          this.preencherForm(this.condutor);
        }
      })
  }

  public salvarCondutor(){
    if(this.cadastroCondutorForm.dirty && this.cadastroCondutorForm.valid){
      this.condutor = Object.assign({}, this.condutor, this.cadastroCondutorForm.value);
      this.condutor.dataNascimento = this.formatarDataNascimento(this.condutor.dataNascimento)
      if (this.novoCadastro){        
        this.cadastrarCondutor(this.condutor); 
      }
      else{ this.atualizarCondutor(this.condutor); }
    }
  }

  public cadastrarCondutor(condutor: Condutor){
    this.condutorService.cadastrarCondutor(condutor)
      .subscribe(response => {
        if (response){                    
          this.voltarDashboard();
          this.toastr.success('Condutor cadastrado com sucesso!');
        }else{
          this.toastr.error('Erro ao cadastrar condutor');
        }
      })
  }

  public atualizarCondutor(condutor: Condutor){
    this.condutorService.atualizarCondutor(condutor, this.idCondutor)
      .subscribe(response => {
        if (response){
          this.voltarDashboard();
          this.toastr.success('Condutor alterado com sucesso!');
        }else{
          this.toastr.error('Erro ao atualizar condutor');
        }
      })
  }
}
