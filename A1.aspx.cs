using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace SF_A1
{
    public partial class A1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendBtn_Click(object sender, EventArgs e)
        {
            string txt = InputTextArea.InnerText;
            string[] commandList = txt.Split(new Char[] { '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
            int commandCount = commandList.Count();
            bool missingInstallCommand = false;
            string dependenciesList = "";
            string OutputMessages="";
            if (string.Compare(commandList[commandCount - 1], "END") == 1)
            { 
                OutputTextArea.InnerText = "Wrong Command. Need END command.";
                InputTextArea.InnerText = "";
            }
            else
            {
                //lOOP
                for (var i = 0; i <= commandCount-1; i++)
                {
                    var command = commandList[i].Split(new[] { ' ' },2);
                    switch (command[0])
                    {
                        case "DEPEND":
                            missingInstallCommand = true;
                            dependenciesList = dependenciesList + ','+command[1];
                            break;
                        case "INSTALL":
                            if (!missingInstallCommand)
                            {
                                OutputMessages += "\n Wrong Command. Need DEPEND command first";                                
                            }
                            else
                            {
                                OutputMessages += "\n  " + DependecyAdd(dependenciesList);
                                OutputMessages += "\n " + command; //INSTALL
                                OutputMessages += "\n " + SoftwareInstall(command[1]); //INSTALLING
                            }
                            break;
                        case "REMOVE":
                            OutputMessages += "\n " + command; //REMOVE
                            OutputMessages += "\n  " + SoftwareRemove(command[1]); //REMOVING
                            break;
                        case "LIST":
                            OutputMessages += "\n " + command; //LIST
                            OutputMessages += "\n " + SoftwareList();//LIST
                            break;
                        case "END":
                            break;
                        default:
                            OutputMessages += "\n Wrong Command. Unkown command.";
                            break;
                    }
                }

                OutputTextArea.InnerText = OutputMessages;
               // InputTextArea.InnerText = "";
            }
        }
        public static DataSet Execute(string dbObject, IEnumerable<SqlParameter> dbParams)
        {
            DataSet ds = new DataSet();

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand(dbObject, cn) { CommandType = CommandType.StoredProcedure };
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    da.SelectCommand.Parameters.Add(dbParam);
                }
            }
            da.Fill(ds);
            return ds;
        }
        public static SqlParameter MakeParam(string paramName, SqlDbType dbType, int size, object objValue)
        {
            SqlParameter param;

            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);
            else
                param = new SqlParameter(paramName, dbType);

            param.Value = objValue;

            return param;
        }

        public static string ConvertDStoOutputMessage(DataSet ds)
        {
            string outputMessage = "";
            foreach (DataRow row in ds.Tables)
            {
                outputMessage += "\n" + row["message"];
            }
            return outputMessage;
        }
        public static string DependecyAdd(string dependenciesList)
        {
            SqlParameter[] dbParams = new[] {
                MakeParam("@dependenciesList", SqlDbType.VarChar, 500, dependenciesList)
            };
            DataSet ds = Execute("usp_DependecyAdd", dbParams);
            return ConvertDStoOutputMessage(ds);
        }
        public static string SoftwareInstall(string software)
        {
            SqlParameter[] dbParams = new[] {
                MakeParam("@software", SqlDbType.VarChar, 500, software)
            };
            var ds = Execute("usp_SoftwareInstall", dbParams);
            return ConvertDStoOutputMessage(ds);
        }
        public static string SoftwareRemove(string software)
        {
            SqlParameter[] dbParams = new[] {
                MakeParam("@software", SqlDbType.VarChar, 500, software)
            };
            var ds = Execute("usp_SoftwareRemove", dbParams);
            return ConvertDStoOutputMessage(ds);
        }
        public static string SoftwareList()
        {
            var ds = Execute("usp_SoftwareList", null);
            return ConvertDStoOutputMessage(ds);
        }

    }
}