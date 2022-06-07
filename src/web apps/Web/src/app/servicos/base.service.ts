import { HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";
import { environment } from 'src/environments/environment';
import { ListaDados } from "../modelos/lista-dados";
import { Paginacao } from "../modelos/paginacao";
import { LocalStorageUtils } from "../util/localStorage";

export abstract class BaseService {

    public localStorage: LocalStorageUtils = new LocalStorageUtils();
    
    protected UrlServiceDetran: string = environment.apiDetran;

    protected ObterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected ObterAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.localStorage.obterToken()}`
            })
        };
    }    

    // protected extractData(response: any) {
    //     return response.data || {};
    // }

    protected serviceError(response: Response | any) {
        let customError: string[] = [];

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }
        }

        console.error(response);
        return throwError(response);
    }

    protected dadosPaginacao(dados: ListaDados<any>): Paginacao{
        let paginacao = new Paginacao();
        if(dados){
            paginacao.pageIndex = dados.pageIndex;
            paginacao.pageSize = dados.pageSize;
            paginacao.totalResults = dados.totalResults;
        }

        return paginacao;
    }
}
