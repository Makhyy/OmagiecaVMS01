namespace OmagiecaVMS01
{
    partial class ucfrmVisitor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.omagiecaVMS01DBDataSet2 = new OmagiecaVMS01.OmagiecaVMS01DBDataSet2();
            this.visitorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.visitorTableAdapter = new OmagiecaVMS01.OmagiecaVMS01DBDataSet2TableAdapters.VisitorTableAdapter();
            this.tableAdapterManager = new OmagiecaVMS01.OmagiecaVMS01DBDataSet2TableAdapters.TableAdapterManager();
            this.cboRFIDTag = new System.Windows.Forms.ComboBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtForeignCountry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCityMunicipality = new System.Windows.Forms.TextBox();
            this.txtPaymentAmount = new System.Windows.Forms.ComboBox();
            this.chkIsPWD = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboVisitorType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnRegisterVisitor = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGroupRegister = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvVisitors = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnVisitorsData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.omagiecaVMS01DBDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitors)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // omagiecaVMS01DBDataSet2
            // 
            this.omagiecaVMS01DBDataSet2.DataSetName = "OmagiecaVMS01DBDataSet2";
            this.omagiecaVMS01DBDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // visitorBindingSource
            // 
            this.visitorBindingSource.DataMember = "Visitor";
            this.visitorBindingSource.DataSource = this.omagiecaVMS01DBDataSet2;
            // 
            // visitorTableAdapter
            // 
            this.visitorTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.ArchivedVisitorsTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.PaymentTableAdapter = null;
            this.tableAdapterManager.RfidTagTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = OmagiecaVMS01.OmagiecaVMS01DBDataSet2TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UserAccountTableAdapter = null;
            this.tableAdapterManager.VisitorTableAdapter = this.visitorTableAdapter;
            this.tableAdapterManager.VisitorTypeTableAdapter = null;
            this.tableAdapterManager.VisitStatusTableAdapter = null;
            this.tableAdapterManager.VisitTableAdapter = null;
            // 
            // cboRFIDTag
            // 
            this.cboRFIDTag.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboRFIDTag.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRFIDTag.FormattingEnabled = true;
            this.cboRFIDTag.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cboRFIDTag.Location = new System.Drawing.Point(758, 242);
            this.cboRFIDTag.Name = "cboRFIDTag";
            this.cboRFIDTag.Size = new System.Drawing.Size(153, 38);
            this.cboRFIDTag.TabIndex = 213;
            // 
            // txtAge
            // 
            this.txtAge.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.Location = new System.Drawing.Point(173, 163);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(100, 35);
            this.txtAge.TabIndex = 212;
            this.txtAge.TextChanged += new System.EventHandler(this.txtAge_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(91, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 32);
            this.label4.TabIndex = 211;
            this.label4.Text = "Age";
            // 
            // txtForeignCountry
            // 
            this.txtForeignCountry.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtForeignCountry.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForeignCountry.Location = new System.Drawing.Point(758, 130);
            this.txtForeignCountry.Name = "txtForeignCountry";
            this.txtForeignCountry.Size = new System.Drawing.Size(257, 32);
            this.txtForeignCountry.TabIndex = 210;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(556, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 32);
            this.label1.TabIndex = 209;
            this.label1.Text = "Foreign Country";
            // 
            // txtCityMunicipality
            // 
            this.txtCityMunicipality.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCityMunicipality.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCityMunicipality.Location = new System.Drawing.Point(758, 61);
            this.txtCityMunicipality.Name = "txtCityMunicipality";
            this.txtCityMunicipality.Size = new System.Drawing.Size(257, 35);
            this.txtCityMunicipality.TabIndex = 208;
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPaymentAmount.Enabled = false;
            this.txtPaymentAmount.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentAmount.FormattingEnabled = true;
            this.txtPaymentAmount.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.txtPaymentAmount.Location = new System.Drawing.Point(758, 187);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Size = new System.Drawing.Size(153, 38);
            this.txtPaymentAmount.TabIndex = 207;
            this.txtPaymentAmount.SelectedIndexChanged += new System.EventHandler(this.txtPaymentAmount_SelectedIndexChanged);
            // 
            // chkIsPWD
            // 
            this.chkIsPWD.AutoSize = true;
            this.chkIsPWD.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsPWD.Location = new System.Drawing.Point(172, 251);
            this.chkIsPWD.Name = "chkIsPWD";
            this.chkIsPWD.Size = new System.Drawing.Size(88, 29);
            this.chkIsPWD.TabIndex = 206;
            this.chkIsPWD.Text = "IsPWD";
            this.chkIsPWD.UseVisualStyleBackColor = true;
            this.chkIsPWD.CheckedChanged += new System.EventHandler(this.chkIsPWD_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(513, 140);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(242, 32);
            this.label9.TabIndex = 201;
            this.label9.Text = "Payment Amount (₱)";
            // 
            // cboVisitorType
            // 
            this.cboVisitorType.BackColor = System.Drawing.SystemColors.Window;
            this.cboVisitorType.Enabled = false;
            this.cboVisitorType.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVisitorType.FormattingEnabled = true;
            this.cboVisitorType.Items.AddRange(new object[] {
            "Adult",
            "Child",
            "Senior Citizen"});
            this.cboVisitorType.Location = new System.Drawing.Point(173, 213);
            this.cboVisitorType.Margin = new System.Windows.Forms.Padding(2);
            this.cboVisitorType.Name = "cboVisitorType";
            this.cboVisitorType.Size = new System.Drawing.Size(155, 38);
            this.cboVisitorType.TabIndex = 199;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 216);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 32);
            this.label6.TabIndex = 198;
            this.label6.Text = "Visitor Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(67, 280);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 32);
            this.label7.TabIndex = 197;
            this.label7.Text = "Gender";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(173, 113);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(2);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(280, 35);
            this.txtLastName.TabIndex = 205;
            this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 116);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 32);
            this.label3.TabIndex = 204;
            this.label3.Text = "Last Name";
            // 
            // cboGender
            // 
            this.cboGender.BackColor = System.Drawing.SystemColors.Window;
            this.cboGender.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Others"});
            this.cboGender.Location = new System.Drawing.Point(172, 283);
            this.cboGender.Margin = new System.Windows.Forms.Padding(2);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(156, 38);
            this.cboGender.TabIndex = 203;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(610, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 32);
            this.label2.TabIndex = 202;
            this.label2.Text = "RFID Tag #";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(554, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 32);
            this.label5.TabIndex = 200;
            this.label5.Text = "City/Municipality";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(28, 64);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(132, 32);
            this.lblName.TabIndex = 196;
            this.lblName.Text = "First Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(173, 63);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(280, 35);
            this.txtFirstName.TabIndex = 195;
            // 
            // btnRegisterVisitor
            // 
            this.btnRegisterVisitor.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnRegisterVisitor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegisterVisitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterVisitor.ForeColor = System.Drawing.Color.White;
            this.btnRegisterVisitor.Location = new System.Drawing.Point(26, 347);
            this.btnRegisterVisitor.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegisterVisitor.Name = "btnRegisterVisitor";
            this.btnRegisterVisitor.Size = new System.Drawing.Size(134, 45);
            this.btnRegisterVisitor.TabIndex = 214;
            this.btnRegisterVisitor.Text = "Register";
            this.btnRegisterVisitor.UseVisualStyleBackColor = false;
            this.btnRegisterVisitor.Click += new System.EventHandler(this.btnRegisterVisitor_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Green;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(184, 347);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(143, 45);
            this.btnUpdate.TabIndex = 215;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1112, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 38);
            this.button2.TabIndex = 218;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGroupRegister
            // 
            this.btnGroupRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroupRegister.BackColor = System.Drawing.Color.DarkMagenta;
            this.btnGroupRegister.FlatAppearance.BorderSize = 2;
            this.btnGroupRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnGroupRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroupRegister.ForeColor = System.Drawing.Color.White;
            this.btnGroupRegister.Location = new System.Drawing.Point(1112, 102);
            this.btnGroupRegister.Name = "btnGroupRegister";
            this.btnGroupRegister.Size = new System.Drawing.Size(139, 56);
            this.btnGroupRegister.TabIndex = 219;
            this.btnGroupRegister.Text = "Group Registration";
            this.btnGroupRegister.UseVisualStyleBackColor = false;
            this.btnGroupRegister.Click += new System.EventHandler(this.btnGroupRegister_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Firebrick;
            this.btnDelete.FlatAppearance.BorderSize = 2;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(527, 347);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(143, 46);
            this.btnDelete.TabIndex = 217;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvVisitors
            // 
            this.dgvVisitors.AllowUserToResizeColumns = false;
            this.dgvVisitors.AllowUserToResizeRows = false;
            this.dgvVisitors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVisitors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVisitors.BackgroundColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SaddleBrown;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVisitors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVisitors.ColumnHeadersHeight = 50;
            this.dgvVisitors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVisitors.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVisitors.EnableHeadersVisualStyles = false;
            this.dgvVisitors.Location = new System.Drawing.Point(0, 414);
            this.dgvVisitors.Name = "dgvVisitors";
            this.dgvVisitors.ReadOnly = true;
            this.dgvVisitors.RowHeadersVisible = false;
            this.dgvVisitors.Size = new System.Drawing.Size(1261, 285);
            this.dgvVisitors.TabIndex = 221;
            this.dgvVisitors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVisitors_CellContentClick_1);
            this.dgvVisitors.SelectionChanged += new System.EventHandler(this.dgvVisitors_SelectionChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(352, 346);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(153, 47);
            this.btnClear.TabIndex = 262;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1261, 48);
            this.panel4.TabIndex = 278;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Location = new System.Drawing.Point(457, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(298, 38);
            this.label10.TabIndex = 0;
            this.label10.Text = "Visitor Registration";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 651);
            this.panel2.TabIndex = 279;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1251, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 641);
            this.panel3.TabIndex = 280;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(10, 689);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1251, 10);
            this.panel5.TabIndex = 281;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.OldLace;
            this.panel6.Controls.Add(this.btnVisitorsData);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 48);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1261, 651);
            this.panel6.TabIndex = 282;
            // 
            // btnVisitorsData
            // 
            this.btnVisitorsData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVisitorsData.BackColor = System.Drawing.Color.SeaGreen;
            this.btnVisitorsData.FlatAppearance.BorderSize = 2;
            this.btnVisitorsData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnVisitorsData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisitorsData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisitorsData.ForeColor = System.Drawing.Color.White;
            this.btnVisitorsData.Location = new System.Drawing.Point(1112, 129);
            this.btnVisitorsData.Name = "btnVisitorsData";
            this.btnVisitorsData.Size = new System.Drawing.Size(139, 43);
            this.btnVisitorsData.TabIndex = 221;
            this.btnVisitorsData.Text = "All Records";
            this.btnVisitorsData.UseVisualStyleBackColor = false;
            this.btnVisitorsData.Click += new System.EventHandler(this.btnVisitorsData_Click);
            // 
            // ucfrmVisitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvVisitors);
            this.Controls.Add(this.btnGroupRegister);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnRegisterVisitor);
            this.Controls.Add(this.cboRFIDTag);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtForeignCountry);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCityMunicipality);
            this.Controls.Add(this.txtPaymentAmount);
            this.Controls.Add(this.chkIsPWD);
            this.Controls.Add(this.cboVisitorType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Name = "ucfrmVisitor";
            this.Size = new System.Drawing.Size(1261, 699);
            this.Load += new System.EventHandler(this.ucfrmVisitor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.omagiecaVMS01DBDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitors)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OmagiecaVMS01DBDataSet2 omagiecaVMS01DBDataSet2;
        private System.Windows.Forms.BindingSource visitorBindingSource;
        private OmagiecaVMS01DBDataSet2TableAdapters.VisitorTableAdapter visitorTableAdapter;
        private OmagiecaVMS01DBDataSet2TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ComboBox cboRFIDTag;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtForeignCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCityMunicipality;
        private System.Windows.Forms.ComboBox txtPaymentAmount;
        private System.Windows.Forms.CheckBox chkIsPWD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboVisitorType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnRegisterVisitor;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGroupRegister;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvVisitors;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnVisitorsData;
    }
}
