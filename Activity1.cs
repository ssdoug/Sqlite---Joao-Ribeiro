using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Runtime;
using Mono.Data.Sqlite;

namespace BdSqlite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Activity1 : Activity
    {
        //----------------------------------------------------------------------
        // dados da BD       
        private static string folder_name = "PASTA_DADOS";
        private static string nome_banco = "base_dados2.db";
        private static string nome_arquivo = "myfile.txt";      // arquivo alternativo para testes.


        // Função retorna o ambiente do sd card
        static string sd = Android.OS.Environment.ExternalStorageDirectory.ToString();

        // Função retorna a pasta download
        static string download = Android.OS.Environment.DirectoryDownloads.ToString();

        // Função retorna o ambiente interno do sistema: app/files      
        private static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        // Pasta criada para armazenar o arquivo
        private static string folder = Path.Combine(path, folder_name);

        // Local e nome do arquivo que será criado
        private string filename = Path.Combine(folder, nome_arquivo);
        private string filenameB = Path.Combine(folder, nome_banco);


        //----------------------------------------------------------------------
        //widgets
        Button btn_criar, btn_ler, btn_listar, btn_criar2;        
        TextView textView1;


        // ---------------------------------------------------------------------
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            //widgets
            btn_criar = FindViewById<Button>(Resource.Id.btn_criar);
            btn_criar2 = FindViewById<Button>(Resource.Id.btn_criar2);            
            btn_ler = FindViewById<Button>(Resource.Id.btn_ler);
            btn_listar = FindViewById<Button>(Resource.Id.btn_listar);
            textView1 = FindViewById<TextView>(Resource.Id.textView1);

            //eventos
            btn_criar.Click += Btn_criar_Click;
            btn_criar2.Click += Btn_criar2_Click;
            btn_ler.Click += Btn_ler_Click;
            btn_listar.Click += Btn_listar_Click;           
        }

        //------------------------------------------------------------------------
        private void Btn_criar_Click(object sender, System.EventArgs e)
        {
            // verificar a existencia da pasta do arquivo, se não existir, criar.
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // criar a base de dados
            SqliteConnection.CreateFile(filenameB);


        }

        //-----------------------------------------------------------------------
        private void Btn_criar2_Click(object sender, EventArgs e)
        {
            //verificar a existencia da pasta do arquivo, se não existir, criar.
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // criar a base de dados
            using (var streamWriter = new StreamWriter(filenameB, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
            }
        }

        //------------------------------------------------------------------------
        private void Btn_listar_Click(object sender, EventArgs e)
        {
            // listar os arquivos
            var arquivos = Directory.GetFiles(folder);
            var valor = string.Join(System.Environment.NewLine, arquivos);
            textView1.Text = valor;
        }

        //------------------------------------------------------------------------
        private void Btn_ler_Click(object sender, EventArgs e)
        {
            // ler a base
            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                textView1.Text = content;
            }
        }


        //------------------------------------------------------------------------
        //private void Btn_criar_Click(object sender, System.EventArgs e)
        //{
        //    // verificar a existencia da pasta do arquivo, se não existir, criar.
        //    if (!Directory.Exists(folder))
        //    {
        //        Directory.CreateDirectory(folder);
        //    }

        //    // criar a base de dados
        //    using (var streamWriter = new StreamWriter(filename, true))
        //    {
        //        streamWriter.WriteLine(DateTime.UtcNow);
        //    }
        //}
    }
}