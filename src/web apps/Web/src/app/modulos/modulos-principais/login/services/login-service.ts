import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/servicos/base.service';
import { UsuarioRespostaLogin } from "../models/login-resposta";
import { UsuarioLogin, UsuarioRegistro } from "../models/login-envio";

@Injectable()
export class LoginService extends BaseService {

    constructor(private http: HttpClient) { super() }
    
    login(usuario: UsuarioLogin): Observable<UsuarioRespostaLogin> {
        return this.http.post<UsuarioRespostaLogin>(`${this.UrlServiceDetran}autenticar`,usuario, super.ObterHeaderJson());
    }

    registro(usuario: UsuarioRegistro): Observable<UsuarioRespostaLogin> {
        return this.http.post<UsuarioRespostaLogin>(`${this.UrlServiceDetran}nova-conta`,usuario, super.ObterHeaderJson());
    }    
}