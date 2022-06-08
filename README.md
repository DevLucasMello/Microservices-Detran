# Aplicação Distribuída em APIs
 
## Tecnologias / Componentes implementados

    - .Net 5;
    - WebApi;
    - SOLID;
    - Clean Code;
    - Design Patterns;
    - DDD;
    - CQRS;
    - Arquitetura Hexagonal;
    - SqlServer;
    - EntityFrameworkCore 5;
    - Dapper;
    - Automapper;
    - Identity;
    - JWT;
    - Angular 11;
    - Docker;
    - Testes de Unidade;
    - Testes de Integração;
    - Bogus;
    - Moq;
    - Automocker;
    - FluentValidator;
    - MediatR;
    - Swagger UI with JWT support;
    - RabbitMq;
    - EasyNetQ;

## Recursos para rodar a aplicação

   - Docker instalado com conta ativa
   - Interface gráfica SqlServer


## Instruções de uso

   - 1° - Baixar projeto do GitHub
   - 2° - Criar uma pasta com nome "dev" no diretório C:
   - 3° - Extrair o projeto baixado para pasta "dev"
   - 4° - No cmd do windows digitar o seguinte comando para navegar até o diretório "cd c:/dev/Microservices-Detran/docker"
   - 5° - Ainda no cmd, digitar o comando para criar imagem e rodar os containers docker "docker-compose -f tp_producao.yml up --build"
   - 6° - Abrir interface gráfica do SqlServer e verificar se os bancos de dados "TesteCondutor" "TesteVeiculo" "TesteIdentidade" foram criados
conforme credenciais: host -> localhost, 1433 user -> sa password -> Lm@518792

### Passo 7º apenas se os bancos mencionados acima não foram criados
   - 7° - Abra a interface do docker e clique em "restart" no container "tp-sql-server"

   - 8° - Abrir o navegador de sua preferência e digite a url "localhost:8080" para carregar a aplicação.
