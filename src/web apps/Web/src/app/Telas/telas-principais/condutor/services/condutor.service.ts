import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/services/base.service';
import { catchError } from "rxjs/operators";
import { Condutor } from "../models/condutor";
import { ListaDados } from "src/app/Models/lista-dados";

@Injectable()
export class CondutorService extends BaseService {

    constructor(private http: HttpClient) { super() }
    
    obterTodosCondutores(page: number, take: number): Observable<ListaDados<Condutor[]>> {        
        return this.http
            .get<ListaDados<Condutor[]>>(`${this.UrlServiceDetran}condutor?ps=${take}&page=${page}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterCondutoresPorPlaca(page: number, take: number, placa: string): Observable<ListaDados<Condutor[]>> {        
        return this.http
            .get<ListaDados<Condutor[]>>(`${this.UrlServiceDetran}condutor/placa?ps=${take}&page=${page}&placa=${placa}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }
}