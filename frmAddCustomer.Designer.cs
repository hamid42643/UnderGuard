namespace WindowsFormsApplication
{
    partial class frmAddCustomer
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
            this.txtCUSTOMER_name = new System.Windows.Forms.TextBox();
            this.txtCUSTOMER_phone = new System.Windows.Forms.TextBox();
            this.txtCUSTOMER_email = new System.Windows.Forms.TextBox();
            this.txtCUSTOMER_address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCUSTOMER_name
            // 
            this.txtCUSTOMER_name.Location = new System.Drawing.Point(111, 23);
            this.txtCUSTOMER_name.Name = "txtCUSTOMER_name";
            this.txtCUSTOMER_name.Size = new System.Drawing.Size(130, 20);
            this.txtCUSTOMER_name.TabIndex = 0;
            // 
            // txtCUSTOMER_phone
            // 
            this.txtCUSTOMER_phone.Location = new System.Drawing.Point(111, 49);
            this.txtCUSTOMER_phone.Name = "txtCUSTOMER_phone";
            this.txtCUSTOMER_phone.Size = new System.Drawing.Size(130, 20);
            this.txtCUSTOMER_phone.TabIndex = 1;
            // 
            // txtCUSTOMER_email
            // 
            this.txtCUSTOMER_email.Location = new System.Drawing.Point(111, 75);
            this.txtCUSTOMER_email.Name = "txtCUSTOMER_email";
            this.txtCUSTOMER_email.Size = new System.Drawing.Size(130, 20);
            this.txtCUSTOMER_email.TabIndex = 2;
            // 
            // txtCUSTOMER_address
            // 
            this.txtCUSTOMER_address.Location = new System.Drawing.Point(111, 106);
            this.txtCUSTOMER_address.Name = "txtCUSTOMER_address";
            this.txtCUSTOMER_address.Size = new System.Drawing.Size(243, 20);
            this.txtCUSTOMER_address.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Customer name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Customer phone";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Customer email";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Customer address";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(152, 193);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Save";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCUSTOMER_name);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCUSTOMER_phone);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCUSTOMER_email);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCUSTOMER_address);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 155);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer";
            // 
            // frmAddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 236);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAdd);
            this.Name = "frmAddCustomer";
            this.Text = "Add/Edit Customer";
            this.Load += new System.EventHandler(this.frmAddCustomer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCUSTOMER_name;
        private System.Windows.Forms.TextBox txtCUSTOMER_phone;
        private System.Windows.Forms.TextBox txtCUSTOMER_email;
        private System.Windows.Forms.TextBox txtCUSTOMER_address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}