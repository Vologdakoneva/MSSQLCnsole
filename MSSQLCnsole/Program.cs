// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using MSSQLCnsole;
using MSSQLCnsole.dataTableAdapters;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;


Console.Clear();
Console.WriteLine("Проверка настроек.......");
try
{
    // Проверяем наличие и содержание appsettings.json
    var builders = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
    var configuration = builders.Build();

    string? str = configuration.GetConnectionString("Work_MSSQL");
    if (str == null)
    {
        Console.WriteLine("Не определена настройка для соединения с сервером (appsettings.json)");
        Console.ReadLine(); return;
    }
    else
    {

        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        builder.ConnectionString = str;

        string server = builder["Data Source"] as string;
        string database = builder["Initial Catalog"] as string;
        builder["Initial Catalog"] = "master";
        var srv = builder.ConnectionString;
        try
        {
            using (var connection = new SqlConnection(srv))
            {
                connection.Open();
                connection.Close();
                connection.ConnectionString = str;
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    // Базы нет
                    using (var connectiondb = new SqlConnection(srv))
                    {
                        connectiondb.Open();
                        var command = connectiondb.CreateCommand();
                        command.CommandText = "CREATE DATABASE EmployeeDB";
                        command.ExecuteNonQuery();

                    }




                    using (var connectiondb = new SqlConnection(str))
                    {
                        connectiondb.Open();
                        var command = connectiondb.CreateCommand();

                        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.sql", SearchOption.AllDirectories);
                        foreach (string file in files)
                        {
                            command.CommandText = File.ReadAllText(file);
                            command.ExecuteNonQuery();
                        }

                    }
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Сервер MS SQL указан не верно. Исправьте.(appsettings.json)");
            Console.ReadLine(); return;
        }

        //var command = connection.CreateCommand();
        //    command.CommandText = "CREATE DATABASE mydb";
        //    command.ExecuteNonQuery();
        data DataEmp = new data();
        sp_GetEmployesTableAdapter sp_GetEmployesTableAdapter = new sp_GetEmployesTableAdapter();
        Console.Clear();
        int num = 0;
        while (true)
        {
            sp_GetEmployesTableAdapter.Connection.ConnectionString = str;
            sp_GetEmployesTableAdapter.Fill(DataEmp.sp_GetEmployes, 0);
            Console.Clear();
            if (num==0)
            {
               
                Console.WriteLine("Команда не распознана");
            }


            Console.WriteLine("Сотрудники:");
            Console.WriteLine("");
            ShowEmploys.ShowEmploy(DataEmp.sp_GetEmployes);

            Console.WriteLine("");
            Console.WriteLine("Работа с сотрудниками");
            Console.WriteLine("Укажите что нужно выполнить :");
            Console.WriteLine("1 - вывести список, 2-добавить сотрудника, 3-Обновить информацию о сотрудникеб 4-удалить сотрудника Ctrl+C Выход");

            string Exectype = Console.ReadLine();
            
            if (int.TryParse(Exectype, out num))
            {
                DataRow newrow = DataEmp.sp_GetEmployes.NewRow();
                var readed = "";
                switch (num)
                {
                    case  2:
                        newrow[0] = 0;
                        for (Int32 i = 1; i < DataEmp.sp_GetEmployes.Columns.Count; i++)
                        {
                            Console.WriteLine(DataEmp.sp_GetEmployes.Columns[i].ColumnName); readed = Console.ReadLine();
                            try
                            {
                                newrow[i] = readed;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Ошибка " + ex.Message);
                                Console.ReadLine();
                                break;
                            }
                        }
                        DataEmp.sp_GetEmployes.Rows.Add(newrow);
                        sp_GetEmployesTableAdapter.Update(DataEmp.sp_GetEmployes);
                        Console.WriteLine("");
                      break;
                        case 3:
                        Console.WriteLine("Укажите ID сотрудника"); readed = Console.ReadLine();
                        if (int.TryParse(readed, out num))
                        {
                            DataRow[] find = DataEmp.sp_GetEmployes.Select("EmployeeID = " + num.ToString());
                            if (find == null)
                            {
                                Console.WriteLine("Сотрудник не найден");
                                Console.ReadLine();
                                break;

                            }
                            else
                            {
                                Console.WriteLine("Укажите Номер колонки для изменения"); 
                                readed = Console.ReadLine();
                                if (int.TryParse(readed, out num))
                                {
                                    try
                                    {
                                        
                                        Console.WriteLine(DataEmp.sp_GetEmployes.Columns[num].ColumnName);
                                        readed = Console.ReadLine();
                                        find[0][num] = readed;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Ошибка " + ex.Message);
                                        Console.ReadLine();
                                        break;
                                    }
                                    sp_GetEmployesTableAdapter.Update(DataEmp.sp_GetEmployes);
                                    Console.WriteLine("");
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка Номер должен быть числом" );
                            Console.ReadLine();
                            break ;
                        }
                        break;
                    case 4:
                        Console.WriteLine("Укажите ID сотрудника для удаления"); readed = Console.ReadLine();
                        if (int.TryParse(readed, out num))
                        {
                            if (int.TryParse(readed, out num))
                            {
                                DataRow[] find = DataEmp.sp_GetEmployes.Select("EmployeeID = " + num.ToString());
                                if (find == null)
                                {
                                    Console.WriteLine("Сотрудник не найден");
                                    Console.ReadLine();
                                    break;

                                }
                                else
                                {
                                    find[0].Delete();
                                    sp_GetEmployesTableAdapter.Update(DataEmp.sp_GetEmployes);
                                    Console.WriteLine("");

                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            
            
        }
    }
}

catch (Exception)
{
    Console.WriteLine("Нет файла настроек appsettings.json");
    Console.ReadLine(); return;
}


