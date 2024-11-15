using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLCnsole
{
    public static class ShowEmploys
    {
        public static void ShowEmploy(DataTable data)
        {
            int[] arrayLength = new int[data.Columns.Count];
            string rowdata = "";
            int maxLength = 0;
            for (int j = 0; j < data.Columns.Count; j++)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][j].ToString().Trim().Length > maxLength)
                        maxLength = data.Rows[i][j].ToString().Trim().Length;
                    if (data.Columns[j].ColumnName.ToString().Trim().Length > maxLength)
                        maxLength = data.Columns[j].ColumnName.ToString().Trim().Length;
                }
                arrayLength[j] = maxLength;
            }

            rowdata = "";
            for (int j = 0; j < data.Columns.Count; j++)
            {
                rowdata = rowdata + "|"+(data.Columns[j].ColumnName + "(" + j.ToString() + ")").PadRight(arrayLength[j] + 3) ;
                


            }
            
            Console.WriteLine(rowdata);
            for (int i = 0; i < data.Rows.Count; i++)
            {
               
                rowdata = "";
                for (int j = 0; j < data.Columns.Count; j++)                
                {
                    rowdata = rowdata + "|"+data.Rows[i][j].ToString().Trim().PadRight(arrayLength[j] + 3) + "";
                }
           
                Console.WriteLine(rowdata);
            }
        }
   }
}
