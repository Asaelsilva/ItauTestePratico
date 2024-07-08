# API para validação de senha

## Instruções para executar a API
Para executar basta entrar na raiz da pasta src onde se encontra o arquivo .csproj e executar o comando:

```bash
# Comando para executar a aplicação
dotnet watch run 
```

A Api swagger será exposta no endereço http://localhost:5228/swagger/index.html

Caso queira executar via postman basta acessar http://localhost:5228/login/validar-senha enviando:
{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"senha": "string"
}

### Sobre a API

Escolhi trabalhar com a estrutura folder by feature, onde concentro tudo o que é daquela funcionalidade em um mesmo local. Para nível de organização, fica mais simples e tudo em um mesmo lugar.
Pra ter um código coerente, criei um model login que tem a a propriedade senha. 
Meus arquivos de configuração e extensões se encontram dentro de submodule/infraentructure

### Solução para o desafio

Eu como desenvolvedor do Back-end, pensei em como devolver o erro para o front-end de maneira que se a senha estiver errado não retorne um false, mas sim o motivo do erro. Coso tudo ocorra bem, retorne true.

Para resolver o desafio, decidi usar o poder do Regex par validar erros como: 
- Deve ter nove ou mais caracteres
- Ao menos 1 dígito
- Ao menos 1 letra minúscula
- Ao menos 1 letra maiúscula
- Ao menos 1 caractere especial

A escolha do Regex foi pelo fato de consegui concentrar muitas regras em um unico ponto, assim evitando certos tipos de complexidade.

Para a regra: 
- Não possuir caracteres repetidos dentro do conjunto

Decidi usar o Distinct do linq pelo simples fato de não ter que criar um loop para validar carácter por carácter, ou ter que criar um regex mapeando carácter por carácter. A ideia é evitar complexidade desnecessariamente.

Estou utilizando a validação manual do fluentValidation para realizar as validações, deixando assim o código muito mais clean e legível.

## Testes
Para executar os testes, basta executá-los usando os recursos Run Test, Run All Tests ou executá-los através do Test Explore.