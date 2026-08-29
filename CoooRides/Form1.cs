using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoooRides
{
    public partial class Form1 : Form
    {
       
        private Label lblCarQueue;
        private Label lblMiniBusQueue;
        private GroupBox groupBox1;
        private RadioButton rbnBlackLUX1000;
        private RadioButton rbnWhiteMV500;
        private RadioButton rbnWhiteLUX1000;
        private RadioButton rbnBlackMV500;
        private Button btnOrder;
        private Label lblCarAssembly;
        private Label lblMiniBusAssembly;
        private Label lblSpraybooth;
        private Label lblCarLineStatus;
        private Label lblMinibusLineStatus;
        private Label lblSprayboothStatus;
        private Label lblCarQueueCount;
        private Label lblMinibusQueueCount;

       
        private CorporateHQ _hq;
        private CarAssemblyLine _carLine;
        private MinibusAssemblyLine _minibusLine;

        public Form1()
        {
           
            InitializeComponent();

            
            _hq = new CorporateHQ();
            _carLine = new CarAssemblyLine();
            _minibusLine = new MinibusAssemblyLine();

           
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);

            
            _hq.OnCarQueueCountChanged += UpdateCarQueueUI;
            _hq.OnMinibusQueueCountChanged += UpdateMinibusQueueUI;

            _carLine.OnStatusChanged += UpdateCarStatusUI;
            _minibusLine.OnStatusChanged += UpdateMinibusStatusUI;

            Spraybooth.Instance.OnStatusChanged += UpdateSprayboothStatusUI;
        }

        private void InitializeComponent()
        {
            this.lblCarQueue = new System.Windows.Forms.Label();
            this.lblMiniBusQueue = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnBlackLUX1000 = new System.Windows.Forms.RadioButton();
            this.rbnWhiteMV500 = new System.Windows.Forms.RadioButton();
            this.rbnWhiteLUX1000 = new System.Windows.Forms.RadioButton();
            this.rbnBlackMV500 = new System.Windows.Forms.RadioButton();
            this.btnOrder = new System.Windows.Forms.Button();
            this.lblCarAssembly = new System.Windows.Forms.Label();
            this.lblMiniBusAssembly = new System.Windows.Forms.Label();
            this.lblSpraybooth = new System.Windows.Forms.Label();
            this.lblCarLineStatus = new System.Windows.Forms.Label();
            this.lblMinibusLineStatus = new System.Windows.Forms.Label();
            this.lblSprayboothStatus = new System.Windows.Forms.Label();
            this.lblCarQueueCount = new System.Windows.Forms.Label();
            this.lblMinibusQueueCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCarQueue
            // 
            this.lblCarQueue.AutoSize = true;
            this.lblCarQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarQueue.Location = new System.Drawing.Point(416, 43);
            this.lblCarQueue.Name = "lblCarQueue";
            this.lblCarQueue.Size = new System.Drawing.Size(113, 22);
            this.lblCarQueue.TabIndex = 0;
            this.lblCarQueue.Text = "Car Queue:";
            this.lblCarQueue.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblMiniBusQueue
            // 
            this.lblMiniBusQueue.AutoSize = true;
            this.lblMiniBusQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiniBusQueue.Location = new System.Drawing.Point(859, 43);
            this.lblMiniBusQueue.Name = "lblMiniBusQueue";
            this.lblMiniBusQueue.Size = new System.Drawing.Size(151, 22);
            this.lblMiniBusQueue.TabIndex = 1;
            this.lblMiniBusQueue.Text = "MiniBus Queue:";
            this.lblMiniBusQueue.Click += new System.EventHandler(this.lblMiniBusQueue_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbnBlackLUX1000);
            this.groupBox1.Controls.Add(this.rbnWhiteMV500);
            this.groupBox1.Controls.Add(this.rbnWhiteLUX1000);
            this.groupBox1.Controls.Add(this.rbnBlackMV500);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 167);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // rbnBlackLUX1000
            // 
            this.rbnBlackLUX1000.AutoSize = true;
            this.rbnBlackLUX1000.Location = new System.Drawing.Point(6, 21);
            this.rbnBlackLUX1000.Name = "rbnBlackLUX1000";
            this.rbnBlackLUX1000.Size = new System.Drawing.Size(155, 26);
            this.rbnBlackLUX1000.TabIndex = 0;
            this.rbnBlackLUX1000.TabStop = true;
            this.rbnBlackLUX1000.Text = "Black LUX1000";
            this.rbnBlackLUX1000.UseVisualStyleBackColor = true;
            this.rbnBlackLUX1000.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbnWhiteMV500
            // 
            this.rbnWhiteMV500.AutoSize = true;
            this.rbnWhiteMV500.Location = new System.Drawing.Point(6, 99);
            this.rbnWhiteMV500.Name = "rbnWhiteMV500";
            this.rbnWhiteMV500.Size = new System.Drawing.Size(138, 26);
            this.rbnWhiteMV500.TabIndex = 5;
            this.rbnWhiteMV500.TabStop = true;
            this.rbnWhiteMV500.Text = "White MV500";
            this.rbnWhiteMV500.UseVisualStyleBackColor = true;
            // 
            // rbnWhiteLUX1000
            // 
            this.rbnWhiteLUX1000.AutoSize = true;
            this.rbnWhiteLUX1000.Location = new System.Drawing.Point(6, 47);
            this.rbnWhiteLUX1000.Name = "rbnWhiteLUX1000";
            this.rbnWhiteLUX1000.Size = new System.Drawing.Size(157, 26);
            this.rbnWhiteLUX1000.TabIndex = 3;
            this.rbnWhiteLUX1000.TabStop = true;
            this.rbnWhiteLUX1000.Text = "White LUX1000";
            this.rbnWhiteLUX1000.UseVisualStyleBackColor = true;
            // 
            // rbnBlackMV500
            // 
            this.rbnBlackMV500.AutoSize = true;
            this.rbnBlackMV500.Location = new System.Drawing.Point(6, 73);
            this.rbnBlackMV500.Name = "rbnBlackMV500";
            this.rbnBlackMV500.Size = new System.Drawing.Size(136, 26);
            this.rbnBlackMV500.TabIndex = 4;
            this.rbnBlackMV500.TabStop = true;
            this.rbnBlackMV500.Text = "Black MV500";
            this.rbnBlackMV500.UseVisualStyleBackColor = true;
            // 
            // btnOrder
            // 
            this.btnOrder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.Location = new System.Drawing.Point(12, 240);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(158, 96);
            this.btnOrder.TabIndex = 3;
            this.btnOrder.Text = "Order";
            this.btnOrder.UseVisualStyleBackColor = false;
            // 
            // lblCarAssembly
            // 
            this.lblCarAssembly.AutoSize = true;
            this.lblCarAssembly.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarAssembly.Location = new System.Drawing.Point(646, 204);
            this.lblCarAssembly.Name = "lblCarAssembly";
            this.lblCarAssembly.Size = new System.Drawing.Size(177, 22);
            this.lblCarAssembly.TabIndex = 4;
            this.lblCarAssembly.Text = "Car Assembly Line";
            this.lblCarAssembly.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblMiniBusAssembly
            // 
            this.lblMiniBusAssembly.AutoSize = true;
            this.lblMiniBusAssembly.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiniBusAssembly.Location = new System.Drawing.Point(646, 320);
            this.lblMiniBusAssembly.Name = "lblMiniBusAssembly";
            this.lblMiniBusAssembly.Size = new System.Drawing.Size(219, 22);
            this.lblMiniBusAssembly.TabIndex = 5;
            this.lblMiniBusAssembly.Text = "Minibus Assembly Line:";
            // 
            // lblSpraybooth
            // 
            this.lblSpraybooth.AutoSize = true;
            this.lblSpraybooth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpraybooth.Location = new System.Drawing.Point(646, 412);
            this.lblSpraybooth.Name = "lblSpraybooth";
            this.lblSpraybooth.Size = new System.Drawing.Size(118, 22);
            this.lblSpraybooth.TabIndex = 6;
            this.lblSpraybooth.Text = "Spraybooth:";
            this.lblSpraybooth.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblCarLineStatus
            // 
            this.lblCarLineStatus.AutoSize = true;
            this.lblCarLineStatus.Location = new System.Drawing.Point(631, 265);
            this.lblCarLineStatus.Name = "lblCarLineStatus";
            this.lblCarLineStatus.Size = new System.Drawing.Size(35, 20);
            this.lblCarLineStatus.TabIndex = 7;
            this.lblCarLineStatus.Text = "Idle";
            // 
            // lblMinibusLineStatus
            // 
            this.lblMinibusLineStatus.AutoSize = true;
            this.lblMinibusLineStatus.Location = new System.Drawing.Point(630, 373);
            this.lblMinibusLineStatus.Name = "lblMinibusLineStatus";
            this.lblMinibusLineStatus.Size = new System.Drawing.Size(35, 20);
            this.lblMinibusLineStatus.TabIndex = 8;
            this.lblMinibusLineStatus.Text = "Idle";
            this.lblMinibusLineStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSprayboothStatus
            // 
            this.lblSprayboothStatus.AutoSize = true;
            this.lblSprayboothStatus.Location = new System.Drawing.Point(630, 483);
            this.lblSprayboothStatus.Name = "lblSprayboothStatus";
            this.lblSprayboothStatus.Size = new System.Drawing.Size(35, 20);
            this.lblSprayboothStatus.TabIndex = 9;
            this.lblSprayboothStatus.Text = "Idle";
            // 
            // lblCarQueueCount
            // 
            this.lblCarQueueCount.AutoSize = true;
            this.lblCarQueueCount.Location = new System.Drawing.Point(415, 119);
            this.lblCarQueueCount.Name = "lblCarQueueCount";
            this.lblCarQueueCount.Size = new System.Drawing.Size(18, 20);
            this.lblCarQueueCount.TabIndex = 10;
            this.lblCarQueueCount.Text = "0";
            // 
            // lblMinibusQueueCount
            // 
            this.lblMinibusQueueCount.AutoSize = true;
            this.lblMinibusQueueCount.Location = new System.Drawing.Point(851, 119);
            this.lblMinibusQueueCount.Name = "lblMinibusQueueCount";
            this.lblMinibusQueueCount.Size = new System.Drawing.Size(18, 20);
            this.lblMinibusQueueCount.TabIndex = 11;
            this.lblMinibusQueueCount.Text = "0";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1180, 637);
            this.Controls.Add(this.lblMinibusQueueCount);
            this.Controls.Add(this.lblCarQueueCount);
            this.Controls.Add(this.lblSprayboothStatus);
            this.Controls.Add(this.lblMinibusLineStatus);
            this.Controls.Add(this.lblCarLineStatus);
            this.Controls.Add(this.lblSpraybooth);
            this.Controls.Add(this.lblMiniBusAssembly);
            this.Controls.Add(this.lblCarAssembly);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMiniBusQueue);
            this.Controls.Add(this.lblCarQueue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // --- 6. THE LOGIC TRIGGERS ---

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (rbnBlackLUX1000.Checked)
                _hq.PlaceOrder(new BuildOrderCommand(_carLine, "Black"), "Car");
            else if (rbnWhiteLUX1000.Checked)
                _hq.PlaceOrder(new BuildOrderCommand(_carLine, "White"), "Car");
            else if (rbnBlackMV500.Checked)
                _hq.PlaceOrder(new BuildOrderCommand(_minibusLine, "Black"), "Minibus");
            else if (rbnWhiteMV500.Checked)
                _hq.PlaceOrder(new BuildOrderCommand(_minibusLine, "White"), "Minibus");
        }

        // --- 7. THREAD-SAFE UI UPDATES ---

        private void UpdateCarQueueUI(int count)
        {
            if (lblCarQueueCount.InvokeRequired)
            {
                lblCarQueueCount.Invoke(new Action<int>(UpdateCarQueueUI), count);
            }
            else
            {
                lblCarQueueCount.Text = count.ToString();
            }
        }

        private void UpdateMinibusQueueUI(int count)
        {
            if (lblMinibusQueueCount.InvokeRequired)
            {
                lblMinibusQueueCount.Invoke(new Action<int>(UpdateMinibusQueueUI), count);
            }
            else
            {
                lblMinibusQueueCount.Text = count.ToString();
            }
        }

        private void UpdateCarStatusUI(string status)
        {
            if (lblCarLineStatus.InvokeRequired)
            {
                lblCarLineStatus.Invoke(new Action<string>(UpdateCarStatusUI), status);
            }
            else
            {
                lblCarLineStatus.Text = status;
            }
        }

        private void UpdateMinibusStatusUI(string status)
        {
            if (lblMinibusLineStatus.InvokeRequired)
            {
                lblMinibusLineStatus.Invoke(new Action<string>(UpdateMinibusStatusUI), status);
            }
            else
            {
                lblMinibusLineStatus.Text = status;
            }
        }

        private void UpdateSprayboothStatusUI(string status)
        {
            if (lblSprayboothStatus.InvokeRequired)
            {
                lblSprayboothStatus.Invoke(new Action<string>(UpdateSprayboothStatusUI), status);
            }
            else
            {
                lblSprayboothStatus.Text = status;
            }
        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }

        private void lblMiniBusQueue_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}