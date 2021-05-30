# CadastroExemplo
Exemplo de cadastro

* Para treinamento na criação de testes unitários e de integração.

## Requisitos

* Login
    - O sistema deve permitir o login de usuários cadastrados com senha válida.
    - O sistema deve bloquear o acesso de usuários não cadastrados e/ou com senha inválida.
    - Usuários diferentes do administrador do sistema 'admin', não deve ter acesso ao menu 'Usuários'.
    - Ao clicar em 'Logout' o sistema deve sair da sessão o usuário logado.
    - O usuário logado deve ser exibido no canto superior direito em todas as telas.

* Clientes
    - Esta tela deve exibir o nome 'Início' no canto superior esquerdo.
    - A tela de clientes deve exibir todos os clientes cadastrados e seus telefones.
    - Ao clicar em 'Editar' na lista de clientes o sistema deve direcionar para tela de edição de cliente, com os dados carregados.
    - Ao clicar em 'Remover' na lista de clientes o sistema deve remover o cliente em questão e recarregar a lista.

* Adiçao de clientes
    - Esta tela deve exibir o nome 'Adicionar cliente' no canto superior esquerdo.
    - O sistema deve permitir adicionar um novo cliente.
    - O campo Id deve esta com o valor de '0'
    - Campos obrigatórios:
        - Nome
        - CPF 
            - Deve ser válido
        - Data de nascimento
            - Deve ser uma data válida
            - Dever ser menor que a data atual
            - Deve ser maior que a data atual subtraída de 200 anos.
        - Ocupação
        - telefone
        - Logradouro
        - Bairro
        - Cidade
        - Estado
        - País
    - Caso o campo não seja preenchido o sistema de ve exibir uma mensagem no padrão:
        - "Campo \<nome do campo\> obrigatório"
    - Caso o campo não esteja válido o sistema de ve exibir uma mensagem no padrão:
        - "\<nome do campo\> inválido"
        - "\<nome do campo\> inválida"
    - ao adicionar um cliente com sucesso o sistema deve retornar para a tela inicial, com o cliente adicionado já listado.    

* Edição de cliente
    - Esta tela deve exibir o nome 'Editar cliente' no canto superior esquerdo.
    - O sistema deve permitir adicionar um novo cliente.
    - O campo Id deve esta com o valor do id do cliente a ser editado
    - Campos obrigatórios:
        - Nome
        - CPF 
            - Deve ser válido
        - Data de nascimento
            - Deve ser uma data válida
            - Dever ser menor que a data atual
            - Deve ser maior que a data atual subtraída de 200 anos.
        - Ocupação
        - telefone
        - Logradouro
        - Bairro
        - Cidade
        - Estado
        - País
    - Caso o campo não seja preenchido o sistema de ve exibir uma mensagem no padrão:
        - "Campo \<nome do campo\> obrigatório"
    - Caso o campo não esteja válido o sistema de ve exibir uma mensagem no padrão:
        - "\<nome do campo\> inválido"
        - "\<nome do campo\> inválida"
    - ao adicionar um cliente com sucesso o sistema deve retornar para a tela inicial, com o cliente adicionado já listado.    

* Usuários
    - Esta tela deve exibir o nome 'Usuários' no canto superior esquerdo.
    - Esta tela deve exibir de todos os usuários cadastrados.
    - O usuário 'admin' não pode ser removido.
    - O sistema deve permitir adicionar um novo usuário.
    - O sistema deve permitir editar um usuário existente.
    - O usuário 'admin' quando editado não pode ter seu login alterado.
    - O campo Id deve esta com o valor do id do usuário a ser editado, ou '0' caso seja um novo usuário.
    - Campos obrigatórios:
        - Login
        - Senha             
    - Caso o campo não seja preenchido o sistema de ve exibir uma mensagem no padrão:
        - "Campo \<nome do campo\> obrigatório"    
    - Ao adicionar um usuário com sucesso o sistema deve exibir o mesmo na lista.
    - Os usuários adicionados devem ter a capacidade de login, porem sem capacidade administrativas (acessar o menu 'Usuários')
    - Todos os usuários podem ter suas senhas alteradas.
