using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace gerenciadorDeMatricula
{

    class Program
    {

        static void Main(string[] args)
        {

            List<string> curso = new List<string>(); // Guarda o nome e posição dos cursos registrado
            List<DataTable> tabelaAlunos = new List<DataTable>();
            DataRow row;

            // Dá as boas vindas
            BoasVindas();

            int loop = 1, e, j;

            Registro reg = new Registro(); //estanciando as classes como objeto para poder invocá-las
            Editor edit = new Editor();

            // loop do menu
            while (loop != 7)
            {

                Console.WriteLine("\n Por favor escolha uma das opções a baixo: ");
                Console.WriteLine(
                    "\n 1) Registrar Curso" +
                    "\n 2) Matrícular aluno" +
                    "\n 3) Editar Matrícula" +
                    "\n 4) Exibir Matrículas" +
                    "\n 5) Deletar Curso" +
                    "\n 6) Deletar Matrículas" +
                    "\n 7) Sair"
                );

                try { loop = Convert.ToInt32(Console.ReadLine()); } catch { loop = 7; }

                // Executa as operações
                switch (loop)
                {

                    // Registrar Curso
                    case 1:
                        Console.Clear(); //Limpar o texto do console
                        BoasVindas();
                        barra(20);

                        Console.Write("Insira a nome do curso a ser registrado:");
                        curso.Add(Console.ReadLine());
                        tabelaAlunos.Add(CreateTable());
                        barra(20);

                        break;

                    // Matrícular aluno
                    case 2:
                        Console.Clear();
                        BoasVindas();
                        barra(20);

                        if (curso.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {

                            Console.WriteLine("\nLista de cursos: ");
                            for (int i = 0; i < curso.Count; i++)
                            {
                                if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                            }

                            Console.Write("\nEscolha o curso: ");
                            e = Convert.ToInt32(Console.ReadLine());

                            string[] rowValue = reg.registrar();
                            row = tabelaAlunos[e].NewRow();
                            row["nome"] = rowValue[0];
                            row["dataNascimento"] = rowValue[1];
                            row["endereco"] = rowValue[2];
                            row["telefone"] = rowValue[3];
                            row["responsavel"] = rowValue[4];
                            tabelaAlunos[e].Rows.Add(row);

                            Console.WriteLine("\nLista de alunos: ");
                            for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                            {
                                Console.WriteLine("{0, -17}{1}", (""), (tabelaAlunos[e].Rows[i]["nMatricula"] + " - " + tabelaAlunos[e].Rows[i]["nome"]));
                            }
                        }
                        barra(20);

                        break;

                    // Editar Matrícula
                    case 3:
                        Console.Clear();
                        BoasVindas();
                        barra(20);

                        if (curso.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {
                            Console.WriteLine("\nLista de curso: ");
                            for (int i = 0; i < curso.Count; i++)
                            {
                                if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                            }

                            Console.Write("\nEscolha o curso: ");
                            e = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\nLista de alunos: ");
                            for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                            {
                                if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (tabelaAlunos[e].Rows[i]["nMatricula"] + " - " + tabelaAlunos[e].Rows[i]["nome"]));
                            }

                            Console.Write("\nEscolha o aluno: ");
                            j = Convert.ToInt32(Console.ReadLine());

                            DataRow[] searchInRow = tabelaAlunos[e].Select(("nMatricula = " + j), "nMatricula");

                            string[] searchResult = new string[5];
                            for (int i = 0; i < 5; i++) searchResult[i] = (string)searchInRow[0][(i + 1)];

                            string[] rowValue = edit.editor(searchResult);
                            tabelaAlunos[e].Rows[j]["nome"] = rowValue[0];
                            tabelaAlunos[e].Rows[j]["dataNascimento"] = rowValue[1];
                            tabelaAlunos[e].Rows[j]["endereco"] = rowValue[2];
                            tabelaAlunos[e].Rows[j]["telefone"] = rowValue[3];
                            tabelaAlunos[e].Rows[j]["responsavel"] = rowValue[4];

                        }
                        barra(20);

                        break;

                    // Exibir Matrículas
                    case 4:
                        Console.Clear();
                        BoasVindas();
                        barra(20);

                        if (curso.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {
                            Console.WriteLine("\nLista de curso: ");
                            for (int i = 0; i <= curso.Count; i++)
                            {
                                if (i != curso.Count)
                                {
                                    if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                                }
                                else Console.WriteLine("{0, -17}{1}", (""), (i + " - Mostrar todas as Matriculas"));
                            }

                            Console.Write("\nEscolha o curso: ");
                            e = Convert.ToInt32(Console.ReadLine());

                            if (e != curso.Count)
                            {
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
                                {
                                    if (tabelaAlunos[e].Rows.Count == 0) { throw new ArgumentNullException("Nenhuma matrícula registrada"); }
                                    for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                                    {
                                        Console.WriteLine("{0, -6}| {1, -19} | {2, -17} | {3, -16} | {4, -18} | {5, -19} | {6, -16} |",
                                            (""),
                                            tabelaAlunos[e].Rows[i]["nMatricula"],
                                            tabelaAlunos[e].Rows[i]["nome"],
                                            tabelaAlunos[e].Rows[i]["telefone"],
                                            tabelaAlunos[e].Rows[i]["dataNascimento"],
                                            tabelaAlunos[e].Rows[i]["responsavel"],
                                            tabelaAlunos[e].Rows[i]["endereco"]
                                        );
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("{0,-6}| {1} {0,-80}|", (""), "Não existe nenhuma matrícula registrada!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nTodas as matriculas: ");
                                for (e = 0; e < curso.Count; e++)
                                {
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
                                                tabelaAlunos[e].Rows[i]["nMatricula"],
                                                tabelaAlunos[e].Rows[i]["nome"],
                                                tabelaAlunos[e].Rows[i]["telefone"],
                                                tabelaAlunos[e].Rows[i]["dataNascimento"],
                                                tabelaAlunos[e].Rows[i]["responsavel"],
                                                tabelaAlunos[e].Rows[i]["endereco"]
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
                        barra(20);

                        break;

                    // Deletar Matrículas 
                    case 5:
                        Console.Clear();
                        BoasVindas();
                        barra(20);

                        Console.WriteLine("\nLista de curso: ");
                        for (int i = 0; i < curso.Count; i++)
                        {
                            if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                        }

                        Console.Write("\nEscolha o curso: ");
                        e = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("\nLista de alunos: ");
                        for (int i = 0; i < tabelaAlunos[e].Rows.Count; i++)
                        {
                            Console.WriteLine("{0, -17}{1}", (""), (i + " - " + tabelaAlunos[e].Rows[i]["nome"]));
                        }

                        Console.Write("\nEscolha o aluno: ");
                        j = Convert.ToInt32(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Tem certeza que quer deletar a matricula desse aluno(a)? [y/n] ");
                        if (Console.ReadLine() == "y")
                        {
                            tabelaAlunos[e].Rows[j].Delete();
                        }


                        barra(20);

                        break;

                    // Deletar Curso
                    case 6:
                        Console.Clear();
                        BoasVindas();
                        barra(20);

                        Console.WriteLine("\nLista de curso: ");
                        for (int i = 0; i < curso.Count; i++)
                        {
                            if (curso[i] != null) Console.WriteLine("{0, -17}{1}", (""), (i + " - " + curso[i]));
                        }

                        Console.Write("\nEscolha o curso: ");
                        e = Convert.ToInt32(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Tem certeza que quer deletar esse curso? [y/n] ");
                        if (Console.ReadLine() == "y")
                        {
                            curso.RemoveAt(e);
                            tabelaAlunos.RemoveAt(e);
                        }


                        barra(20);

                        break;

                    default: loop = 7; break;
                }

            }
        }

        // método que insere uma barra de separação
        static void barra(int size)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("\n//");
            for (int i = 0; i < size; i++) Console.Write("=");
            Console.Write("//");
            for (int i = 0; i < size; i++) Console.Write("=");
            Console.Write("//\n\n");

            Console.ResetColor();
        }

        static void BoasVindas()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" Ola! Bem vindo(a) a");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" \"GERENCIADOR DE MATRICULA\" ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(
              "{1, -48}\n{0, -15} {2, -32}\n {1, -15}{3, -32}\n {1, -15}{4, -32}\n {1, -15}{5, -32}\n{1, -48}",
              (" --> Feito Por:"),
              (""),
              ("Denilson Araujo"),
              ("Leandro De Meirelles"),
              ("Igor da Silva Costa"),
              ("Sara Sony")
            );
            Console.ResetColor();
        }

        static DataTable CreateTable()
        {

            DataTable table = new DataTable("alunos");
            DataColumn column;

            // Criar colunas, setar o tipo de informação:

            // Coluna do Número de matricula.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "nMatricula";
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 0;
            column.AutoIncrementStep = 1;
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            // Coluna do Nome do aluno.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nome";
            column.AutoIncrement = false;
            column.Caption = "Nome do aluno";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Coluna do número de telefone.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "telefone";
            column.AutoIncrement = false;
            column.Caption = "Telefone";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Coluna da data de nascimento
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dataNascimento";
            column.AutoIncrement = false;
            column.Caption = "Data de Nascimento";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Coluna do Nome do Responsável.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "responsavel";
            column.AutoIncrement = false;
            column.Caption = "Nome do Responsável";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Coluna do Endereço do aluno.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "endereco";
            column.AutoIncrement = false;
            column.Caption = "Endereço do aluno";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            return table;



        }

    }

    //Classe com as funções do registro
    public class Registro
    {
        private string[] aluno = new string[5];

        public string[] registrar()
        {
            Console.Write("\n Digite o nome do aluno: ");
            aluno[0] = Console.ReadLine();
            //Registra a string digitada pelo usuário na posição especificada
            Console.Write("\n Digite a data de nascimento do aluno (dd/mm/aaaa): ");
            aluno[1] = Console.ReadLine();

            Console.Write("\n Digite o endereço do aluno: ");
            aluno[2] = Console.ReadLine();

            Console.Write("\n Digite o número de telefone do aluno: ");
            aluno[3] = Console.ReadLine();

            Console.Write("\n Digite o nome do responsavél pelo o aluno: ");
            aluno[4] = Console.ReadLine();

            Console.Write("\n");
            return aluno;
        }

    }
    //Classe com função de editar
    public class Editor
    {

        private string[] aluno = new string[5];

        public string[] editor(string[] row)
        {
            string value = null;
            Console.Write("\n Digite o nome do aluno: ");
            value = Console.ReadLine();
            if (value != null) { aluno[0] = value; value = null; }
            else aluno[0] = row[0];
            //se o usuário não digitar nada o valor se mantem null e  ativa a condição do else,
            //mantendo os valores da linha nas posições especificadas

            Console.Write("\n Digite a data de nascimento do aluno (dd/mm/aaaa): ");
            value = Console.ReadLine();
            if (value != null) { aluno[1] = value; value = null; }
            else aluno[1] = row[1];

            Console.Write("\n Digite o endereço do aluno: ");
            value = Console.ReadLine();
            if (value != null) { aluno[2] = value; value = null; }
            else aluno[2] = row[2];

            Console.Write("\n Digite o número de telefone do aluno: ");
            value = Console.ReadLine();
            if (value != null) { aluno[3] = value; value = null; }
            else aluno[3] = row[3];

            Console.Write("\n Digite o nome do responsavél pelo o aluno: ");
            value = Console.ReadLine();
            if (value != null) { aluno[4] = value; value = null; }
            else aluno[4] = row[4];

            Console.Write("\n");
            return aluno;
        }
    }

}




