using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avSV = ApplicationVariables.AV.SystemValues;
using bl = WiggleBusinessLogic.BusLayer;
using dl = WiggleData.DataLayer;
using cl = WiggleClasses;

namespace WiggleBasketWebPage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack == true)
            //{

            //}
            //else
            //{
            //    bl bl1 = new bl();
            //    populateDropDownList(avSV.DropDownLists.Vouchers.ControlID, bl1.GetItems);
            //}
        }

        protected void rblCreateChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = rblCreateChoice.SelectedValue;
            
            switch (selectedValue)
            {
                case avSV.RadioButtonList.Gift:
                    panelGift.Visible = true;
                    panelItem.Visible = false;
                    panelOffer.Visible = false;
                    btnApply.Enabled = true;
                    btnBuy.Enabled = true;
                    break;
                case avSV.RadioButtonList.Item:
                    panelGift.Visible = false;
                    panelItem.Visible = true;
                    panelOffer.Visible = false;
                    btnApply.Enabled = false;
                    btnBuy.Enabled = true;
                    break;
                case avSV.RadioButtonList.Offer:
                    panelGift.Visible = false;
                    panelItem.Visible = false;
                    panelOffer.Visible = true;
                    btnApply.Enabled = true;
                    btnBuy.Enabled = false;
                    break;
            }
            //btnApply.Visible = true; // TODO check if its not possible 
            //btnBuy.Visible = true;
        }

        //protected void btnAddNew_Click(object sender, EventArgs e) //TODO remove or keep
        //{

        //}

        private void AddRow()
        {
            // add a row with cells with the approprioate values
        }

        private void qtyChanged(string tbID, int qty)
        {
            //change qty in basket
            //CalcTotal
            //update Total
        }

        private void TableHeaders()
        {
            tblBasket.Visible = true;
            TableHeaderRow header = new TableHeaderRow();
            header.Font.Bold = true;

            for (int i = 0; i < avSV.TableValues.HeaderCells.Count; i++)
            {
                TableCell cell = new TableCell();
                cell.Text = avSV.TableValues.HeaderCells[i];
                header.Cells.Add(cell);
            }
            tblBasket.Rows.Add(header);
        }



        protected void btnBuy_Click(object sender, EventArgs e)
        {
            if (!tblBasket.Visible) TableHeaders();
            

        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (!tblBasket.Visible) TableHeaders();
            //TODO check if values are not filled and place random from the datalayer lists if ()
        }

        //protected void populateDropDownList<T>(string controlID, List<T> datasource)
        //{
        //    DropDownList ddl = Page.FindControl(controlID) as DropDownList;
        //    ddl.DataSource = datasource;
        //    ddl.DataBind();
        //    ddl.Items.Insert(0, new ListItem("choose what to add", "NOT SELECTED"));
        //    ddl.SelectedIndex = 0;
        //}


    }
}