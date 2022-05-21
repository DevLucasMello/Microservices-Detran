import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'ng2-validation';
import { LoginService } from 'src/app/Telas/telas-principais/login/services/login-service';
import { LocalStorageUtils } from 'src/app/Validacoes/localStorage';
import { UsuarioLogin } from '../models/login-envio';
import { UsuarioRespostaLogin } from '../models/login-resposta';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/Validacoes/generic-form-validator';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, AfterViewInit {
  
  public localStorage: LocalStorageUtils = new LocalStorageUtils();
  @ViewChildren(FormControlName, {read: ElementRef}) forInputElements: ElementRef[];  
  loginForm: FormGroup;

  public usuarioLogin: UsuarioLogin;  
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};
  public usuarioRespostaLogin: UsuarioRespostaLogin;
  public errors: string[] = [];
  returnUrl: string;

  constructor(private fb: FormBuilder,    
    private router: Router,
    private loginService: LoginService,
    private toastr: ToastrService,
    private route: ActivatedRoute
    ) {    

    this.validationMessages = {
      email: {
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      senha: {
        required: 'Informe a senha',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres'
      }
    };
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
    if (this.returnUrl) this.returnUrl = this.returnUrl.replace(',','/');  

    this.genericValidator = new GenericValidator(this.validationMessages);    
  }

  ngOnInit(): void {   

    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      senha: ['', [Validators.required, CustomValidators.rangeLength([6, 15])]]      
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.forInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.loginForm);
    });
  }

  login() {
    if (this.loginForm.dirty && this.loginForm.valid) {
      this.usuarioLogin = Object.assign({}, this.usuarioLogin, this.loginForm.value);
      this.loginUsuario(this.usuarioLogin);
    }
  }

  registrar(){
    this.router.navigate(['registro']);
  }

  irDashboard(){
    this.router.navigate(['home']);
  }

  salvarLocalStorage(){
    this.localStorage.salvarDadosLocais(this.usuarioRespostaLogin.accessToken, JSON.stringify(this.usuarioRespostaLogin.usuarioToken));
  }

  public loginUsuario(usuarioLogin: UsuarioLogin){    
    this.loginService.login(usuarioLogin)    
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
    this.loginForm.reset();
    this.errors = [];

    this.usuarioRespostaLogin = response;
    this.salvarLocalStorage();

    let toast = this.toastr.success('Login realizado com Sucesso!', 'Bem vindo!!!');
    if(toast){
      toast.onHidden.subscribe(() => {
        this.returnUrl
        ? this.router.navigate([this.returnUrl])
        : this.irDashboard();
      });
    }
  }

  processarFalha(fail: string[]){
    this.errors = fail;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }
}
