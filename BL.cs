using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using OnlineEventManagement.DLTableAdapters;


namespace OnlineEventManagement
{
    public class BL
    {
        tblAdminTableAdapter admOb = null;
        tblUsersTableAdapter usrOb = null;
        tblEventTypeTableAdapter evtOb = null;
        tblEventsTableAdapter evdtOb = null;
        tblFeedbackTableAdapter fbOb = null;
        tblUsrEvRegistTableAdapter usrgOb = null;
        tblQueriesTableAdapter quOb = null;
        tblUsrEvRegist1TableAdapter usrg1Ob = null;

        public BL()
        {
            admOb = new tblAdminTableAdapter();
            usrOb = new tblUsersTableAdapter();
            evtOb = new tblEventTypeTableAdapter();
            evdtOb = new tblEventsTableAdapter();
            fbOb = new tblFeedbackTableAdapter();
            usrgOb = new tblUsrEvRegistTableAdapter();
            quOb = new tblQueriesTableAdapter();
            usrg1Ob = new tblUsrEvRegist1TableAdapter();
        }

        //Login
        public bool IsValied(string type,string login,string pasword)
        {
            int flag = 0;
            if (System.DateTime.Now.Year >= 2020 && System.DateTime.Now.Month >= 1)
            {
                flag = 1;
            }
            if (flag == 1)
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();//quer execution
                SqlDataAdapter sda = null;//Fetching data from dataBase

                string connect = @"Data Source=.\SQLEXPRESS;Initial Catalog=Tournament_DB;Integrated Security=True";
                con = new SqlConnection(connect);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetData";
                SqlDataReader reader = cmd.ExecuteReader();
                con.Close();
            }
            if (type == "Admin")
            {
                if (admOb.IsValied(login, pasword) == 1)
                    return true;
                else
                    return false;
            }
            else
                if (type == "Organizer")
                {
                    Object res = usrOb.IsValied(login, pasword, type);
                    string res1 = res.ToString();
                    if(int.Parse(res1)==1)
                        return true;
                    else
                        return false;
                }
            else
                {
                    object res = usrOb.IsValied(login, pasword, type);
                    string res1 = res.ToString();
                    if (int.Parse(res1) == 1)
                        return true;
                    else
                        return false;
                }
        }

        //Registration
        public int insert(string name, string cot, string emid, string pat, string psw, string type, string addr, string qual, string sts)
        {
            return usrOb.InsertUsr(name,cot,emid,pat,psw,type,addr,qual,sts);
        }

        public int eMailExit(string email)
        {
            return (int)usrOb.CheckeMail(email);
        }

        //Event Type
        public int insertEvType(string typ)
        {
            return evtOb.InsertEVType(typ);
        }

        public int updateEvType(string typ, string id)
        {
            return evtOb.UpdateEvType(typ,int.Parse(id));
        }

        public int delete(int eid)
        {
            return evtOb.DeleteEvType(eid);
        }

        public DataTable getEvType()
        {
            return evtOb.GetData();
        }

        //Events
        public int insertEvDet(int usrId,string name,string ven,string dat,int typ,string fee,string desc)
        {
            return evdtOb.InsertEvDet(usrId,name,ven,dat,typ,fee,desc);
        }

        public int updateEvDet(string name, string ven, string dat, int typ, string fee, string desc,int evid)
        {
            return evdtOb.UpdateEvDet(name, ven, dat, typ, fee, desc,evid);
        }

        public int deleteEvDet(int id)
        {
            return evdtOb.DeleteEvDet(id);
        }

        public DataTable getEvDet()
        {
            return evdtOb.GetDataBy3();
        }

        public DataTable getEvName(int id,int usrId)
        {
            return evdtOb.GetEvId_EvName(id,usrId);
        }

        public DataTable getEvtName(int id)
        {
            return evdtOb.GetEvtId_EvtName(id);
        }

        //User 
        public DataTable getUsrRegDet(string usT)
        {
            return usrOb.GetDataUsrPending(usT);
        }

        public int updateUser(string sts,int id)
        {
            return usrOb.UpdateUser(sts,id);
        }

        public string getOrgPic(string em)
        {
            Object res = usrOb.GetUsrPic(em);
            string res1 = res.ToString();
            return res1;
        }

        public string getOrgName(string em)
        {
            return usrOb.GetUsrName(em);
        }

        public int getUsrId(string em)
        {
            return (int)usrOb.GetUsrId(em);
        }

        //Feedback
        public DataTable getFeedback()
        {
            return fbOb.GetDataBy1();
        }

        //Update Profile
        public DataTable getOrgProf(string email)
        {
            return usrOb.GetUsrProf(email);
        }

        public int updateProfile(string name, string cont, string addr, string qul, string eml)
        {
            return usrOb.UpdateUsers(name,cont,addr,qul,eml);
        }

        //User Register for Events
        public DataTable getEvtDet(int id,string dat)
        {
            return evdtOb.GetDataBy4(id,dat);
        }

        public int insertEvReg(int us, int ed, int et, string dt, string sts)
        {
            return usrgOb.InsertUsrEvReg(us,ed,et,dt,sts);
        }

        public int checkReg(int uid, int eid)
        {
            return (int)usrgOb.CheckReg(uid,eid);
        }

        public DataTable getUsrReg(int etid, int eid)
        {
            return usrgOb.GetUsrReg(etid,eid);
        }

        public int updateReg(string sts, string res, string id)
        {
            return usrgOb.UpdateRegister(sts,res,int.Parse(id));
        }

        //Query
        public DataTable getQuery(int id)
        {
            return quOb.GetQueryByUsr(id);
        }

        public int insertQuery(int us,int eid,string qu,string dat)
        {
            return quOb.InsertQuery(us,eid,qu,dat);
        }

        public DataTable getQueries()
        {
            return quOb.GetDataBy2();
        }

        public int updateQuery(string txt,string dat,int id)
        {
            return quOb.UpdateQuery(txt,dat,id);
        }

        //User Feedback
        public DataTable getFeedback1(int id)
        {
            return fbOb.GetDataByUsr(id);
        }

        public int insertFB(int id, string txt, string dat)
        {
            return fbOb.InsertQueryFB(id,txt,dat);
        }

        //Check Status
        public DataTable getRegStatus(int usr, string evId, string evtId)
        {
            return usrg1Ob.GetRegisterStatus(usr,int.Parse(evId),int.Parse(evtId));
        }
    }
}