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
    
    obterTodosVeiculos(page: number, take: number, query: string): Observable<ListaDados<Veiculo>> {        
        return this.http
            .get<ListaDados<Veiculo>>(`${this.UrlServiceDetran}veiculo?ps=${take}&page=${page}&q=${query}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterVeiculosPorCpf(page: number, take: number, cpf: string): Observable<ListaDados<Veiculo>> {        
        return this.http
            .get<ListaDados<Veiculo>>(`${this.UrlServiceDetran}veiculo/documento?ps=${take}&page=${page}&cpf=${cpf}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterPorId(id: string): Observable<Veiculo> {
        return this.http
            .get<Veiculo>(`${this.UrlServiceDetran}veiculo/${id}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    cadastrarVeiculo(veiculo: Veiculo): Observable<any> {
        return this.http.post<any>(`${this.UrlServiceDetran}veiculo`,veiculo, super.ObterAuthHeaderJson());
    }

    atualizarVeiculo(veiculo: Veiculo, id: string): Observable<any> {        
        return this.http.put<any>(`${this.UrlServiceDetran}veiculo/${id}`,veiculo, super.ObterAuthHeaderJson());
    }

    excluirVeiculo(id: string): Observable<any> {        
        return this.http.delete<any>(`${this.UrlServiceDetran}veiculo/${id}`, super.ObterAuthHeaderJson());
    }
}