using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

namespace threeTierApp
{
    public partial class AddEmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDept();
                fillGrid();
            }

        }

        public void fillDept()
        {
            deptTbl dept = new deptTbl();
            ddlDept.DataSource = dept.getAll();
            ddlDept.DataValueField = "Id";
            ddlDept.DataTextField = "deptName";
            ddlDept.DataBind();
        }

        public void fillGrid()
        {
            empTbl e = new empTbl();
            List<empTbl> emp = e.getAll();
            GridView1.DataSource = emp;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            empTbl emp = new empTbl();
            emp.empName = txtEmpNm.Text;
            emp.empAge = Convert.ToInt16(txtAge.Text);
            emp.empSalary = Convert.ToDecimal(txtSalary.Text);
            emp.deptId = Convert.ToInt16(ddlDept.SelectedItem.Value);
            emp.AddEmp(emp);

            txtAge.Text = txtEmpNm.Text = txtSalary.Text = "";
            fillGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["Id"]);
                empTbl emp = new empTbl();
                emp.Id = id;
                emp.Delemp(emp);
                fillGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            fillGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["Id"]);
                string empnm = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEmpName")).Text;

                TextBox empage = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEmpAge");
                TextBox empsal = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEmpSalary");
                int deptid = Convert.ToInt16(((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlDept")).SelectedItem.Value);

                empTbl emp = new empTbl();
                emp.Id = id;
                emp.empAge = Convert.ToInt16(empage.Text);
                emp.empName = empnm;
                emp.empSalary = Convert.ToDecimal(empsal.Text);
                emp.deptId = deptid;

                emp.UpdateEmp(emp);
                GridView1.EditIndex = -1;
                fillGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                HiddenField deptVal = (HiddenField)e.Row.FindControl("hDeptnm");
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlDept");

                deptTbl dept = new deptTbl();
                ddl.DataSource = dept.getAll();
                ddl.DataValueField = "Id";
                ddl.DataTextField = "deptName";
                ddl.DataBind();

                ddl.SelectedValue = deptVal.Value;

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
                    GridView1.PageIndex = e.NewPageIndex;
                    fillGrid();
        }

        public string GridViewSortExpression
        {
               get
               {
                   return ViewState["GridViewSortExpression"] == null ? "FirstName" : ViewState["GridViewSortExpression"] as string;
               }
            set
               {
                   ViewState["GridViewSortExpression"] = value;
               }
           }
 
           /// <summary>
           /// for Sorting Direction
           /// </summary>
           public SortDirection GridViewSortDirection
            {
                get
                {
                    if (ViewState["sortDirection"] == null)
                        ViewState["sortDirection"] = SortDirection.Ascending;

                    return (SortDirection)ViewState["sortDirection"];
                }
                set { ViewState["sortDirection"] = value; }
            }


            protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
            {
                GridViewSortExpression = e.SortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    empTbl em = new empTbl();
                    List<empTbl> emp = em.getAll();
                    GridView1.DataSource =emp.OrderBy(x => x.GetType().GetProperty(GridViewSortExpression).GetValue(x, null)).ToList();
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    empTbl em = new empTbl();
                    List<empTbl> emp = em.getAll();
                    GridView1.DataSource = emp.OrderBy(x => x.GetType().GetProperty(GridViewSortExpression).GetValue(x, null)).ToList();
                }
          GridView1.DataBind();


        }


    }
}