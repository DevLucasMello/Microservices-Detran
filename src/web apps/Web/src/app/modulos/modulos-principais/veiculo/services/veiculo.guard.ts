import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/Validacoes/localStorage';

@Injectable()
export class VeiculoGuard implements CanActivate {

    localStorageUtils = new LocalStorageUtils();

    constructor(protected router: Router) {}

    canActivate(routeAc: ActivatedRouteSnapshot) {
        if(!this.localStorageUtils.obterToken()){
            this.router.navigate(['./login'], { queryParams: { returnUrl: "/" + routeAc.url }});
        }

        return true;
    }
}