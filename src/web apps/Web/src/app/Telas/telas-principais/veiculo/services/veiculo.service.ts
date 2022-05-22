import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/services/base.service';
import { catchError } from "rxjs/operators";
import { ListaDados } from "src/app/Models/lista-dados";
import { Veiculo } from "../models/veiculo";

@Injectable()
export class VeiculoService extends BaseService {

    constructor(private http: HttpClient) { super() }
    
    obterTodosVeiculos(): Observable<ListaDados<Veiculo[]>> {
        return this.http
            .get<ListaDados<Veiculo[]>>(this.UrlServiceDetran + "veiculo", super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }
}