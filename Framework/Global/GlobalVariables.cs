using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.Global
{
    public class GlobalVariables
    {
        public static string ConnectionString = "";
        public static string BaseAddress = "";
        //application
        public static string ApplicationId = "";
        public static string ApplicationName = "";
        public static string ProcessorId = "";
        public static string TrialVersion = "";
        public static string LicenseKey = "";
        public static DateTime lLicenseExpiry;
        public static string VersionNo = "";
        public static string DevelopedBy = "";
        //company
        public static string CompanyName = "";
        public static string CompanyAddress = "";
        public static string ContactNumber = "";
        public static string CompanyLogo = "";
        public static string BannerImage = "";
        public static string ReportLogo = "";
        //system settings
        public static string CurrentLocationId = "";
        public static string CurrentConnection = "";
        //database backup
        public static string BackupPath = "";
        public static string BackupMySqlDumpAddress = "";
        public static string RestoreMySqlAddress = "";
        //display
        public static string ScreenSaverImage = "";
        public static string TabAlignment = "";
        public static int DisplayRecordLimit = 0;
        public static string PrimaryColor = "";
        public static string SecondaryColor = "";
        public static string FormBackColor = "";
        public static string EmailAddress = "";
        public static string EmailPassword = "";
        //accounts setup
        public static string CashierPeriodDebit = "";
        public static string CashierPeriodCredit = "";

        public static string CashierPeriodId = "0";
        public static string CashierId = "0";
        public static string CashierName = "";
        public static string DefaultCustomerId = "0";
        public static string DefaultCustomerName = "";
        public static string DefaultModeOfPaymentId = "";
        public static string DefaultModeOfPaymentDescription = "";
        public static string DefaultTableId = "";
        public static string DefaultTableDescription = "";

        public static string POSType = "";
        public static bool TouchScreen = false;
        public static bool PreviewOR = false;
        public static bool PreviewSlip = false;
        public static string PrinterName = "";
        public static string Terminal = "";
        public static string ORSize = "";
        public static string OverridePassword = "";

        public static bool Speak = false;
        public static bool RecognizedSpeech = false;
        public static decimal ShortTermPlacementTax = 0;
        public static decimal LongTermPlacementTax = 0;
        public static decimal PortfolioManagedTax = 0;

        public static string UserId = "";
        public static string Username = "";
        public static string UserPassword = "";
        public static string Userfullname = "";
        public static string UserGroupId = "";
        public static string UserEmailAddress = "";
        public static DateTime LastBackupDate;
        public static string Hostname = "";
        public static int xLocation;
        public static int yLocation;
        //Dataview
        public static DataView DVRights;
        //data table
        public static DataTable DTCompanyLogo = new DataTable();
        //report
        public static ReportViewerUI loReportPreviewer = new ReportViewerUI();
        public static CrystalReport loCrystalReport = new CrystalReport();
        public static CrystalReport1 loCrystalReport1 = new CrystalReport1();
        //technical support
        public static string TechnicalSupportEmailAddress = "";

        public enum Operation
        {
            Add = 0,
            Edit = 1,
            Delete = 2,
            Open = 3,
            Close = 4
        };
        public enum Icons
        {
            Information = 0,
            Save = 1,
            Ok = 2,
            QuestionMark = 3,
            Delete = 4,
            Warning = 5,
            Error = 6,
            Close = 7
        };
        public enum Buttons
        {
            OK = 0,
            OKCancel = 1,
            YesNo = 2,
            YesNoCancel = 3,
            Close = 4
        };
    }
}
