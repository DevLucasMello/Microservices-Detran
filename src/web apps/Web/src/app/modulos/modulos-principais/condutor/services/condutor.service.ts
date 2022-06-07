import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/servicos/base.service';
import { catchError } from "rxjs/operators";
import { Condutor } from "../models/condutor";
import { ListaDados } from "src/app/modelos/lista-dados";

@Injectable()
export class CondutorService extends BaseService {

    constructor(private http: HttpClient) { super() }
    
    obterTodosCondutores(page: number, take: number, query: string): Observable<ListaDados<Condutor>> {        
        return this.http
            .get<ListaDados<Condutor>>(`${this.UrlServiceDetran}condutor?ps=${take}&page=${page}&q=${query}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterCondutoresPorPlaca(page: number, take: number, placa: string): Observable<ListaDados<Condutor>> {        
        return this.http
            .get<ListaDados<Condutor>>(`${this.UrlServiceDetran}condutor/placa?ps=${take}&page=${page}&placa=${placa}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterPorId(id: string): Observable<Condutor> {
        return this.http
            .get<Condutor>(`${this.UrlServiceDetran}condutor/${id}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterPorCpf(cpf: string): Observable<Condutor> {
        return this.http
            .get<Condutor>(`${this.UrlServiceDetran}condutor/documento/${cpf}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    cadastrarCondutor(condutor: Condutor): Observable<any> {
        return this.http.post<any>(`${this.UrlServiceDetran}condutor`,condutor, super.ObterAuthHeaderJson());
    }

    atualizarCondutor(condutor: Condutor, id: string): Observable<any> {        
        return this.http.put<any>(`${this.UrlServiceDetran}condutor/${id}`,condutor, super.ObterAuthHeaderJson());
    }

    excluirCondutor(id: string): Observable<any> {        
        return this.http.delete<any>(`${this.UrlServiceDetran}condutor/${id}`, super.ObterAuthHeaderJson());
    }
}