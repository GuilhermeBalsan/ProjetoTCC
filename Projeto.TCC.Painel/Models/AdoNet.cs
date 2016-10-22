using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Projeto.TCC.Painel.Models
{
    public class AdoNet
    {

        private string sqlConn = WebConfigurationManager.ConnectionStrings["ProjetoTCCPainelContext"].ToString();

        public DataTable ExecProcedure(int questionarioId)
        {

            using (SqlConnection conn = new SqlConnection(sqlConn))
            {

                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = new SqlCommand("RelatorioQuestionario", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.Add(new SqlParameter("@QuestionarioID", SqlDbType.Int)).Value = questionarioId;

                    DataSet ds = new DataSet();
                    da.Fill(ds, "RelatorioQuestionario");

                    DataTable dt = ds.Tables["RelatorioQuestionario"];

                    return dt;
                }

            }

        }
    }
}

