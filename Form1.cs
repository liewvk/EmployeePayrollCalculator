using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace EmployeePayrollCalculator
{
    public partial class Form1 : Form
    {
        private DataTable payrollTable = new DataTable();

        private decimal overtimePay = 0;
        private decimal grossPay = 0;
        private decimal netPay = 0;

        private bool ValidateInput(out decimal basicSalary,
                               out decimal overtimeHours,
                               out decimal overtimeRate,
                               out decimal allowance,
                               out decimal deduction)
        {
            basicSalary = 0;
            overtimeHours = 0;
            overtimeRate = 0;
            allowance = 0;
            deduction = 0;

            if (txtEmployeeId.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the Employee ID.",
                                "Missing Employee ID",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtEmployeeId.Focus();
                return false;
            }

            if (txtEmployeeName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the Employee Name.",
                                "Missing Employee Name",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtEmployeeName.Focus();
                return false;
            }

            if (cmbDepartment.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a department.",
                                "Missing Department",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                cmbDepartment.Focus();
                return false;
            }

            if (!decimal.TryParse(txtBasicSalary.Text.Trim(), out basicSalary))
            {
                MessageBox.Show("Please enter a valid basic salary.",
                                "Invalid Basic Salary",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtBasicSalary.Focus();
                return false;
            }

            if (basicSalary < 0)
            {
                MessageBox.Show("Basic salary cannot be negative.",
                                "Invalid Basic Salary",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtBasicSalary.Focus();
                return false;
            }

            if (!decimal.TryParse(txtOvertimeHours.Text.Trim(), out overtimeHours))
            {
                MessageBox.Show("Please enter valid overtime hours.",
                                "Invalid Overtime Hours",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtOvertimeHours.Focus();
                return false;
            }

            if (overtimeHours < 0)
            {
                MessageBox.Show("Overtime hours cannot be negative.",
                                "Invalid Overtime Hours",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtOvertimeHours.Focus();
                return false;
            }

            if (!decimal.TryParse(txtOvertimeRate.Text.Trim(), out overtimeRate))
            {
                MessageBox.Show("Please enter a valid overtime rate.",
                                "Invalid Overtime Rate",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtOvertimeRate.Focus();
                return false;
            }

            if (overtimeRate < 0)
            {
                MessageBox.Show("Overtime rate cannot be negative.",
                                "Invalid Overtime Rate",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtOvertimeRate.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAllowance.Text.Trim(), out allowance))
            {
                MessageBox.Show("Please enter a valid allowance.",
                                "Invalid Allowance",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtAllowance.Focus();
                return false;
            }

            if (allowance < 0)
            {
                MessageBox.Show("Allowance cannot be negative.",
                                "Invalid Allowance",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtAllowance.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDeduction.Text.Trim(), out deduction))
            {
                MessageBox.Show("Please enter a valid deduction.",
                                "Invalid Deduction",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtDeduction.Focus();
                return false;
            }

            if (deduction < 0)
            {
                MessageBox.Show("Deduction cannot be negative.",
                                "Invalid Deduction",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtDeduction.Focus();
                return false;
            }

            return true;
        }

        private void GeneratePayslip(decimal basicSalary,
                             decimal overtimePay,
                             decimal allowance,
                             decimal grossPay,
                             decimal deduction,
                             decimal netPay)
        {
            string employeeId = txtEmployeeId.Text.Trim();
            string employeeName = txtEmployeeName.Text.Trim();
            string department = cmbDepartment.SelectedItem.ToString();

            rtbPayslip.Clear();

            rtbPayslip.AppendText("EMPLOYEE PAYSLIP\n");
            rtbPayslip.AppendText("--------------------------------\n");
            rtbPayslip.AppendText($"Date          : {DateTime.Today.ToShortDateString()}\n");
            rtbPayslip.AppendText($"Employee ID   : {employeeId}\n");
            rtbPayslip.AppendText($"Name          : {employeeName}\n");
            rtbPayslip.AppendText($"Department    : {department}\n");
            rtbPayslip.AppendText("--------------------------------\n");
            rtbPayslip.AppendText($"Basic Salary  : RM {basicSalary:0.00}\n");
            rtbPayslip.AppendText($"Overtime Pay  : RM {overtimePay:0.00}\n");
            rtbPayslip.AppendText($"Allowance     : RM {allowance:0.00}\n");
            rtbPayslip.AppendText($"Gross Pay     : RM {grossPay:0.00}\n");
            rtbPayslip.AppendText($"Deduction     : RM {deduction:0.00}\n");
            rtbPayslip.AppendText("--------------------------------\n");
            rtbPayslip.AppendText($"Net Pay       : RM {netPay:0.00}\n");
            rtbPayslip.AppendText("--------------------------------\n");
        }

        private void ClearInputFields()
        {
            txtEmployeeId.Clear();
            txtEmployeeName.Clear();
            cmbDepartment.SelectedIndex = -1;

            txtBasicSalary.Clear();
            txtOvertimeHours.Clear();
            txtOvertimeRate.Clear();
            txtAllowance.Text = "0";
            txtDeduction.Text = "0";

            lblNetPay.Text = "RM 0.00";
            rtbPayslip.Clear();

            overtimePay = 0;
            grossPay = 0;
            netPay = 0;

            txtEmployeeId.Focus();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void txtDeduction_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbDepartment.Items.Add("Administration");
            cmbDepartment.Items.Add("Sales");
            cmbDepartment.Items.Add("IT");
            cmbDepartment.Items.Add("Finance");
            cmbDepartment.Items.Add("Human Resource");
            cmbDepartment.Items.Add("Operations");

            cmbDepartment.SelectedIndex = -1;

            payrollTable.Columns.Add("Employee ID", typeof(string));
            payrollTable.Columns.Add("Employee Name", typeof(string));
            payrollTable.Columns.Add("Department", typeof(string));
            payrollTable.Columns.Add("Basic Salary", typeof(decimal));
            payrollTable.Columns.Add("Overtime Pay", typeof(decimal));
            payrollTable.Columns.Add("Allowance", typeof(decimal));
            payrollTable.Columns.Add("Deduction", typeof(decimal));
            payrollTable.Columns.Add("Net Pay", typeof(decimal));

            dgvPayroll.DataSource = payrollTable;

            dgvPayroll.Columns["Basic Salary"].DefaultCellStyle.Format = "0.00";
            dgvPayroll.Columns["Overtime Pay"].DefaultCellStyle.Format = "0.00";
            dgvPayroll.Columns["Allowance"].DefaultCellStyle.Format = "0.00";
            dgvPayroll.Columns["Deduction"].DefaultCellStyle.Format = "0.00";
            dgvPayroll.Columns["Net Pay"].DefaultCellStyle.Format = "0.00";

            txtAllowance.Text = "0";
            txtDeduction.Text = "0";
            lblNetPay.Text = "RM 0.00";

            txtEmployeeId.Focus();

        }

        private void lblNetPay_Click(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal basicSalary,
                       out decimal overtimeHours,
                       out decimal overtimeRate,
                       out decimal allowance,
                       out decimal deduction))
            {
                return;
            }

            overtimePay = overtimeHours * overtimeRate;
            grossPay = basicSalary + overtimePay + allowance;
            netPay = grossPay - deduction;

            if (netPay < 0)
            {
                MessageBox.Show("Deduction cannot be greater than gross pay.",
                                "Invalid Deduction",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtDeduction.Focus();
                return;
            }

            lblNetPay.Text = $"RM {netPay:0.00}";

            GeneratePayslip(basicSalary, overtimePay, allowance, grossPay, deduction, netPay);

            MessageBox.Show("Payroll calculated successfully.",
                            "Calculation Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal basicSalary,
                       out decimal overtimeHours,
                       out decimal overtimeRate,
                       out decimal allowance,
                       out decimal deduction))
            {
                return;
            }

            overtimePay = overtimeHours * overtimeRate;
            grossPay = basicSalary + overtimePay + allowance;
            netPay = grossPay - deduction;

            if (netPay < 0)
            {
                MessageBox.Show("Deduction cannot be greater than gross pay.",
                                "Invalid Deduction",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtDeduction.Focus();
                return;
            }

            string employeeId = txtEmployeeId.Text.Trim();
            string employeeName = txtEmployeeName.Text.Trim();
            string department = cmbDepartment.SelectedItem.ToString();

            payrollTable.Rows.Add(employeeId,
                                  employeeName,
                                  department,
                                  basicSalary,
                                  overtimePay,
                                  allowance,
                                  deduction,
                                  netPay);

            lblNetPay.Text = $"RM {netPay:0.00}";

            GeneratePayslip(basicSalary, overtimePay, allowance, grossPay, deduction, netPay);

            MessageBox.Show("Payroll record added successfully.",
                            "Record Added",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        }

        private void btnClearInput_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (payrollTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no payroll records to clear.",
                                "Empty Payroll List",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to clear all payroll records?",
                                                  "Confirm Clear All",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                payrollTable.Rows.Clear();

                ClearInputFields();

                MessageBox.Show("All payroll records have been cleared.",
                                "Payroll Records Cleared",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to exit?";

            if (payrollTable.Rows.Count > 0)
            {
                message = "You have payroll records that are not saved permanently.\n\nAre you sure you want to exit?";
            }

            DialogResult result = MessageBox.Show(message,
                                                  "Confirm Exit",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
