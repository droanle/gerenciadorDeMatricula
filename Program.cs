using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace gerenciadorDeMatricula
{

    // Classe principal
    // Responsável por iniciar o programa e chamar os métodos de geração de relatórios
    // Com comentarios para facilitar a leitura do código, feitos com auxilio do Copilot https://copilot.github.com/

    //Algumas observações que eu tentei fazer mas não consegui devido não ter rodado em minha maquina:
    // - Tentar colocar os metodos em uma outra classes, para que não fique muito grande o código
    // - Criar um metodo para os trechos repetitivos de código, para que não fique muito grande o código
    // - Usem o Copilot é muito bom, uns 90% dos comemntários são feitos com ele, o bagulho é bem inteligente
    // - é isso, bon voyage

    class Program
    {

        static void Main(string[] args)
        {

            List<string> curso = new List<string>();                    // Guarda o nome e posição dos cursos registrado
            List<DataTable> tabelaAlunos = new List<DataTable>();       // Guarda os dados dos alunos
            DataRow row;                                                // Linha da tabela

            BoasVindas();                                               // Dá as boas vindas

            int loop = 1, e, j;                                         // Variáveis de controle

            Registro reg = new Registro();                              // Instancia a classe de registro
            Editor edit = new Editor();                                 // Instancia a classe de edição

            // loop do menu
            while (loop != 7)
            {                                                           // loop do menu

                Console.WriteLine("\n Por favor escolha uma das opções a baixo: ");
                Console.Write(
                    "\n 1) Registrar Curso" +
                    "\n 2) Matricular aluno" +
                    "\n 3) Editar Matrícula" +
                    "\n 4) Exibir Matrículas" +
                    "\n 5) Deletar Curso" +
                    "\n 6) Deletar Matrículas" +
                    "\n 7) Sair" +
                    "\n\n Opção: ");                                    // Menu

                try
                {                                                       // Verifica se o usuário digitou um número
                    loop = Convert.ToInt32(Console.ReadLine());         // Recebe a opção do usuário
                }
                catch
                {                                                       // Caso o usuário não digite um número
                    loop = 7;                                           // Se o usuário digitar algo diferente de um número, o loop será encerrado
                }                                                       // Verifica se o usuário digitou um número

                // Executa as operações
                switch (loop)
                {                                                       // Verifica qual opção foi escolhida

                    case 1:
                        BoasVindas();                                   // Dá as boas vindas
                        

                        Console.Write("Insira a nome do curso a ser registrado: ");
                        curso.Add(Console.ReadLine());                  // Recebe o nome do curso
                        tabelaAlunos.Add(CreateTable());                // Cria uma tabela para o curso

                        barra(20);                                      // Cria uma barra de separação
                        break;                                          // Fim do case 1

                    // Matrícular aluno
                    case 2:
                        BoasVindas();                                   // Dá as boas vindas

                        if (curso.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {

                            Console.WriteLine("\nLista de cursos: ");   // Lista os cursos registrados
                            for (int i = 0; i < curso.Count; i++)
                            {                                           // Loop para listar os cursos
                                if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i])); // Imprime os cursos
                            }

                            Console.Write("\nEscolha o curso: ");       // Escolhe o curso
                            e = Convert.ToInt32(Console.ReadLine());    // Recebe a opção do usuário

                            string[] rowValue = reg.registrar();        // Recebe os dados do aluno
                            row = tabelaAlunos[e].NewRow();             // Cria uma nova linha na tabela
                            row["nome"] = rowValue[0];                  // Adiciona o nome do aluno
                            row["dataNascimento"] = rowValue[1];        // Adiciona a data de nascimento do aluno
                            row["endereco"] = rowValue[2];              // Adiciona o endereço do aluno
                            row["telefone"] = rowValue[3];              // Adiciona o telefone do aluno
                            row["responsavel"] = rowValue[4];           // Adiciona o responsável do aluno
                            tabelaAlunos[e].Rows.Add(row);              // Adiciona a linha na tabela

                            Console.WriteLine("\nLista de alunos: ");
                            for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                            {                                           // Loop para listar os alunos
                                Console.WriteLine("{0, -17}{1}", (""), (tabelaAlunos[e].Rows[i]["nMatricula"] + " - " + tabelaAlunos[e].Rows[i]["nome"]));
                            }
                        }

                        barra(20);                                      // Cria uma barra de separação
                        break;                                          // Fim da opção 2

                    // Editar Matrícula
                    case 3:
                        BoasVindas();                                   // Dá as boas vindas

                        if (curso.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {
                            Console.WriteLine("\nLista de curso: ");    // Lista os cursos registrados
                            for (int i = 0; i < curso.Count; i++)
                            {                                           // Loop para listar os cursos
                                if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));   // Imprime o nome do curso
                            }

                            Console.Write("\nEscolha o curso: ");       // Escolhe o curso
                            e = Convert.ToInt32(Console.ReadLine());    // Recebe a opção do usuário

                            Console.WriteLine("\nLista de alunos: ");   // Lista os alunos do curso

                            for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                            {                                           // Loop para listar os alunos
                                if (curso[e] != null) Console.WriteLine("{0, -17}{1}", (""), (tabelaAlunos[e].Rows[i]["nMatricula"] + " - " + tabelaAlunos[e].Rows[i]["nome"]));
                            }

                            Console.Write("\nEscolha o aluno: ");       // Escolhe o aluno
                            j = Convert.ToInt32(Console.ReadLine());    // Recebe a opção do usuário

                            DataRow[] searchInRow = tabelaAlunos[e].Select(("nMatricula = " + j), "nMatricula");        // Busca o aluno na tabela

                            string[] searchResult = new string[5];                                                      // Cria um array para receber os dados do aluno
                            for (int i = 0; i < 5; i++) searchResult[i] = (string)searchInRow[0][(i + 1)];

                            string[] rowValue = edit.editor(searchResult);              // Recebe os dados do aluno editados
                            tabelaAlunos[e].Rows[j]["nome"] = rowValue[0];              // Adiciona o nome do aluno
                            tabelaAlunos[e].Rows[j]["dataNascimento"] = rowValue[1];    // Adiciona a data de nascimento do aluno
                            tabelaAlunos[e].Rows[j]["endereco"] = rowValue[2];          // Adiciona o endereço do aluno
                            tabelaAlunos[e].Rows[j]["telefone"] = rowValue[3];          // Adiciona o telefone do aluno
                            tabelaAlunos[e].Rows[j]["responsavel"] = rowValue[4];       // Adiciona o responsável do aluno

                        }
                        barra(20);                                      // Cria uma barra de separação
                        break;                                          // Fim da opção 3

                    // Exibir Matrículas
                    case 4:
                        BoasVindas();                                   // Dá as boas vindas

                        if (curso.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {
                            Console.WriteLine("\nLista de curso: ");    // Lista os cursos registrados
                            for (int i = 0; i <= curso.Count; i++)
                            {                                           // Loop para listar os cursos
                                if (i != curso.Count)
                                {                                       // Se o curso não for nulo
                                    if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                                }
                                else Console.WriteLine("{0, -17}{1}", (""), (i + " - Mostrar todas as Matriculas"));
                            }

                            Console.Write("\nEscolha o curso: ");       // Escolhe o curso
                            e = Convert.ToInt32(Console.ReadLine());    // Recebe a opção do usuário

                            if (e != curso.Count)
                            {                                           // Se o curso não for nulo
                                Console.WriteLine("\n   Lista de matriculas do curso \"" + curso[e] + "\":");
                                Console.WriteLine(
                                    "{0, -6}" +
                                    "| Numero da matricula " +
                                    "|   Nome do aluno   " +
                                    "|     Telefone     " +
                                    "| Data de Nascimento " +
                                    "| Nome do Responsável " +
                                    "|     Endereço     |", ("")
                                );

                                try
                                {                                       // Tenta listar as matrículas do curso
                                    if (tabelaAlunos[e].Rows.Count == 0) { throw new ArgumentNullException("Nenhuma matrícula registrada"); }
                                    for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                                    {
                                        Console.WriteLine("{0, -6}| {1, -19} | {2, -17} | {3, -16} | {4, -18} | {5, -19} | {6, -16} |",
                                            (""),
                                            tabelaAlunos[e].Rows[i]["nMatricula"],      // Imprime o número da matrícula
                                            tabelaAlunos[e].Rows[i]["nome"],            // Imprime o nome do aluno
                                            tabelaAlunos[e].Rows[i]["telefone"],        // Imprime o telefone do aluno
                                            tabelaAlunos[e].Rows[i]["dataNascimento"],  // Imprime a data de nascimento do aluno
                                            tabelaAlunos[e].Rows[i]["responsavel"],     // Imprime o responsável do aluno
                                            tabelaAlunos[e].Rows[i]["endereco"]         // Imprime o endereço do aluno
                                        );
                                    }
                                }
                                catch
                                {                                   // Se não houver matrículas
                                    Console.WriteLine("{0,-6}| {1} {0,-80}|", (""), "Não existe nenhuma matrícula registrada!");
                                }
                            }
                            else
                            {                                       // Se o usuário escolher a opção "Mostrar todas as matrículas"
                                Console.WriteLine("\nTodas as matriculas: ");
                                for (e = 0; e < curso.Count; e++)
                                {                                   // Loop para listar todas as matrículas   
                                    Console.WriteLine("\n   Lista de matriculas do curso \"" + curso[e] + "\":");
                                    Console.WriteLine(
                                        "\n{0, -6}" +
                                        "| Numero da matricula " +
                                        "|   Nome do aluno   " +
                                        "|     Telefone     " +
                                        "| Data de Nascimento " +
                                        "| Nome do Responsável " +
                                        "|     Endereço     |", ("")
                                    );

                                    try
                                    {
                                        if (tabelaAlunos[e].Rows.Count == 0) { throw new ArgumentNullException("Nenhuma matrícula registrada"); }
                                        for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                                        {
                                            Console.WriteLine("{0, -6}| {1, -19} | {2, -17} | {3, -16} | {4, -18} | {5, -19} | {6, -16} |",
                                                (""),
                                                tabelaAlunos[e].Rows[i]["nMatricula"],      // Imprime o número da matrícula
                                                tabelaAlunos[e].Rows[i]["nome"],            // Imprime o nome do aluno
                                                tabelaAlunos[e].Rows[i]["telefone"],        // Imprime o telefone do aluno
                                                tabelaAlunos[e].Rows[i]["dataNascimento"],  // Imprime a data de nascimento do aluno
                                                tabelaAlunos[e].Rows[i]["responsavel"],     // Imprime o responsável do aluno
                                                tabelaAlunos[e].Rows[i]["endereco"]         // Imprime o endereço do aluno
                                            );
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("{0,-6}| {1} {0,-80}|", (""), "Não existe nenhuma matrícula registrada!");
                                    }
                                }
                            }
                        }
                        barra(20);                                                      // Imprime a barra de divisão
                        break;                                                          // Sai do case 4

                    // Deletar Matrículas 
                    case 5:
                        BoasVindas();                                                   // Boas-vindas

                        Console.WriteLine("\nLista de curso: ");                        // Lista os cursos
                        for (int i = 0; i < curso.Count; i++)
                        {                                                               // Loop para listar os cursos
                            if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                        }

                        Console.Write("\nEscolha o curso: ");                           // Escolhe o curso
                        e = Convert.ToInt32(Console.ReadLine());                        // Recebe a opção do usuário

                        Console.ForegroundColor = ConsoleColor.DarkRed;                 // Cor da letra
                        Console.Write("Tem certeza que quer deletar esse curso? [y/n] ");   // Pergunta se o usuário quer deletar o curso
                        if (Console.ReadLine() == "y")
                        {                                                               // Se o usuário escolher a opção "Sim"
                            curso.RemoveAt(e);                                          // Remove o curso
                            tabelaAlunos.RemoveAt(e);                                   // Remove a tabela de alunos
                        }


                        barra(20);                                                      // Barra de separação
                        
                        break;                                                          // Fim do case 5

                    // Deletar Curso
                    case 6:
                        BoasVindas();                                                   // Boas-vindas

                        Console.WriteLine("\nLista de curso: ");                        // Lista os cursos
                        for (int i = 0; i < curso.Count; i++)
                        {                                                               // Loop para listar os cursos
                            if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                        }

                        Console.Write("\nEscolha o curso: ");                           // Escolhe o curso
                        e = Convert.ToInt32(Console.ReadLine());                        // Recebe a opção do usuário

                        Console.WriteLine("\nLista de alunos: ");                       // Lista os alunos
                        for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                        {                                                               // Loop para listar os alunos
                            Console.WriteLine("{0, -17}{1}", (""), (i + " - " + tabelaAlunos[e].Rows[i]["nome"]));
                        }

                        Console.Write("\nEscolha o aluno: ");                           // Escolhe o aluno
                        j = Convert.ToInt32(Console.ReadLine());                        // Recebe a opção do usuário

                        Console.ForegroundColor = ConsoleColor.DarkRed;                 // Cor da letra
                        Console.Write("Tem certeza que quer deletar a matricula desse aluno(a)? [y/n] ");   // Pergunta se o usuário quer deletar a matrícula
                        if (Console.ReadLine() == "y")
                        {                                                               // Se o usuário escolher a opção "Sim"
                            tabelaAlunos[e].Rows[j].Delete();                           // Deleta a matrícula
                        }


                        barra(20);                                                      // Barra de separação  
                        break;                                                          // Fim do case 6

                    default: loop = 7; break;                                           // Fim do switch
                }

            }
        }

        // método de Boas-vindas
        static void BoasVindas()
        {
            Console.Clear();                                        // limpa a tela

            Console.BackgroundColor = ConsoleColor.Black;           // cor de fundo
            Console.Write(" Ola! Bem vindo(a) ao ");                // texto

            Console.ForegroundColor = ConsoleColor.DarkRed;         // cor do texto
            Console.WriteLine(" \"GERENCIADOR DE MATRICULA\" ");    // texto
            Console.ForegroundColor = ConsoleColor.White;           // cor do texto

            Console.ForegroundColor = ConsoleColor.DarkGray;        // cor do texto
            Console.WriteLine(
              "{1, -48}\n{0, -15} {2, -32}\n {1, -15}{3, -32}\n {1, -15}{4, -32}\n {1, -15}{5, -32}\n{1, -48}",
              (" --> Feito Por:"),
              (" "),
              ("Denilson Araujo"),
              ("Leandro De Meirelles"),
              ("Igor da Silva Costa"),
              ("Sara Sony")
            );                                                      // texto
            Console.ResetColor();                                   // cor do texto

            barra(20);                                              // Cria uma barra de separação
        }

        // método que insere uma barra de separação
        static void barra(int size)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;            // cor da barra

            Console.Write("\n//");                                  // texto
            for (int i = 0; i < size; i++) Console.Write("=");      // texto
            Console.Write("//");                                    // texto
            for (int i = 0; i < size; i++) Console.Write("=");      // texto
            Console.Write("//\n\n");                                // texto

            Console.ResetColor();                                   // cor do texto
        }

        // Cria uma tabela de dados
        static DataTable CreateTable()
        {

            DataTable table = new DataTable("alunos");              // Cria uma tabela de dados
            DataColumn column;                                      // Cria uma coluna de dados

            // Criar colunas, setar o tipo de informação:

            // Coluna do Número de matricula.
            column = new DataColumn();                              // Cria uma coluna de dados
            column.DataType = System.Type.GetType("System.Int32");  // Tipo de informação
            column.ColumnName = "nMatricula";                       // Nome da coluna
            column.AutoIncrement = true;                            // Autoincremento
            column.AutoIncrementSeed = 0;                           // Valor inicial
            column.AutoIncrementStep = 1;                           // Valor de incremento
            column.ReadOnly = true;                                 // Somente leitura
            column.Unique = true;                                   // Único
            table.Columns.Add(column);                              // Adiciona a coluna

            // Coluna do Nome do aluno.
            column = new DataColumn();                              // Cria uma coluna de dados
            column.DataType = System.Type.GetType("System.String"); // Tipo de informação
            column.ColumnName = "nome";                             // Nome da coluna
            column.AutoIncrement = false;                           // Autoincremento
            column.Caption = "Nome do aluno";                       // Legenda
            column.ReadOnly = false;                                // Somente leitura
            column.Unique = false;                                  // Único
            table.Columns.Add(column);                              // Adiciona a coluna

            // Coluna do número de telefone.
            column = new DataColumn();                              // Cria uma coluna de dados
            column.DataType = System.Type.GetType("System.String"); // Tipo de informação
            column.ColumnName = "telefone";                         // Nome da coluna
            column.AutoIncrement = false;                           // Autoincremento
            column.Caption = "Telefone";                            // Legenda
            column.ReadOnly = false;                                // Somente leitura
            column.Unique = false;                                  // Único
            table.Columns.Add(column);                              // Adiciona a coluna

            // Coluna da data de nascimento
            column = new DataColumn();                              // Cria uma coluna de dados
            column.DataType = System.Type.GetType("System.String"); // Tipo de informação
            column.ColumnName = "dataNascimento";                   // Nome da coluna
            column.AutoIncrement = false;                           // Autoincremento
            column.Caption = "Data de Nascimento";                  // Legenda
            column.ReadOnly = false;                                // Somente leitura
            column.Unique = false;                                  // Único
            table.Columns.Add(column);                              // Adiciona a coluna

            // Coluna do Nome do Responsável.
            column = new DataColumn();                              // Cria uma coluna de dados
            column.DataType = System.Type.GetType("System.String"); // Tipo de informação
            column.ColumnName = "responsavel";                      // Nome da coluna
            column.AutoIncrement = false;                           // Autoincremento
            column.Caption = "Nome do Responsável";                 // Legenda
            column.ReadOnly = false;                                // Somente leitura
            column.Unique = false;                                  // Único
            table.Columns.Add(column);                              // Adiciona a coluna

            // Coluna do Endereço do aluno.
            column = new DataColumn();                              // Cria uma coluna de dados
            column.DataType = System.Type.GetType("System.String"); // Tipo de informação
            column.ColumnName = "endereco";                         // Nome da coluna
            column.AutoIncrement = false;                           // Autoincremento
            column.Caption = "Endereço do aluno";                   // Legenda
            column.ReadOnly = false;                                // Somente leitura
            column.Unique = false;                                  // Único
            table.Columns.Add(column);                              // Adiciona a coluna

            return table;                                           // Retorna a tabela

        }

    }

    //Classe com as funções do registro
    public class Registro
    {                                                               // Classe com as funções do registro
        private string[] aluno = new string[5];                     // Array com os dados do aluno

        public string[] registrar()
        {                                                           // Método que registra os dados do aluno
            Console.Write("\n Digite o nome do aluno: ");           // exibir texto que está entre parênteses 
            aluno[0] = Console.ReadLine();                          // Ler o nome do aluno

            Console.Write("\n Digite a data de nascimento do aluno (dd/mm/aaaa): ");
            aluno[1] = Console.ReadLine();                          // Ler a data de nascimento do aluno

            Console.Write("\n Digite o endereço do aluno: ");
            aluno[2] = Console.ReadLine();                          // Ler o endereço do aluno

            Console.Write("\n Digite o número de telefone do aluno: ");
            aluno[3] = Console.ReadLine();                          // Ler o número de telefone do aluno

            Console.Write("\n Digite o nome do responsavél pelo o aluno: ");
            aluno[4] = Console.ReadLine();                          // Ler o nome do responsavél pelo o aluno

            Console.Write("\n");                                    // pular linha
            return aluno;                                           // Retorna o array com os dados do aluno
        }

    }
    //Classe com função de editar
    public class Editor
    {                                                               // Classe com função de editar
        private string[] aluno = new string[5];                     // Array com os dados do aluno
        public string[] editor(string[] row)
        {                                                           // Método que edita os dados do aluno
            Console.WriteLine("\nCaso queira manter o valor atual basta apertar \"Enter\" com o campo vazio. Os valores que estão atualmente registrados na matrícula do aluno são exibidos ante dos campos de inserção do novo dado.");
            string value = "";                                    // Variável que recebe o valor digitado pelo usuário

            Console.Write("\n Valor atual: " + row[0]);
            Console.Write("\n Digite o nome do aluno: ");           // exibir texto que está entre parênteses
            value = Console.ReadLine();                             // Ler o nome do aluno
            if (value != "") { aluno[0] = value; value = ""; }  // Se o valor não for nulo, atribui o valor ao array
            else aluno[0] = row[0];                                 // Se o valor for nulo, atribui o valor do array

            Console.Write("\n Valor atual: " + row[2]);
            Console.Write("\n Digite a data de nascimento do aluno (dd/mm/aaaa): ");
            value = Console.ReadLine();                             // Ler a data de nascimento do aluno
            if (value != "") { aluno[1] = value; value = ""; }  // Se o valor não for nulo, atribui o valor ao array
            else aluno[1] = row[2];                                 // Se o valor for nulo, atribui o valor do array

            Console.Write("\n Valor atual: " + row[4]);
            Console.Write("\n Digite o endereço do aluno: ");
            value = Console.ReadLine();                             // Ler o endereço do aluno
            if (value != "") { aluno[2] = value; value = ""; }  // Se o valor não for nulo, atribui o valor ao array
            else aluno[2] = row[4];                                 // Se o valor for nulo, atribui o valor do array

            Console.Write("\n Valor atual: " + row[1]);
            Console.Write("\n Digite o número de telefone do aluno: ");
            value = Console.ReadLine();                             // Ler o número de telefone do aluno
            if (value != "") { aluno[3] = value; value = ""; }  // Se o valor não for nulo, atribui o valor ao array
            else aluno[3] = row[1];                                 // Se o valor for nulo, atribui o valor do array

            Console.Write("\n Valor atual: " + row[3]);
            Console.Write("\n Digite o nome do responsavél pelo o aluno: ");
            value = Console.ReadLine();                             // Ler o nome do responsavél pelo o aluno
            if (value != "") { aluno[4] = value; value = ""; }  // Se o valor não for nulo, atribui o valor ao array
            else aluno[4] = row[3];                                 // Se o valor for nulo, atribui o valor do array

            Console.Write("\n");                                    // pular linha
            return aluno;                                           // Retorna o array com os dados do aluno
        }
    }

}




