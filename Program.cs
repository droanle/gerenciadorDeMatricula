using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace gerenciadorDeMatricula{
    
    class Program{

        static void Main(string[] args){

            List<string> materia = new List<string>();
            List<DataTable> data = new List<DataTable>();
            DataRow row;

            // Dá as boas vindas
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

            int loop = 1;

            Registro reg = new Registro();
            Editor edit = new Editor();
            Excluir exc = new Excluir();
            //Exibir exb = new Exibir();

            // loop do menu
            while (loop != 5) {
                
                Console.WriteLine("\n Por favor escolha uma das opções a baixo: ");
                Console.WriteLine(
                    "\n 1) Registrar Curso " +
                    "\n 2) Matrícular aluno" +
                    "\n 3) Editar Matrícula" +
                    "\n 4) Exibir Matrículas " +
                    "\n 5) Sair"
                );

                loop  = Convert.ToInt32(Console.ReadLine());

                //Selecionar as funções
                switch (loop) {
                    case 1:
                    Console.Clear();
                        Console.Write("Insira a nome do curso a ser registrado:");
                        materia.Add(Console.ReadLine());
                        data.Add(CreateTable());
                        break;

                    case 2:
                        Console.Clear();
                        if (materia.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else{
                            Console.WriteLine("\nLista de cursos: ");
                            for(int i = 0; i < materia.Count; i++){
                                Console.WriteLine("{0, -17}{1}", (""), (i + " - " + materia[i]));
                            }

                            Console.Write("\nEscolha o curso: ");
                            int e = Convert.ToInt32(Console.ReadLine());

                            string[] rowValue = reg.registrar();
                            row = data[e].NewRow();
                            row["nome"] = rowValue[0];
                            row["dataNascimento"] = rowValue[1];
                            row["endereco"] = rowValue[2];
                            row["telefone"] = rowValue[3];
                            row["responsavel"] = rowValue[4];
                            data[e].Rows.Add(row);

                            Console.WriteLine("\nLista de alunos ");
                            for (int i = 0; i < data[e].Rows.Count; i++)
                            {
                                Console.WriteLine("{0, -17}{1}", (""), (data[e].Rows[i]["nMatricula"] + " - " + data[e].Rows[i]["nome"]));
                            }
                        }
                        
                        break;
                    case 3:
                        Console.Clear();
                        if (materia.Count == 0) Console.WriteLine("\nVocê precisa registrar um curso para matricular um aluno");
                        else
                        {
                            Console.WriteLine("\nLista de curso ");
                            for (int i = 0; i < materia.Count; i++)
                            {
                                Console.WriteLine("{0, -17}{1}", (""), (i + " - " + materia[i]));
                            }

                            Console.Write("\nEscolha o curso: ");
                            int e = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\nLista de alunos ");
                            for (int i = 0; i < data[e].Rows.Count; i++)
                            {
                                Console.WriteLine("{0, -17}{1}", (""), (data[e].Rows[i]["nMatricula"] + " - " + data[e].Rows[i]["nome"]));
                            }

                            Console.Write("\nEscolha o curso: ");
                            int j = Convert.ToInt32(Console.ReadLine());

                            DataRow[] searchInRow = data[e].Select(("nMatricula = " + j), "nMatricula");

                            string[] searchResult = new string[5];
                            for (int i = 0; i < 5; i++) searchResult[i] = (string)searchInRow[0][(i+1)];

                            string[] rowValue = edit.editor(searchResult);
                            data[e].Rows[j]["nome"] = rowValue[0];
                            data[e].Rows[j]["dataNascimento"] = rowValue[1];
                            data[e].Rows[j]["endereco"] = rowValue[2];
                            data[e].Rows[j]["telefone"] = rowValue[3];
                            data[e].Rows[j]["responsavel"] = rowValue[4];


                            Console.Write(
                                data[e].Rows[j]["nome"] + "\n"
                                + data[e].Rows[j]["dataNascimento"] + "\n"
                                + data[e].Rows[j]["endereco"] + "\n"
                                + data[e].Rows[j]["telefone"] + "\n"
                                + data[e].Rows[j]["responsavel"] + "\n"
                            );
                        }
                        break;
                    case 4:
                   
                        break;

                    default: loop = 5; break;
                }

            }
        }
        static DataTable CreateTable()
        {
            
            DataTable table = new DataTable("alunos");
            DataColumn column;

            // Criar colunas, setar o tipo de informação,
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
            column.Caption = "ParentItem";
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
            Console.Write("\n Digite o nome do aluno: ");//exibir texto que está entre parênteses
            aluno[0] = Console.ReadLine();

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
    //Classe com a função de excluir
    public class Excluir
    {

        public void excluir()
        {

        }
    }
}


    

