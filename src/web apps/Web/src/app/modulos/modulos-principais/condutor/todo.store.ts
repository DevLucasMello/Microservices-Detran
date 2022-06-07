import { BehaviorSubject, Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Paginacao } from 'src/app/modelos/paginacao';

export interface State {
    paginacao: Paginacao
}

const state: State = {
    paginacao: new Paginacao()
};

export class Store {
    private subject = new BehaviorSubject<State>(state);
    private store = this.subject.asObservable();

    get value() {
        return this.subject.value;
    }

    public getPaginacao(): Observable<Paginacao> {
        return this.store
            .pipe(map(store => store.paginacao));
    }

    set(name: string, state: any) {
        this.subject.next({
            ...this.value, [name]: state
        });
    }
}
