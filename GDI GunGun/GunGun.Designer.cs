﻿namespace GDI_GunGun
{
    partial class GunGun
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GunGun));
            this.SuspendLayout();
            // 
            // GunGun
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackgroundImage = global::GDI_GunGun.Properties.Resources.bggg;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GunGun";
            this.Load += new System.EventHandler(this.GunGun_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GunGun_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GunGun_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GunGun_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

