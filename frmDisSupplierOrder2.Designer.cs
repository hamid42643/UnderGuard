namespace WindowsFormsApplication
{
    partial class frmDisSupplierOrder
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.btn_OrderDetails = new System.Windows.Forms.Button();
            this.btnSupplierChooser = new System.Windows.Forms.Button();
            this.btnChooseSupplier = new System.Windows.Forms.Button();
            this.dataGridViewParts2 = new System.Windows.Forms.DataGridView();
            this.btnSaveToDatabase = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridViewProducts);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(629, 154);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Products In Customer Order";
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.AllowUserToAddRows = false;
            this.dataGridViewProducts.AllowUserToDeleteRows = false;
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProducts.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(623, 135);
            this.dataGridViewProducts.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtTotalPrice);
            this.groupBox4.Controls.Add(this.btn_OrderDetails);
            this.groupBox4.Controls.Add(this.btnSupplierChooser);
            this.groupBox4.Controls.Add(this.btnChooseSupplier);
            this.groupBox4.Controls.Add(this.dataGridViewParts2);
            this.groupBox4.Location = new System.Drawing.Point(12, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(629, 233);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parts required to build the products";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Total Price:";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Enabled = false;
            this.txtTotalPrice.Location = new System.Drawing.Point(531, 200);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.Size = new System.Drawing.Size(92, 20);
            this.txtTotalPrice.TabIndex = 4;
            // 
            // btn_OrderDetails
            // 
            this.btn_OrderDetails.Location = new System.Drawing.Point(3, 191);
            this.btn_OrderDetails.Name = "btn_OrderDetails";
            this.btn_OrderDetails.Size = new System.Drawing.Size(92, 36);
            this.btn_OrderDetails.TabIndex = 3;
            this.btn_OrderDetails.Text = "Order details";
            this.btn_OrderDetails.UseVisualStyleBackColor = true;
            this.btn_OrderDetails.Click += new System.EventHandler(this.btn_OrderDetails_Click);
            // 
            // btnSupplierChooser
            // 
            this.btnSupplierChooser.Location = new System.Drawing.Point(318, 191);
            this.btnSupplierChooser.Name = "btnSupplierChooser";
            this.btnSupplierChooser.Size = new System.Drawing.Size(104, 36);
            this.btnSupplierChooser.TabIndex = 3;
            this.btnSupplierChooser.Text = "Choose Supplier";
            this.btnSupplierChooser.UseVisualStyleBackColor = true;
            this.btnSupplierChooser.Click += new System.EventHandler(this.btnSupplierChooser_Click);
            // 
            // btnChooseSupplier
            // 
            this.btnChooseSupplier.Location = new System.Drawing.Point(212, 191);
            this.btnChooseSupplier.Name = "btnChooseSupplier";
            this.btnChooseSupplier.Size = new System.Drawing.Size(100, 36);
            this.btnChooseSupplier.TabIndex = 2;
            this.btnChooseSupplier.Text = "Edit Part";
            this.btnChooseSupplier.UseVisualStyleBackColor = true;
            this.btnChooseSupplier.Click += new System.EventHandler(this.btnEditPart_Click);
            // 
            // dataGridViewParts2
            // 
            this.dataGridViewParts2.AllowUserToAddRows = false;
            this.dataGridViewParts2.AllowUserToDeleteRows = false;
            this.dataGridViewParts2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParts2.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewParts2.Name = "dataGridViewParts2";
            this.dataGridViewParts2.Size = new System.Drawing.Size(623, 169);
            this.dataGridViewParts2.TabIndex = 0;
            // 
            // btnSaveToDatabase
            // 
            this.btnSaveToDatabase.Location = new System.Drawing.Point(274, 422);
            this.btnSaveToDatabase.Name = "btnSaveToDatabase";
            this.btnSaveToDatabase.Size = new System.Drawing.Size(100, 35);
            this.btnSaveToDatabase.TabIndex = 2;
            this.btnSaveToDatabase.Text = "Place the Order";
            this.btnSaveToDatabase.UseVisualStyleBackColor = true;
            this.btnSaveToDatabase.Click += new System.EventHandler(this.btnSaveToDatabase_Click);
            // 
            // frmDisSupplierOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 477);
            this.Controls.Add(this.btnSaveToDatabase);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "frmDisSupplierOrder";
            this.Text = "Supplier Order";
            this.Load += new System.EventHandler(this.frmDisSupplierOrder_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridViewParts2;
        private System.Windows.Forms.Button btnChooseSupplier;
        private System.Windows.Forms.Button btnSupplierChooser;
        private System.Windows.Forms.Button btnSaveToDatabase;
        private System.Windows.Forms.Button btn_OrderDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalPrice;
    }
}