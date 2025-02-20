using Register.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Register.BAL
{
    public class BAL_bus_wise_driver
    {
       
        public static int insert_bus_wise_route_wise_driver(int bus_id, int route_id, int driver_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_bus_wise_route_wise_driver";
            cmd.Parameters.Add(para.IntInputPara("@brd_bus_id", bus_id));
            cmd.Parameters.Add(para.IntInputPara("@brd_route_id", route_id));
            cmd.Parameters.Add(para.IntInputPara("@brd_driver_id", driver_id));

            
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        
        public static DataTable get_bus_wise_route_wise_driver()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_bus_wise_route_wise_driver";

           
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_bus_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_bus_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_route_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_route_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_driver_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_driver_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable edit_bus_wise_route_wise_driver(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_bus_wise_driver";
            cmd.Parameters.AddWithValue("@brd_id", id);
            return command.ExecuteQuery(cmd);
        }

        public static int update_bus_wise_route_wise_driver(int brd_id, int bus_id, int route_id, int driver_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_bus_wise_route_wise_driver";
            cmd.Parameters.Add(para.IntInputPara("@brd_id", brd_id));
            cmd.Parameters.Add(para.IntInputPara("@brd_bus_id", bus_id));
            cmd.Parameters.Add(para.IntInputPara("@brd_route_id", route_id));
            cmd.Parameters.Add(para.IntInputPara("@brd_driver_id", driver_id));
            return command.ExecuteNonQuery1(cmd);
        }

        public static int delete_bus_wise_route_wise_driver(int brd_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_bus_wise_route_wise_driver";
            cmd.Parameters.Add(para.IntInputPara("@brd_id", brd_id));
            return command.ExecuteNonQuery1(cmd);
        }

    }
}
