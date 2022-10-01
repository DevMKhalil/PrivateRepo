using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSalesApp.Classes
{
    public enum FormStates
    {
        NormalFirstoaded = 1,
        AddingNew = 2,
        Editing = 3
    }

    public enum BtnType
    {
        New = 1,
        Save = 2,
        Delete = 3,
        Refresh = 4,
        Open = 5,
        Reset = 6,
        FindWarehouseItem = 7,
        Print = 8,
        PrintBarCode = 9,
        Copy = 10,
        Other = 11
    }
    class ModuleRibbonButton
    {
        private DevExpress.XtraBars.BarButtonItem button;
        private List<FormStates> EnabledStates;
        private BtnType BtnType;

        private string ObjectArabicName;
        private string ObjectEnglishName;

        public ModuleRibbonButton(DevExpress.XtraBars.BarButtonItem btn, BtnType type, string arabicName, string englishName, params FormStates[] enabledStates)
            : this(btn, enabledStates)
        {

            BtnType = type;
            ObjectArabicName = arabicName;
            ObjectEnglishName = englishName;

            if (BtnType != 0)
            {
                SuperToolTip sToolTip = new SuperToolTip();
                SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();

                switch (BtnType)
                {
                    case (BtnType.Delete):
                        {
                            args.Contents.Text = "Shift+Delete";
                            args.Title.Text = "حذف";
                            btn.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Shortcut.ShiftDel);
                        }
                        break;
                    case (BtnType.FindWarehouseItem):
                        {
                            args.Contents.Text = "F3";
                            args.Title.Text = "بحث";
                        }
                        break;
                    case (BtnType.New):
                        {
                            args.Contents.Text = "Ctrl+N";
                            args.Title.Text = "جديد";
                            btn.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Shortcut.CtrlN);
                        }
                        break;
                    case (BtnType.Open):
                        {
                            args.Contents.Text = "Ctrl+O";
                                args.Title.Text = "فتح";
                            btn.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Shortcut.CtrlO);
                        }
                        break;
                    case (BtnType.Print):
                        {
                            args.Contents.Text = "Ctrl+P";
                            args.Title.Text = "طباعه";
                        }
                        break;
                    case (BtnType.Refresh):
                        {
                            args.Contents.Text = "F5";
                            args.Title.Text = "اعاده التحميل";
                        }
                        break;
                    case (BtnType.Reset):
                        {
                            args.Contents.Text = "Ctrl+R";
                            args.Title.Text = "تراجع";
                            btn.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Shortcut.CtrlR);
                        }
                        break;
                    case (BtnType.Save):
                        {
                            args.Contents.Text = "Ctrl+S";
                            args.Title.Text = "حفظ";
                            btn.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Shortcut.CtrlS);
                        }
                        break;
                    case (BtnType.PrintBarCode):
                        {
                            args.Title.Text = "طباعه الباركود";
                        }
                        break;
                    case (BtnType.Copy):
                        {
                            args.Contents.Text = "Ctrl+Shift+C";
                            args.Title.Text = "نسخ";
                        }
                        break;
                    case (BtnType.Other):
                        {
                            args.Contents.Text = btn.ItemShortcut.DisplayString;
                            args.Footer.Text = ObjectArabicName;
                        }
                        break;
                }
                sToolTip.Setup(args);
                button.SuperTip = sToolTip;
            }
        }

        public ModuleRibbonButton(DevExpress.XtraBars.BarButtonItem btn, params FormStates[] enabledStates)
        {
            button = btn;
            EnabledStates = enabledStates.ToList();
        }

        public static void SetButtonsState(List<ModuleRibbonButton> formButtons, FormStates state)
        {
            foreach (ModuleRibbonButton btn in formButtons)
            {
                if (btn.EnabledStates.Count == 0)
                {
                    continue;
                }
                btn.button.Enabled = btn.EnabledStates.Exists(u => u == state);
            }
        }
    }
}
