using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiggleBasketWebPage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                DropDownList drop = Page.FindControl("DropDownAddOptions") as DropDownList;
                int index = drop.SelectedIndex;
                switch (index)
                {
                    case 1:
                        BuyItemPanelLoad();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }

            }
            else
            {
                List<string> ddl = new List<string>() { "Add Item to Buy", "Add Gift to Buy", "Apply gift voucher", "Apply offer voucher" };
                populateDropDownList("DropDownAddOptions", ddl);
            }
        }

        private void BuyItemPanelLoad()
        {
            TextBox qty = new TextBox();
            qty.Text = "Quontity";
            qty.ID = "tb_qty";
            pnlGetInfo.Controls.Add(qty);
            TextBox name = new TextBox();
            name.Text = "Product Name";
            name.ID = "tb_name";
            pnlGetInfo.Controls.Add(name);
            TextBox subset = new TextBox();
            subset.Text = "Product SubSet";
            subset.ID = "tb_subset";
            pnlGetInfo.Controls.Add(subset);
            TextBox value = new TextBox();
            value.Text = "Product Value";
            value.ID = "tb_value";
            pnlGetInfo.Controls.Add(value);
            btn_Add.Visible = true;
        }


        protected void populateDropDownList<T>(string controlID, List<T> datasource)
        {
            DropDownList ddl = Page.FindControl(controlID) as DropDownList;
            ddl.DataSource = datasource;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("choose what to add", "NOT SELECTED"));
            ddl.SelectedIndex = 0;
        }
    }
}