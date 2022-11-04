using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;

namespace SQLHelper
{
    public partial class frmQueryHelper : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        BindingList<ParamsTyps> ParamsTyps = new BindingList<ParamsTyps>();
        public frmQueryHelper()
        {
            InitializeComponent();
            rpsLkUpTypeName.DataSource = ParamTypeName.GetParamsTypeNameList();
        }

        private void btnGetParams_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(memoInPutQuery.Text))
            {
                MessageBox.Show("Enter Query");
                return;
            }
            string strToProccess = string.Copy(memoInPutQuery.Text);
            
            Regex regex = new Regex(string.Format(@"(?<!\w){0}\w+", '@'));
            MatchCollection res = regex.Matches(strToProccess);
            ParamsTyps = new BindingList<ParamsTyps>();
            foreach (Match m in res)
            {
                ParamsTyps.Add(new SQLHelper.ParamsTyps()
                {
                    ParamName = m.Value,
                    ParamSize = 0
                });
            }
            grdCtrParameters.DataSource = ParamsTyps;
            grdCtrParameters.RefreshDataSource();
            grdVwParameters.RefreshData();

            memoInPutQuery.ReadOnly = true;
        }

        private void grdVwParameters_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //ParamsTyps paramsTyps = grdVwParameters.GetFocusedRow() as ParamsTyps;
            //if (paramsTyps == null)
            //{
            //    return;
            //}
            //if (e.Column == colParamValue)
            //{
            //    if (string.IsNullOrEmpty(paramsTyps.ParamType))
            //    {
            //        e.RepositoryItem.ReadOnly = true;
            //    }
            //    else
            //    {
            //        e.RepositoryItem.ReadOnly = false;
            //    }

            //    if (paramsTyps.ParamType == "DECIMAL"
            //        || paramsTyps.ParamType == "FLOAT"
            //        || paramsTyps.ParamType == "MONEY")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueDecimal;
            //    }
            //    else if (paramsTyps.ParamType == "DATETIME")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueDateTime;
            //    }
            //    else if (paramsTyps.ParamType == "INT")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueInt;
            //    }
            //    else if (paramsTyps.ParamType == "BIT")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueBool;
            //    }
            //}
        }

        private void grdVwParameters_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            ParamsTyps paramsTyps = grdVwParameters.GetFocusedRow() as ParamsTyps;
            if (paramsTyps == null)
            {
                return;
            }
            if (e.Column == colParamSize)
            {
                if (string.IsNullOrEmpty(paramsTyps.ParamType))
                {
                    e.RepositoryItem.ReadOnly = true;
                }
                else if (paramsTyps.ParamType == "NVARCHAR" || paramsTyps.ParamType == "DECIMAL")
                {
                    e.RepositoryItem.ReadOnly = false;
                }
                else
                {
                    e.RepositoryItem.ReadOnly = true;
                }
            }

            if (e.Column == colParamSize2)
            {
                if (string.IsNullOrEmpty(paramsTyps.ParamType))
                {
                    e.RepositoryItem.ReadOnly = true;
                }
                else if (paramsTyps.ParamType == "DECIMAL")
                {
                    e.RepositoryItem.ReadOnly = false;
                }
                else
                {
                    e.RepositoryItem.ReadOnly = true;
                }
            }
            //if (e.Column == colParamValue)
            //{
            //    if (string.IsNullOrEmpty(paramsTyps.ParamType))
            //    {
            //        e.RepositoryItem.ReadOnly = true;
            //    }
            //    else
            //    {
            //        e.RepositoryItem.ReadOnly = false;
            //    }

            //    if (paramsTyps.ParamType == "DECIMAL" 
            //        || paramsTyps.ParamType == "FLOAT" 
            //        || paramsTyps.ParamType == "MONEY")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueDecimal;
            //    }
            //    else if (paramsTyps.ParamType == "DATETIME")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueDateTime;
            //    }
            //    else if (paramsTyps.ParamType == "INT")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueInt;
            //    }
            //    else if (paramsTyps.ParamType == "BIT")
            //    {
            //        e.RepositoryItem = rpsTxtParamValueBool;
            //    }
            //}
        }

        private void grdVwParameters_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            ParamsTyps paramsTyps = grdVwParameters.GetFocusedRow() as ParamsTyps;
            if (paramsTyps == null)
            {
                return;
            }
            if ((paramsTyps.ParamType == "NVARCHAR" || paramsTyps.ParamType == "DECIMAL") && paramsTyps.ParamSize <= 0)
            {
                e.ErrorText = "Insert Type Size";
                e.Valid = false;
            }
            if (paramsTyps.ParamType == "DECIMAL" && paramsTyps.ParamSize2 <= 0)
            {
                e.ErrorText = "Insert Type Size2";
                e.Valid = false;
            }
            //if (string.IsNullOrEmpty(paramsTyps.ParamValue))
            //{
            //    e.ErrorText = "Insert Parameter Value";
            //    e.Valid = false;
            //}
        }

        private void btnAddParamsToQuery_Click(object sender, EventArgs e)
        {
            if (ParamsTyps.Any(x => x.ParamType == "NVARCHAR" && x.ParamSize <= 0))
            {
                MessageBox.Show("Insert Size For (NVARCHAR)");
                return;
            }
            if (ParamsTyps.Any(x => x.ParamType == "DECIMAL" && x.ParamSize <= 0))
            {
                MessageBox.Show("Insert Size For (DECIMAL)");
                return;
            }
            if (ParamsTyps.Any(x => x.ParamType == "DECIMAL" && x.ParamSize2 <= 0))
            {
                MessageBox.Show("Insert Size For (DECIMAL)");
                return;
            }
            List<string> strParams = new List<string>();
            foreach (var item in ParamsTyps)
            {
                if (item.ParamType == "NVARCHAR")
                {
                    //strParams.Add($"DECLARE {item.ParamName} {item.ParamType}({item.ParamSize}) = {item.ParamValue};");
                    strParams.Add($"DECLARE {item.ParamName} {item.ParamType}({item.ParamSize}) = ;");
                }
                else if (item.ParamType == "DECIMAL")
                {
                    //strParams.Add($"DECLARE {item.ParamName} {item.ParamType}({item.ParamSize},{item.ParamSize2}) = {item.ParamValue};");
                    strParams.Add($"DECLARE {item.ParamName} {item.ParamType}({item.ParamSize},{item.ParamSize2}) = ;");
                }
                else
                {
                    //strParams.Add($"DECLARE {item.ParamName} {item.ParamType} = {item.ParamValue};");
                    strParams.Add($"DECLARE {item.ParamName} {item.ParamType} = ;");
                }
            }
            string strTxt = string.Join("\n", strParams);
            memofinalQuery.Text = $"{strTxt}\n\n{memoInPutQuery.Text}";
        }

        private void brBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            memoInPutQuery.Text = string.Empty;
            memoInPutQuery.ReadOnly = false;
            grdCtrParameters.DataSource = new BindingList<ParamsTyps>();
            memofinalQuery.Text = string.Empty;
        }

        private void rpsTxtParamSize_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
            {
                e.Cancel = true;
            }
        }

        private void rpsTxtParamSize2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
            {
                e.Cancel = true;
            }
        }

        private void rpsTxtParamValueInt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
            {
                e.Cancel = true;
            }
        }

        private void rpsTxtParamValueDecimal_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()) && e.NewValue.ToString().Contains("-"))
            {
                e.Cancel = true;
            }
        }

        private void rpsTxtParamValueDateTime_EditValueChanged(object sender, EventArgs e)
        {
            //ParamsTyps paramsTyps = grdVwParameters.GetFocusedRow() as ParamsTyps;
            //if (paramsTyps == null)
            //{
            //    return;
            //}
            //paramsTyps.ParamValue = (sender as DateEdit).DateTime.ToString();
            //grdVwParameters.RefreshData();
        }
    }
    class ParamsTyps
    {
        public string ParamName { get; set; }
        public string ParamType { get; set; }
        public int ParamSize { get; set; }
        public int ParamSize2 { get; set; }
        //public string ParamValue { get; set; }
    }
    class ParamTypeName
    {
        public static List<ParamTypeName> GetParamsTypeNameList()
        {
            List<ParamTypeName> list = new List<ParamTypeName>();
            list.Add(new ParamTypeName("NVARCHAR"));
            list.Add(new ParamTypeName("DATETIME"));
            list.Add(new ParamTypeName("BIT"));
            list.Add(new ParamTypeName("INT"));
            list.Add(new ParamTypeName("FLOAT"));
            list.Add(new ParamTypeName("DECIMAL"));
            list.Add(new ParamTypeName("MONEY"));
            return list;
        }
        public ParamTypeName(string typeName)
        {
            ParamTypeNameStraing = typeName;
        }
        public string ParamTypeNameStraing { get; set; }
    }
}