import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'ng2-validation';
import { LocalStorageUtils } from 'src/app/util/localStorage';
import { LoginService } from '../services/login-service';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/util/generic-form-validator';
import { UsuarioRegistro } from '../models/login-envio';
import { UsuarioRespostaLogin } from '../models/login-resposta';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit, AfterViewInit {
  
  public localStorage: LocalStorageUtils = new LocalStorageUtils();

  @ViewChildren(FormControlName, {read: ElementRef}) forInputElements: ElementRef[];
  
  registroForm: FormGroup;

  public usuarioRegistro: UsuarioRegistro;
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};
  public usuarioRespostaLogin: UsuarioRespostaLogin;
  public errors: string[] = [];

  constructor(private fb: FormBuilder,    
    private router: Router,
    private route: ActivatedRoute,
    private loginService: LoginService,
    private toastr: ToastrService
    ) {    

    this.validationMessages = {
      email: {
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      senha: {
        required: 'Informe a senha',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres'
      },
      senhaConfirmacao: {
        required: 'Informe a senha novamente',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };
    this.genericValidator = new GenericValidator(this.validationMessages);    
  }

  ngOnInit(): void {
    
    let senhaUser = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);
    let senhaConfirm = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15]), CustomValidators.equalTo(senhaUser)]);

    this.registroForm = this.fb.group({
      email: ['', [Validators.required]],      
      senha: senhaUser,
      senhaConfirmacao: senhaConfirm
    });    
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.forInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.registroForm);
    });
  }

  registrar() {
    if (this.registroForm.dirty && this.registroForm.valid) {
      this.usuarioRegistro = Object.assign({}, this.usuarioRegistro, this.registroForm.value);
      this.registroUsuario(this.usuarioRegistro);      
    }
  }

  login(){
    this.router.navigate(['/login']);
  }

  irDashboard(){
    this.router.navigate(['/home']);
  }

  salvarLocalStorage(){
    this.localStorage.salvarDadosLocais(this.usuarioRespostaLogin.accessToken, JSON.stringify(this.usuarioRespostaLogin.usuarioToken));
  }

  public registroUsuario(usuarioRegistro: UsuarioRegistro){    
    this.loginService.registro(usuarioRegistro)    
      .subscribe(response => {
        if (response.accessToken){          
          this.processarSucesso(response);
        }
        else{
          this.processarFalha(response.responseResult.errors.mensagens);
        }
      },
      (erro) => {
        console.log(erro.error.message);
      })
  }
  
  processarSucesso(response: UsuarioRespostaLogin) {
    this.registroForm.reset();
    this.errors = [];

    this.usuarioRespostaLogin = response;
    this.salvarLocalStorage();

    let toast = this.toastr.success('Registro realizado com Sucesso!', 'Bem vindo!!!');
    if(toast){
      toast.onHidden.subscribe(() => {
        this.irDashboard();
      });
    }
  }

  processarFalha(fail: string[]){
    this.errors = fail;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }
}
