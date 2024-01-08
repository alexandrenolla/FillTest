# FillTest
 Desafio técnico

## Pré-requisitos

- [dotnet SDK](https://learn.microsoft.com/pt-br/dotnet/core/tools/) instalado.
- [MySql Server](https://www.mysql.com/downloads/) instalado.

- ## Instalação e Execução

1. Execute o MySql para receber nova conexão.

  a) Se estiver no Windows execute o comando:
  ```
  > net start MySQL
  ```
  b) Se estiver no MacOs, execute o comando:
  ```
  $ brew services start mysql
  ```
  c) Se estiver no Linux, execute o comando:
  ```
  $ sudo service mysql start
  ```

2. Clone o repositório e acesse o diretório do aplicativo:

```bash
$ git clone https://github.com/alexandrenolla/FillTest
$ cd app
```

3. Instale as dependências executando o comando:

```
 dotnet build
```

4. Execute o comando:

```
 dotnet run
```

5. Acesse o localhost indicado no terminal.
