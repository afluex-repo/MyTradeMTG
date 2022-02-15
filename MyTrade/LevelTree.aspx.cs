using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyTrade.Models;

namespace MyTrade
{
    public partial class LevelTree : System.Web.UI.Page
    {
        public DataSet dsResult = new DataSet();
        Tree obj = new Tree();
        int Count = 0;
        int i = 1;
        DataTable dtParent = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Pk_userId"] == null)
            {
                Response.Redirect("/Home/Login");
            }
            dtParent.Columns.Add("ParentId", typeof(string));
            dtParent.Columns.Add("Id", typeof(string));

            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = dtParent.Columns["Id"];
            dtParent.PrimaryKey = keyColumns;
            DataRow dr1 = dtParent.NewRow();
            dr1["ParentId"] = 0;
            dr1["Id"] = i;
            dtParent.Rows.Add(dr1);
            GetDirectData();
        }

        public void GetDirectData()
        {
            try
            {
                obj.RootAgentCode = Session["LoginId"].ToString();
                obj.LoginId = Session["LoginId"].ToString();
                dsResult = obj.GetLevelTreeData();
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    trvBroker.Nodes.Clear();
                    TreeNode ParentNode = new TreeNode();
                    ParentNode.Value = dsResult.Tables[0].Select("LoginId = '" + obj.LoginId + "'").CopyToDataTable().Rows[0]["Pk_UserId"].ToString();
                    ParentNode.Text = dsResult.Tables[0].Select("LoginId = '" + obj.LoginId + "'").CopyToDataTable().Rows[0]["AssociateMemberName"].ToString();
                    trvBroker.Nodes.Add(ParentNode);
                    BindTree(ParentNode, dsResult);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void BindTree(TreeNode ParentNode, DataSet DsData)
        {

            foreach (DataRow d in dsResult.Tables[0].Select("Parentid  = " + ParentNode.Value))
            {

                if (dtParent.Rows.Contains(ParentNode.Value) == false)
                {
                    Count = Count + 1;
                    i = i + 1;
                    DataRow dr1 = dtParent.NewRow();
                    dr1["ParentId"] = ParentNode.Value;
                    dr1["Id"] = i;
                    dtParent.Rows.Add(dr1);
                }
                TreeNode ChildNode = new TreeNode();
                if (ParentNode.Value != obj.LoginId)
                    ParentNode.ImageUrl = ParentNode.ImageUrl;
                ChildNode.Value = d["PK_UserId"].ToString();
                ChildNode.Text = d["AssociateMemberName"].ToString();
                // ChildNode.ToolTip = ChildNode.Text;
                ParentNode.ChildNodes.Add(ChildNode);
                BindTree(ChildNode, dsResult);
                trvBroker.DataBind();
            }
        }

        void Data_Bound(Object sender, TreeNodeEventArgs e)
        {
            string Prefix = "<div style='display:none;' class='content'>Some Content</div>";
            e.Node.Text = Prefix + e.Node.Text;
        }
    }
}