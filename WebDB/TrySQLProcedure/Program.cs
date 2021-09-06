using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrySQLProcedure.Properties;

namespace TrySQLProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            var newModel1 = new TestTable1()
            {
                ID = Guid.NewGuid(),
                Name = "TestData"
            };
            
            
        }
    
        static void Exec()
        {
            SqlConnection conn = new SqlConnection("data source=.\\SQLExpress;initial catalog=Northwind;integrated security=True");
            // data source=.\SQLExpress  => C#呼叫 要使用 data source=.\\SQLExpress
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "CreateEmployee"; // Procedure Name
            cmd.CommandType = System.Data.CommandType.StoredProcedure; 

            //cmd.Parameters.AddWithValue("@Param1", 500);
            //cmd.Parameters.AddWithValue("@Param2", 100);
            
            cmd.Parameters.AddWithValue("@LastName", "Smith");  // 加入Procedure所需的參數
            cmd.Parameters.AddWithValue("@FirstName", "John");

            var retParameter = new SqlParameter("@Return Value", SqlDbType.Int);  // 定義回傳值
            retParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retParameter);                   // 將此參數化查詢加入參數表

            conn.Open();
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            int newEmpID = Convert.ToInt32(cmd.Parameters["@Return Value"].Value);
            conn.Close();

            dt.Columns.Remove("Photo");

            //foreach(DataRow item in dt.Rows)
            //{
            //    Console.WriteLine($"{item[0]}, {item[1]}");
            //}

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            Console.WriteLine("new employeeID is" + newEmpID);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        static void Exec2() // 使用output取得Procedure中的多筆回傳值
        {
            SqlConnection conn = new SqlConnection("data source=.\\SQLExpress;initial catalog=Northwind;integrated security=True");
            // data source=.\SQLExpress  => C#呼叫 要使用 data source=.\\SQLExpress
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetOrderStatistics"; // Procedure Name
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MAXPrice", SqlDbType.Money) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@MINPrice", SqlDbType.Money) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@SUMPrice", SqlDbType.Money) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@AvgPrice", SqlDbType.Money) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@CNTPrice", SqlDbType.Money) { Direction = ParameterDirection.Output });

            conn.Open();
            cmd.ExecuteNonQuery();

            decimal MAXPrice = Convert.ToDecimal(cmd.Parameters["@MAXPrice"].Value);
            decimal MINPrice = Convert.ToDecimal(cmd.Parameters["@MINPrice"].Value);
            decimal SUMPrice = Convert.ToDecimal(cmd.Parameters["@SUMPrice"].Value);
            decimal AvgPrice = Convert.ToDecimal(cmd.Parameters["@AvgPrice"].Value);
            decimal CNTPrice = Convert.ToDecimal(cmd.Parameters["@CNTPrice"].Value);

            conn.Close();

            Console.WriteLine($"Max: {MAXPrice}, Min: {MINPrice }, Sum: {SUMPrice}, Avg: {AvgPrice}, CNT  { CNTPrice} ");
            Console.ReadLine();
        
        }
    }
}
