using MediatR;
using MiniSalesApp.Application.Materials.Commands.CreateMaterial;
using MiniSalesApp.Application.Materials.Queries.GetMaterial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSalesApp.Application.Materials.Dtos;

namespace MiniSalesApp
{
    public partial class Form1 : Form
    {
        public readonly IMediator _mediator;
        public MaterialDto Material { get; set; }
        public Form1(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;
        }

        private void FillMaterial()
        {
            Material = new MaterialDto();
            Material.Code = Convert.ToInt32(txtCode.Text);
            Material.Name = txtName.Text;
            Material.SellPrice = Convert.ToDecimal(txtSellPrice.Text);
        }

        private bool ValidateMaterial()
        {
            StringBuilder msg = new StringBuilder();

            if (string.IsNullOrEmpty(Material.Name))
                msg.AppendLine(Messages.NameIsRequired);

            if (Material.Code <= 0)
                msg.AppendLine(Messages.NameIsRequired);

            if (Material.SellPrice <= 0)
                msg.AppendLine(Messages.NameIsRequired);

            if (msg.Length > 0)
            { 
                Program.DisplayMessage(msg.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            FillMaterial();

            if (!ValidateMaterial())
                return;
            
            var saveResult = await _mediator.Send(new CreateMaterialCommand()
            {
                Material = new MaterialDto()
                {
                    Code = Convert.ToInt32(txtCode.Text),
                    Name = txtName.Text,
                    SellPrice = Convert.ToDecimal(txtSellPrice.Text)
                }
            });

            if (saveResult.IsFailure)
            {
                Program.DisplayMessage(saveResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var getResult = await _mediator.Send(new GetMaterialQuery());

            grdVwMaterial.DataSource = getResult;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var getResult = await _mediator.Send(new GetMaterialQuery());

            grdVwMaterial.DataSource = getResult;
        }
    }
}
