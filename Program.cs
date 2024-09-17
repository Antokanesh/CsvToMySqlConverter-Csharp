using System;
using System.IO;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace CsvToMysql{
    class Program{
        static void Main(string[] args){
            string FilePath = @"D:\Employee_Details.csv";
            string MysqlInform = "server=localhost;user=root;database=employee;password=Antony";
            try{
                using (MySqlConnection conn = new MySqlConnection(MysqlInform))
                {
                    conn.Open();
                    using(StreamReader Datas=new StreamReader(FilePath)){
                        string line;
                        bool isHeader=true;
                        while((line=Datas.ReadLine())!=null)
                        {
                            if(isHeader)
                            {
                                isHeader=false;
                                continue;
                            }
                            string[] parts=line.Split(',');
                            string query="insert into EmployeeInfo(Employee_Name,Employee_City)"+"values(@Employee_Name,@Employee_City)";

                            using (MySqlCommand cmd = new MySqlCommand(query,conn))
                            {
                              cmd.Parameters.AddWithValue("@Employee_Name",parts[0]);
                              cmd.Parameters.AddWithValue("@Employee_City",parts[1]);
                              cmd.ExecuteNonQuery(); 
                            }
                        }
                    }
                    Console.WriteLine("Data Sucessfully transferred to Mysql");
                }
                
            }
            catch (Exception ex){
                Console.WriteLine("Error{0}",ex.Message);
            }
        }
    }
}