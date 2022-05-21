export class LocalStorageUtils{

    public salvarDadosLocais(token: string, dados: string) {
        this.salvarToken(token);
        this.salvarUsuario(dados);
    }

    public salvarUsuario(dados: string) {
        localStorage.setItem('web.usuario', dados);
    }

    public salvarToken(token: string) {
        localStorage.setItem('web.token', token);
    }
    
    public obterUsuario() {
        return JSON.parse(localStorage.getItem('web.usuario'));
    }

    public obterToken(): string {
        return localStorage.getItem('web.token');
    }

    public limparDadosLocais() {
        localStorage.removeItem('web.token');
        localStorage.removeItem('web.usuario');
    }
}
