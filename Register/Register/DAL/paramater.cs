using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Register.DAL
{
    public class parameter
    {
        //int params
        public SqlParameter IntInputPara(string pname, int pvalue)
        {
            SqlParameter Parameter = new SqlParameter();

            Parameter.DbType = DbType.Int32;
            Parameter.Direction = ParameterDirection.Input;
            Parameter.ParameterName = pname;
            Parameter.Value = pvalue;
            return Parameter;
        }
        public SqlParameter IntOutputPara(string pname)
        {
            SqlParameter Parameter = new SqlParameter();

            Parameter.DbType = DbType.Int32;
            Parameter.Direction = ParameterDirection.Output;
            Parameter.ParameterName = pname;
            return Parameter;
        }

        //string params

        public SqlParameter StringInputPara(string pname, string pvalue)
        {
            SqlParameter para = new SqlParameter();
            para.DbType = DbType.String;
            para.Direction = ParameterDirection.Input;
            para.ParameterName = pname;
            para.Value = pvalue;
            return para;
        }

        public SqlParameter StringOutputPara(string pname)
        {
            SqlParameter Parameter = new SqlParameter();

            Parameter.DbType = DbType.String;
            Parameter.Size = 300;
            Parameter.Direction = ParameterDirection.Output;
            Parameter.ParameterName = pname;
            return Parameter;
        }

        public SqlParameter DecimalInputPara(string pname, decimal pvalue)
        {
            SqlParameter para = new SqlParameter();
            para.DbType = DbType.Decimal;
            para.Direction = ParameterDirection.Input;
            para.ParameterName = pname;
            para.Value = pvalue;
            return para;
        }

        public SqlParameter DecimalOutputPara(string pname)
        {
            SqlParameter Parameter = new SqlParameter();

            Parameter.DbType = DbType.Decimal;
            Parameter.Size = 300;
            Parameter.Direction = ParameterDirection.Output;
            Parameter.ParameterName = pname;
            return Parameter;
        }
        public SqlParameter TimeSpanInputPara(string pname, TimeSpan pvalue)
        {
            SqlParameter Parameter = new SqlParameter();

            Parameter.DbType = DbType.Time; 
            Parameter.Direction = ParameterDirection.Input;
            Parameter.ParameterName = pname;
            Parameter.Value = pvalue;
            return Parameter;
        }
    }
}