namespace Speles_serveris
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.UI_Uptime = new System.Windows.Forms.Label();
            this.UI_Clients_Connected = new System.Windows.Forms.Label();
            this.UI_State = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(798, 633);
            this.listBox1.TabIndex = 0;
            // 
            // UI_Uptime
            // 
            this.UI_Uptime.AutoSize = true;
            this.UI_Uptime.Location = new System.Drawing.Point(162, 0);
            this.UI_Uptime.Margin = new System.Windows.Forms.Padding(0);
            this.UI_Uptime.Name = "UI_Uptime";
            this.UI_Uptime.Size = new System.Drawing.Size(116, 13);
            this.UI_Uptime.TabIndex = 1;
            this.UI_Uptime.Text = "Servera laiks: 00:00:00";
            // 
            // UI_Clients_Connected
            // 
            this.UI_Clients_Connected.AutoSize = true;
            this.UI_Clients_Connected.Location = new System.Drawing.Point(0, 0);
            this.UI_Clients_Connected.Margin = new System.Windows.Forms.Padding(0);
            this.UI_Clients_Connected.Name = "UI_Clients_Connected";
            this.UI_Clients_Connected.Size = new System.Drawing.Size(84, 13);
            this.UI_Clients_Connected.TabIndex = 2;
            this.UI_Clients_Connected.Text = "Savienojušies: 0";
            // 
            // UI_State
            // 
            this.UI_State.Location = new System.Drawing.Point(0, 0);
            this.UI_State.Name = "UI_State";
            this.UI_State.Size = new System.Drawing.Size(100, 23);
            this.UI_State.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 613);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(798, 20);
            this.textBox1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.UI_Uptime);
            this.panel1.Controls.Add(this.UI_Clients_Connected);
            this.panel1.Controls.Add(this.UI_State);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 590);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 23);
            this.panel1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(798, 633);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Serveris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.Label UI_Uptime;
        private System.Windows.Forms.Label UI_Clients_Connected;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label UI_State;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;

        #endregion
    }
}

