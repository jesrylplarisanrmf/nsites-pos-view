using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Xml;
using System.Net;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.POSs;
using NSites_V.ApplicationObjects.Classes.Systems;

using NSites_V.ApplicationObjects.UserInterfaces;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports;

using NSites_V.ApplicationObjects.UserInterfaces.POSs.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.POSs.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.POSs.Reports;

using NSites_V.ApplicationObjects.UserInterfaces.Systems;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Reports;

namespace NSites_V.ApplicationObjects.UserInterfaces
{
    public partial class MDINSites_VUI : Form
    {
        #region "VARIABLES"
        UserGroup loUserGroup;
        DataView ldvUserGroup;
        DataTable ldtUserGroup;
        SystemConfiguration loSystemConfiguration;
        CashierPeriod loCashierPeriod;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public MDINSites_VUI()
        {
            InitializeComponent();
            loUserGroup = new UserGroup();
            ldtUserGroup = new DataTable();
            loSystemConfiguration = new SystemConfiguration();
            loCashierPeriod = new CashierPeriod();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        private void disabledMenuStrip()
        {
            try
            {
                foreach (ToolStripMenuItem item in mnsNSites_V.Items)
                {
                    item.Enabled = false;
                    foreach (ToolStripItem subitem in item.DropDownItems)
                    {
                        if (subitem is ToolStripMenuItem)
                        {
                            subitem.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void enabledMenuStrip()
        {
            try
            {
                ldtUserGroup = loUserGroup.getUserGroupMenuItems();

                GlobalVariables.DVRights = new DataView(loUserGroup.getUserGroupRights());
                ldvUserGroup = new DataView(ldtUserGroup);
                foreach (ToolStripMenuItem item in mnsNSites_V.Items)
                {
                    try
                    {
                        ldvUserGroup.RowFilter = "Menu = '" + item.Name + "'";
                    }
                    catch { }
                    if (ldvUserGroup.Count != 0)
                    {
                        item.Enabled = true;
                        processMenuItems(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void processMenuItems(ToolStripMenuItem pitem)
        {
            try
            {
                if (true)
                {
                    pitem.Enabled = true;
                }

                foreach (ToolStripItem subitem in pitem.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem)
                    {
                        ldvUserGroup.RowFilter = "Item = '" + subitem.Name + "'";
                        if (ldvUserGroup.Count != 0)
                        {
                            subitem.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int displayControlOnTab(Control pControl, TabPage pTabPage)
        {
            try
            {
                // The tabpage.
                Form _FormControl = new Form();
                _FormControl = (Form)pControl;

                // Add to the tab control.
                pTabPage.Text = _FormControl.Text;
                pTabPage.Name = _FormControl.Name;
                tbcNSites_V.TabPages.Add(pTabPage);
                tbcNSites_V.SelectTab(pTabPage);
                _FormControl.TopLevel = false;
                _FormControl.Parent = this;
                _FormControl.Dock = DockStyle.Fill;
                _FormControl.FormBorderStyle = FormBorderStyle.None;
                pTabPage.Controls.Add(_FormControl);
                tbcNSites_V.SelectTab(tbcNSites_V.SelectedIndex);
                _FormControl.Show();
                return tbcNSites_V.SelectedIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeTabPage()
        {
            try
            {
                tbcNSites_V.TabPages.RemoveAt(tbcNSites_V.SelectedIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void changeHomeImage()
        {
            try
            {
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    pctScreenSaver.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getGlobalVariablesData()
        {
            try
            {
                foreach (DataRow _drSystemConfig in loSystemConfiguration.getAllData().Rows)
                {
                    if (_drSystemConfig["Key"].ToString() == "CompanyLogo")
                    {
                        GlobalVariables.CompanyLogo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ReportLogo")
                    {
                        GlobalVariables.ReportLogo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "DisplayRecordLimit")
                    {
                        GlobalVariables.DisplayRecordLimit = int.Parse(_drSystemConfig["Value"].ToString());
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EmailAddress")
                    {
                        GlobalVariables.EmailAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EmailPassword")
                    {
                        GlobalVariables.EmailPassword = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CashierPeriodDebit")
                    {
                        GlobalVariables.CashierPeriodDebit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CashierPeriodCredit")
                    {
                        GlobalVariables.CashierPeriodCredit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "OverridePassword")
                    {
                        GlobalVariables.OverridePassword = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ScreenSaverImage")
                    {
                        GlobalVariables.ScreenSaverImage = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "MDITabAlignment")
                    {
                        GlobalVariables.TabAlignment = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "BackupMySqlDumpAddress")
                    {
                        GlobalVariables.BackupMySqlDumpAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "RestoreMySqlAddress")
                    {
                        GlobalVariables.RestoreMySqlAddress = _drSystemConfig["Value"].ToString();
                    }
                }

                //byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ReportLogo);
                GlobalVariables.DTCompanyLogo = GlobalFunctions.getReportLogo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "END OF METHODS"

        #region "EVENTS"
        private void MDIFrameWork_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += " [" + GlobalVariables.CurrentConnection + "]";
                pnlMenu.BackColor = Color.FromArgb(int.Parse(GlobalVariables.SecondaryColor));
                getGlobalVariablesData();
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch { }
                try
                {
                    byte[] hextobyteLogo = GlobalFunctions.HexToBytes(GlobalVariables.CompanyLogo);
                    pctLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyteLogo);
                }
                catch { }
                try
                {
                    switch (GlobalVariables.TabAlignment)
                    {
                        case "Top":
                            tbcNSites_V.Alignment = TabAlignment.Top;
                            break;
                        case "Bottom":
                            tbcNSites_V.Alignment = TabAlignment.Bottom;
                            break;
                        case "Left":
                            tbcNSites_V.Alignment = TabAlignment.Left;
                            break;
                        case "Right":
                            tbcNSites_V.Alignment = TabAlignment.Right;
                            break;
                        default:
                            tbcNSites_V.Alignment = TabAlignment.Top;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                lblUsername.Text = "Welcome!  " + GlobalVariables.Userfullname;
                lblDateTime.Text = DateTime.Now.ToLongDateString();
                lblOwnerName.UseMnemonic = false;
                lblOwnerName.Text = GlobalVariables.CompanyName;
                lblApplicationName.Text = GlobalVariables.ApplicationName;
                if (GlobalVariables.Username != "admin" && GlobalVariables.Username != "technicalsupport")
                {
                    disabledMenuStrip();
                    enabledMenuStrip();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "MDIFrameWork_Load");
                em.ShowDialog();
                Application.Exit();
            }
        }

        private void tsmSystemConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "System Configuration")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SystemConfigurationUI _SystemConfiguration = new SystemConfigurationUI();
                TabPage _SystemConfigurationTab = new TabPage();
                _SystemConfigurationTab.ImageIndex = 1;
                _SystemConfiguration.ParentList = this;
                displayControlOnTab(_SystemConfiguration, _SystemConfigurationTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSystemConfiguration_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUser_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "User List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                User _User = new User();
                Type _Type = typeof(User);
                ListFormSystemUI _ListForm = new ListFormSystemUI((object)_User, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 2;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUser_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUserGroup_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "User Group List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                UserGroupListUI _UserGroupList = new UserGroupListUI();
                TabPage _UserGroupTab = new TabPage();
                _UserGroupTab.ImageIndex = 3;
                _UserGroupList.ParentList = this;
                displayControlOnTab(_UserGroupList, _UserGroupTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUserGroup_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmChangeUserPassword_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Change User Password")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ChangeUserPasswordUI _ChangeUserPassword = new ChangeUserPasswordUI();
                TabPage _ChangeUserPasswordTab = new TabPage();
                _ChangeUserPasswordTab.ImageIndex = 4;
                _ChangeUserPassword.ParentList = this;
                displayControlOnTab(_ChangeUserPassword, _ChangeUserPasswordTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmChangeUserPassword_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmLockScreen_Click(object sender, EventArgs e)
        {
            try
            {
                UnlockScreenUI _UnlockScreen = new UnlockScreenUI();
                _UnlockScreen.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmLockScreen_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion "END OF EVENTS"

        private void tsmScreenSaver_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Screen Saver")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ScreenSaverUI _ScreenSaver = new ScreenSaverUI();
                TabPage _ScreenSaverTab = new TabPage();
                _ScreenSaverTab.ImageIndex = 5;
                _ScreenSaver.ParentList = this;
                displayControlOnTab(_ScreenSaver, _ScreenSaverTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmScreenSaver_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmAuditTrail_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Audit Trail")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                AuditTrailUI _AuditTrail = new AuditTrailUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 55;
                _AuditTrail.ParentList = this;
                displayControlOnTab(_AuditTrail, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmAuditTrail_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Supplier List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Supplier _Supplier = new Supplier();
                Type _Type = typeof(Supplier);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Supplier, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 14;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSupplier_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Customer List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Customer _Customer = new Customer();
                Type _Type = typeof(Customer);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Customer, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 13;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCustomer_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmTechnicalUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //check technical support username and password
                if (GlobalVariables.Username == "technicalsupport")
                {
                    foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                    {
                        if (_tab.Text == "Technical Update")
                        {
                            tbcNSites_V.SelectedTab = _tab;
                            return;
                        }
                    }

                    TechnicalUpdateUI _TechnicalUpdate = new TechnicalUpdateUI();
                    TabPage _TechnicalUpdateTab = new TabPage();
                    _TechnicalUpdateTab.ImageIndex = 7;
                    _TechnicalUpdate.ParentList = this;
                    displayControlOnTab(_TechnicalUpdate, _TechnicalUpdateTab);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("Only JC Technical Support can open this Function!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmTechnicalUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUnit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Unit List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Unit _Unit = new Unit();
                Type _Type = typeof(Unit);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Unit, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 22;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUnit_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmInventoryGroup_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "InventoryGroup List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                InventoryGroup _InventoryGroup = new InventoryGroup();
                Type _Type = typeof(InventoryGroup);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_InventoryGroup, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 19;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmInventoryGroup_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCategory_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Category List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Category _Category = new Category();
                Type _Type = typeof(Category);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Category, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 20;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCategory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStock_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Stock _Stock = new Stock();
                Type _Type = typeof(Stock);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Stock, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 21;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStock_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmLocation_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Location List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Location _Location = new Location();
                Type _Type = typeof(Location);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Location, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 24;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmLocation_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Adjustment")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockAdjustmentUI _StockAdjustment = new StockAdjustmentUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 33;
                _StockAdjustment.ParentList = this;
                displayControlOnTab(_StockAdjustment, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockAdjustment_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Receiving")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockReceivingUI _StockReceiving = new StockReceivingUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 30;
                _StockReceiving.ParentList = this;
                displayControlOnTab(_StockReceiving, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockReceiving_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockWithdrawal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Withdrawal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockWithdrawalUI _StockWithdrawal = new StockWithdrawalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 29;
                _StockWithdrawal.ParentList = this;
                displayControlOnTab(_StockWithdrawal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockWithdrawal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockInventory_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Inventory")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockInventoryUI _StockInventory = new StockInventoryUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 41;
                _StockInventory.ParentList = this;
                displayControlOnTab(_StockInventory, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockCard_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Card")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockCardUI _StockCard = new StockCardUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 42;
                _StockCard.ParentList = this;
                displayControlOnTab(_StockCard, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockCard_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmInventoryType_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "InventoryType List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                InventoryType _InventoryType = new InventoryType();
                Type _Type = typeof(InventoryType);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_InventoryType, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 23;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmInventoryType_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockTransferOut_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Transfer Out")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockTransferOutUI _StockTransferOut = new StockTransferOutUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 31;
                _StockTransferOut.ParentList = this;
                displayControlOnTab(_StockTransferOut, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockTransferOut_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockTransferIn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Transfer In")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockTransferInUI _StockTransferIn = new StockTransferInUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 32;
                _StockTransferIn.ParentList = this;
                displayControlOnTab(_StockTransferIn, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockTransferIn_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmReorderLevel_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Reorder Level")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ReorderLevelUI _ReorderLevel = new ReorderLevelUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _ReorderLevel.ParentList = this;
                displayControlOnTab(_ReorderLevel, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmReorderLevel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnClearTab_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text != "Home")
                    {
                        tbcNSites_V.TabPages.Remove(_tab);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClearTab_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmModeOfPayment_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "ModeOfPayment List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ModeOfPayment _ModeOfPayment = new ModeOfPayment();
                Type _Type = typeof(ModeOfPayment);
                ListFormPOSUI _ListForm = new ListFormPOSUI((object)_ModeOfPayment, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 16;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmModeOfPayment_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPOSOrdering_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "POS-Ordering")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                GlobalVariables.CashierPeriodId = "";
                CashierPeriod loCashierPeriod = new CashierPeriod();
                foreach (DataRow _dr in loCashierPeriod.getCashierPeriodOpen().Rows)
                { 
                    GlobalVariables.CashierPeriodId = _dr[0].ToString();
                }
                if (GlobalVariables.CashierId == "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier Period must be Open!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                else
                {
                    POSOrderingUI _POSOrdering = new POSOrderingUI();
                    TabPage _ListFormTab = new TabPage();
                    _ListFormTab.ImageIndex = 33;
                    _POSOrdering.ParentList = this;
                    displayControlOnTab(_POSOrdering, _ListFormTab);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPOSOrdering_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmOpenCashierPeriod_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariables.CashierPeriodId = "";
                CashierPeriod loCashierPeriod = new CashierPeriod();
                foreach (DataRow _dr in loCashierPeriod.getCashierPeriodOpen().Rows)
                { 
                    GlobalVariables.CashierPeriodId = _dr[0].ToString();
                }
                if (GlobalVariables.CashierPeriodId != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier Period is already open!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                else
                {
                    OpenCasherPeriodUI loOpenCasherPeriod = new OpenCasherPeriodUI();
                    loOpenCasherPeriod.ParentList = this;
                    loOpenCasherPeriod.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmOpenCashierPeriod_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCloseCashierPeriod_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariables.CashierPeriodId = "";
                CashierPeriod loCashierPeriod = new CashierPeriod();
                foreach (DataRow _dr in loCashierPeriod.getCashierPeriodOpen().Rows)
                {
                    GlobalVariables.CashierPeriodId = _dr[0].ToString();
                }
                if (GlobalVariables.CashierPeriodId == "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Cashier Period is already close!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                else
                {
                    CloseCashierPeriodUI loCloseCashierPeriod = new CloseCashierPeriodUI();
                    loCloseCashierPeriod.ParentList = this;
                    loCloseCashierPeriod.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCloseCashierPeriod_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCashierPeriods_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Cashier Periods")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                CashierPeriodsUI _CashierPeriods = new CashierPeriodsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _CashierPeriods.ParentList = this;
                displayControlOnTab(_CashierPeriods, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCashierPeriods_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesInventory_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Sales Inventory")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SalesInventoryUI _SalesInventory = new SalesInventoryUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _SalesInventory.ParentList = this;
                displayControlOnTab(_SalesInventory, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSalesInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmReturnedItems_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Returned Items")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ReturnedItemsUI _ReturnedItems = new ReturnedItemsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _ReturnedItems.ParentList = this;
                displayControlOnTab(_ReturnedItems, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmReturnedItems_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPOSTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "POS Transactions")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                POSTransactionsUI _POSTransactions = new POSTransactionsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _POSTransactions.ParentList = this;
                displayControlOnTab(_POSTransactions, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPOSTransctions_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCashier_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Cashier List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Cashier _Cashier = new Cashier();
                Type _Type = typeof(Cashier);
                ListFormPOSUI _ListForm = new ListFormPOSUI((object)_Cashier, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 18;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCashier_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBackupRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Database Backup/Restore")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                DatabaseBackupRestoreUI loDatabaseBackupRestore = new DatabaseBackupRestoreUI();
                loDatabaseBackupRestore.ParentList = this;
                loDatabaseBackupRestore.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBackupRestoreDatabase_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPOSMerchandising_Click(object sender, EventArgs e)
        {

        }

        private void tsmDiscount_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Discount List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Discount _Discount = new Discount();
                Type _Type = typeof(Discount);
                ListFormPOSUI _ListForm = new ListFormPOSUI((object)_Discount, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 16;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmDiscount_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
