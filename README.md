# 🚀 Diferenciais e Decisões Técnicas
Além de cumprir todos os requisitos funcionais solicitados para o fluxo do CRUD, utilizei este projeto para demonstrar aplicações reais de arquitetura, escalabilidade e experiência do usuário (UX). Abaixo listo as implementações que foram feitas além do escopo básico:

## 🌐 Aplicação em Produção (Live Demo)
- Para facilitar a avaliação, o projeto foi conteinerizado e publicado na nuvem. Você pode testar o sistema completo sem precisar clonar ou configurar o ambiente localmente:

- Front-end (Vercel): [Acessar a Aplicação](https://mobiis-full-stack-project.vercel.app/regioes)
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/e742fda7-0a92-4208-b7ee-3cb650efb57b" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/386b4641-a030-4d9d-a74d-942478fe1313" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/3f23bf0e-750e-4e74-a686-a0cc33dce401" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/2fb102bd-269e-43b8-afb7-d68303753f9b" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/7a722027-90a0-494a-8136-40cc64ed0e71" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/ba6fe7b4-4a11-4d52-af10-ca749fa8b116" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/77ecc00c-b19d-46be-9c8f-c511bfa3e016" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/6158087b-f620-44b0-8043-d5c495973f2e" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/0dd7cd14-6cbe-441e-b7b8-dc1a772bb7ff" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/17806681-37c0-46f8-8819-d55499ed5e98" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/e70004d9-0a2b-41ad-b40a-f8dd21ddfd72" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/73fc889c-6591-46ae-a7d1-71618d183b81" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/9309d304-483d-4820-b4a1-ff4c61ca5a34" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/21dcac0e-50ef-4a46-ac38-273a26c27493" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/1ae19bda-67cc-4c7a-b597-f56c2ea20cda" />

- Back-end (Render): [Acessar a API] (https://mobiis-fullstack-project-backend.onrender.com/api) (Swagger indisponível em prod, rota base ativa)

<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/246aa48d-ca51-4720-8360-4dc97c16dc0f" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/cdf8a3aa-b0e0-4341-b720-b30eac446a84" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/f57ff9ab-2bdb-468c-a046-706532edfb84" />
<img width="3840" height="2076" alt="image" src="https://github.com/user-attachments/assets/9d8ba4f1-8591-4d69-8036-4894acaa3393" />


## 🏛️ Arquitetura Back-end (.NET Core 3.1)
- Camada de AppService: O README original sugeriu que a camada de Aplicação havia sido abstraída para simplificar. Decidi implementar os AppServices para demonstrar o controle adequado do fluxo e a orquestração entre a API e o Domínio, mantendo os Controllers extremamente enxutos.

- Generics (BaseService e BaseRepository): Para evitar repetição de código (DRY) no CRUD e preparar o sistema para o crescimento de novas entidades, construí as classes base utilizando Generics.

- Injeção de Dependência Modular: Isolei a configuração da injeção de dependências (DI) utilizando métodos de extensão (Extension Methods), o que deixa a classe Startup muito mais limpa e focada.

- Message Pattern: Implementei um padrão de mensagens no domínio para capturar validações de negócio e devolvê-las estruturadas para o front-end, evitando o uso de exceções genéricas para controle de fluxo.

- Sistema de Exclusão (Delete): O escopo original pedia apenas a ativação/inativação de status. Implementei também a deleção física/lógica com tratamento de integridade referencial.

## 🔌 Integração de Dados Externos
- Consumo da API do IBGE: Como diferencial sugerido no escopo sobre consumo de APIs externas via HttpClient, optei por integrar o sistema diretamente à API pública do IBGE. Isso garante que os selects de Estado e Cidade sejam populados com dados reais do Brasil de forma dinâmica, em vez de depender de um banco estático mockado.

## 💻 Front-end e UX (Angular 10)

- Paginação Customizada: Otimizei a visualização da listagem de regiões criando um sistema dinâmico de paginação no front-end.
- 
- Notificações não-obstrutivas (Toasts): Substituí os limitados alert() nativos do navegador pelo ngx-toastr, proporcionando um feedback visual de sucesso/erro profissional e elegante no canto da tela.

- Modais e Confirmações Seguras: Para evitar a quebra de usabilidade com o confirm() nativo, implementei a biblioteca SweetAlert2 para interceptar ações críticas (como a exclusão), garantindo uma experiência contínua.

- Responsividade e Controle de Layout: O sistema foi pensado para Mobile e Desktop. Os formulários foram desenhados com modais de altura fixa e controle de overflow, garantindo que a inserção de muitas cidades ou a exibição de mensagens de erro reativas não quebrem o layout da página.

## ☁️ DevOps e Infraestrutura
- Docker: Escrevi um Dockerfile otimizado para a arquitetura DDD em .NET Core 3.1, garantindo que a aplicação seja isolada e possa rodar em qualquer ambiente moderno (utilizado para o deploy no Render).

- CI/CD e Resolução de Ambiente: Configurei o deploy do Angular na Vercel, solucionando conflitos de compatibilidade de versões legadas do Node.js utilizando variáveis de ambiente de provedor OpenSSL.

## 🧪 Nota sobre Testes Unitários:
- Visando otimizar o tempo hábil estabelecido para a entrega, tomei a decisão técnica de excluir os arquivos .spec.ts (testes unitários do Angular). O foco foi garantir a entrega de uma arquitetura sólida (DDD), integrações externas funcionais e uma UI/UX avançada. Contudo, devido à forte segregação de interfaces aplicada no projeto, o terreno já está 100% preparado para a injeção de mocks em implementações futuras de testes unitários.

________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

# Fretefy | FullStack

Bem vindo,

Se você chegou até aqui é porque queremos conhecer um pouco mais sobre as suas habilidades no desenvolvimento back-end, para isso preparamos um projeto onde você terá que desenvolver um CRUD básico.

Caso você tenha alguma dúvida, pode esclarece-las por email, responderei o mais breve possível: christian.saddock@fretefy.com.br

Esperamos que você faça tudo o que o projeto especifica, mas se você não conhecer alguma tecnologia mesmo que seja o front ou back inteiro, ainda faça aquilo que você domina.

Boa sorte!

# Como começar?

1. Faça o fork do projeto `https://github.com/christiansaddock/Fretefy_FullStack`
2. Faça sua implementação
3. Suba seu código no fork criado
4. Nos avise sobre a finalização da implementação, preferencialmente por email: christian.saddock@fretefy.com.br 🚀

# Atividade

Implementar um cadastro básico de regiões, basicamente um formulário composto por um nome e as cidade/uf que compoem aquela região.
Implementar uma forma de exportar a listagem do cadastro de regiões, preferencialmente em excel.

### Campos Requeridos
- Nome
- Cidades
    - Cidade
    - UF

### Requisitos
- O campo nome é obrigatório
- Não deve permitir cadastrar duas regiões com o mesmo nome
- É obrigatório informar ao menos uma cidade na região
- Não pode ser informada a mesma cidade duas ou mais vezes
- Uma região pode ser desativada/ativada
- O campo de cidade/uf deve ser um seletor (combobox)
- Poder exportar os dados 

# 1. Atividades Front-End

O front-end deve ser desenvolvido em Angular, seguindo os conceitos do framework, na pasta front-end tem uma estrutura básica já com o módulo `regiao` pronto para você começar.

Cada operação irá listar nos requisitos técnicos alguns recursos que devem ser utilizados.

O fluxo das telas é livre, mas deve obrigatóriamente utilizar Angular Routes.

Você pode fazer um mock das operações caso não implemente o back, mas se implementar o backend, deve fazer a comunicação completa.

Não a necessidade que a interface siga um design específico, o importante é ter navegação e ser um formulário, utilizando os componentes nativos do HTML está valendo.

No front deve conter as seguintes operações:

1. Listagem de Regiões
2. Cadastro de Regiões
3. Edição de Regiões
4. Componente de Seletor de Cidade

## 1.1 Listagem de Regiões

Na listagem de regiões devem ser listadas todas as regiões cadastradas e conter ações específicas


![Cadastro de Regiões](assets/referencia_listagem.png)
> Imagem de referência para a listagem

### **Requisitos**

- As regiões devem ser listadas em forma de tabela (table)
- Cada região deve conter uma coluna que identifique se ela está ativa ou inativa
- Cada região deve conter uma ação para ativar ou desativar a região, devendo apresentar apenas a ação que modifique o estado atual. Se ela estiver ativa, deve haver uma ação desativar e vice-versa.
- Cada região deve conter uma ação para editar a região
- Na listagem deve haver em algum local uma ação que permita cadastrar uma nova região

### **Requisitos Técnicos**

- Preferencialmente deve utilizar `rxjs` com o pipe `async` na listagem de regiões
- As ações deverão preferencialmente ser realizadas via `routerLink`
- As chamadas para API devem obrigatóriamente passar por um service

## 1.2 Cadastro de Regiões

No cadastro você deve permitir que o usuário realize o cadastro de uma região, contendo os campos requeridos.


![Cadastro de Regiões](assets/referencia_cadastro.png)
> Imagem de referência para o cadastro

### **Requisitos**
- O campo nome é obrigatório
- Não permitir cadastrar duas regiões com o mesmo nome
- É obrigatório informar ao menos uma cidade na região
- Não pode ser informada a mesma cidade duas ou mais vezes
- O campo de cidade/uf deve ser um seletor (combobox)
- Conter uma ação para salvar
- Conter uma ação para cancelar

### **Requisitos Técnicos**

- Preferencialmente faça o formulário utilizando ReactiveForms, esperamos ver `FormGroup` para o formulário geral, `FormControl` para os campos e um `FormArray` para as cidades.
- As validações devem, preferencialmente ser feitas com os `Validators` do Angular.
- As ações deverão passar por um service, assim como na listagem.

## 1.3 Edição de Região

Na edição você deve permitir que o usuário edite um cadastro, para isso você preferencialmente deve utilizar o mesmo componente de cadastro variando apenas a rota.

## 1.4 Componente Seletor de Cidade

Implementar um componente Angular que represente o seletor de cidade

### **Requisitos**

- Listar todas as cidades no formato de `select`
- Refletir a cidade selecionada 

### **Requisitos Técnicos**

- O componente deverá ser autonomo devendo saber listar e refletir a cidade selecionada
- As ações deverão passar por um service
- Preferencialmente trabalhar com FormControl

# 2. Atividades Back-End

O back-end deve ser desenvolvido em ASP.Net Core 3.1 com uma WebApi REST e uma estruturação do projeto no formato do DDD. A persistência dos dados deve ser atraves do Entity Framework Core, no modelo Code First e utilizando Migrations.

Na pasta back-end já tem uma estrutura básica do projeto para começar, ele já está prepado para seguir os conceito de DDD, incluindo um exemplo.

Como utilizamos Entity para este projeto vamos utitilizar o SQLite para facilitar.

## Requisitos
- Implementar uma entidade região que contenha o nome e as cidades que compoem a região.
- A entidade Região deverão ser persistida em duas tabelas Regiao e RegiaoCidade em uma relação `1..N`.
- Implementar um RegiaoController que contenhas as operações de acordo com o verbo HTTP correspondente (`GET, POST, PUT`) que deverão chamar as respectivas ações do RegiaoService.
- Implementar um RegiaoService que contenha as operações do CRUD (`List, Create, Update`) que deverão chamar as respectivas ações do RegiaoRepository
- Implementar um Repository que contenham as operações de do CRUD (`List, Create, Update`) que deverão chamar as respectivas ações no Entity Framework
- Service e Repository deverão ser instanciados via Dependecy Injection no lifetime apropriado 
- Service e Repository deverão ter cada uma sua respectiva interface para uso e registro no Dependency Injection
- Poder exportar os dados através de um endpoint específico

## Observações
1. Caso não esteja habituado com o formato DDD procure referencia nos exemplos ou faça da forma que você julgar melhor (Priorizamos o formato DDD na avaliação).
2. Fique a vontade para incluir mais operações que julgar necessário, mesmo que elas não estejam nos requisitos.
3. Para simplificar abstraimos o AppService do DDD, caso queira implementar, será um diferencial.
4. Quer fazer algo a mais? Seria um diferencial implementar por exemplo uma busca dos dados de Latitude e Longitude da cidade cadastrada pelas APIs do google ou do mapbox, buscando a chave para esse consumo do appsettings #FicaDica 😉

## Dicas
1. O CORS necessita ser configurado no back para que se comunique corretamente com o front 😉
2. Acha que pode melhorar alguma coisa que está implementada, vá em frente 😎
3. Tem algum conhecimento extra que gostaria de demonstrar, a hora é agora 🏆
