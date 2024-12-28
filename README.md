# Trabalho Prático II
**Integração de Sistemas de Informação**

Licenciatura em Engenharia de Sistemas Informáticos (regime *laboral*) 2024-25

## Constituição do Grupo *38*

| Número  | Nome             | Email                 |
|---------|------------------|-----------------------|
| 26024   | Gonçalo Costa    | a26024@alunos.ipca.pt |
| 25988   | Daniela Pereira  | a25988@alunos.ipca.pt |

---

# **Gestão de Unidade de Saúde**

### Breve Descrição
A gestão hospitalar enfrenta desafios complexos que envolvem a administração de funcionários, a qualidade do atendimento ao paciente e a sustentabilidade das instituições de saúde. Com o aumento da demanda por serviços de saúde, aliado à escassez de recursos e à necessidade de inovação tecnológica, os gestores hospitalares precisam desenvolver estratégias eficazes para melhorar a operação dos hospitais.

Problemas como atrasos no agendamento de consultas, comunicação ineficaz entre setores e falta de integração nos sistemas de informação impactam diretamente a qualidade do atendimento. Além disso, o uso inadequado de recursos financeiros e humanos pode comprometer a sustentabilidade das instituições.

Este trabalho tem como objetivo:

1. Analisar as práticas atuais de gestão hospitalar.
2. Identificar os principais problemas enfrentados pelas instituições de saúde.
3. Desenvolver soluções que contribuam para a melhoria dos serviços prestados aos pacientes e à comunidade em geral.
4. Propor inovações tecnológicas que otimizem processos internos, como o agendamento de consultas e a administração de funcionários.

---

## Tema e Breve Descrição
O tema do projeto é a **Gestão de Unidade de Saúde**, focado em solucionar problemas operacionais e administrativos enfrentados por instituições de saúde, conforme descrito acima.

---


## Modelo ER dos Dados

O modelo Entidade-Relacionamento (ER) utilizado para estruturar os dados do projeto é apresentado abaixo:

![Modelo ER dos Dados](https://github.com/ISI2425d/tp02-g38/raw/master/ISI%20TP02.jpg)

## Query SQL

A query SQL utilizada para criar a base de dados é apresentada abaixo:
![Query SQL](https://github.com/ISI2425d/tp02-g38/blob/master/querySQL.png)

Este modelo descreve as principais entidades, como **Utentes**, **Médicos**, **Consultas** e **Funcionários**, e os relacionamentos entre elas, fundamentais para a gestão eficiente das operações hospitalares.

---


## Arquitetura da Solução

A arquitetura do projeto abrange diferentes tecnologias e serviços para garantir uma solução robusta:


Foram abordadas as duas formas de Serviços SOAP, o *WCF* e o *ASMX*, para o *WCF*, irão ser feitos os métodos para as classes de Medico, Hospital e Consulta e para *ASMX*, os metodos de Utentes e Funcionários
# **SOAP:**  
1. **ObterUtente(s)** - Será possível obter todos os dados relativos a quaisquer utentes, ou passado como parâmetro o seu id, apenas um utente.  
2. **ObterMedico(s)** - Será possível obter todos os dados relativos a médicos associados a utentes.  
3. **ObterHospital(s)** - Será possível obter os hospitais a que um utente está associado.

## Operações implementadas com (SOAP XML)

Foram implementadas as seguintes **operações: **  
1. **Adicionar Utente** - Adicionar um utente à base de dados.  
2. **Adicionar Funcionario** - Adicionar um funcionário à base de dados.  
3. **Adicionar Medico** - Adicionar um médico à base de dados.  
4. **Adicionar Hospital** - Adicionar um hospital à base de dados.  


# **RESTful:**  
1. **ObterConsultas(s)** - Será possível obter todos os dados relativos a qualquer consulta, ou passado como parâmetro o seu id de um determinado utente ou funcionário.  
2. **ObterHospital(is)-apropriadourgencia** - Será possível obter todos os dados relativos a qualquer hospital, nomeadamente o número de consultas do atual dia, para, em caso de urgência, saber qual hospital evitar.


## Operações implementadas com (RESTful API)

Foram implementadas as seguintes **operações: **  
1. **Registar Consulta** - Fazer o registo de uma nova consulta, onde serão colocados os dados do utente, o funcionário para acompanhar, o hospital da mesma e o respetivo método.  
2. **Registar Acompanhamento** - Fazer o registo de acompanhamento de uma nova consulta, por parte do funcionário.



### Sistema de gestão da base de dados

A base de dados é remota e hospedada no **Azure**, com o uso do **SQL Server** para gerenciamento de dados. Isso garantirá escalabilidade e performance no gerenciamento de informações de pacientes, médicos, consultas e outros dados relevantes do sistema.


# Autores
- Gonçalo Costa
- Daniela Pereira
