Projeto criado para simular uma sala de bate papo, permitindo criar usuários, grupos e envio de mensagens entre usuários do grupo. 


Para iniciar é necessário executar o projeto Server primeiro e após o projeto cliente.

*******Linhas de comandos devem ser obedecidas. 

Para iniciar o bate papo é necessário criar o primeiro usuário com o commando: "UC nameuser".

*****Utilizar o Helper para identificar comandos disponíveis na aplicação.

![alt text](https://github.com/dionisoliveira/ICQ_Chat/blob/main/SampleChatWorking.png?raw=true)



****Desenvolvimento:

TcpListener/TcpClient

Utilizado IoC para injeção de dependência e desacoplamento das camadas.

Aplicado testes unitários com MSTest

Aplicado separação de responsabilidade por camadas no projeto ICQ_Server.

No client foi adicionado regra de retry para manter a resiliência da aplicação caso o servidor pare de responder.

 ********COMMAND********
 
UC: Create user - " UC NAMEUSER " 

LS: List all users in server - "LS"  

DM: Use DM command send direct " DM NAMEUSER YOURMESSAGE "  

GC: Create new group " GC NAMEGROUP "  

LSG: List all group server - "LSG"  

JG: Connect user a group - "JG NAMEGROUP" 

EXIT: Leave group " EXIT "  

HELPER: Helper COMMAND " HELPER "  

******** USE COMMAND FOR CREATE USER AND GROUP *******************  
