import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/servicos/base.service';
import { catchError } from "rxjs/operators";
import { ListaDados } from "src/app/modelos/lista-dados";
import { Veiculo } from "../models/veiculo";

@Injectable()
export class VeiculoService extends BaseService {

    constructor(private http: HttpClient) { super() }    
    
    obterTodosVeiculos(page: number, take: number): Observable<ListaDados<Veiculo>> {        
        return this.http
            .get<ListaDados<Veiculo>>(`${this.UrlServiceDetran}veiculo?ps=${take}&page=${page}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterVeiculosPorCpf(page: number, take: number, cpf: string): Observable<ListaDados<Veiculo>> {        
        return this.http
            .get<ListaDados<Veiculo>>(`${this.UrlServiceDetran}veiculo/documento?ps=${take}&page=${page}&cpf=${cpf}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }
}