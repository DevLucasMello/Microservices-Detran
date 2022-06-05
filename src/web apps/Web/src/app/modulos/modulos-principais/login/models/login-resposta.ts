export class UsuarioRespostaLogin
{
    public accessToken: string;
    public expiresIn: number
    public usuarioToken: UsuarioToken;
    public responseResult: ResponseResult;
}

class UsuarioToken
{
    public Id: string;
    public Email: string;
    public Claims: UsuarioClaim[] = []
}

class UsuarioClaim
{
    public Value: string;
    public Type: string;
}


class ResponseResult
{
    public title: string;
    public status: number;
    public errors: ResponseErrorMessages;
}

class ResponseErrorMessages
{
    public mensagens: string[] = [];
}