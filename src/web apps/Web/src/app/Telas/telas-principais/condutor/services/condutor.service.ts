import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/services/base.service';
import { catchError } from "rxjs/operators";
import { Condutor } from "../models/condutor";
import { ListaDados } from "src/app/Models/lista-dados";

@Injectable()
export class CondutorService extends BaseService {

    constructor(private http: HttpClient) { super() }
    
    obterTodosCondutores(): Observable<ListaDados<Condutor[]>> {
        return this.http
            .get<ListaDados<Condutor[]>>(this.UrlServiceDetran + "condutor", super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }
}