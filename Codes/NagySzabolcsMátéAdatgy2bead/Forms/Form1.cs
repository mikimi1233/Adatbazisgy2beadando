﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagySzabolcsMátéAdatgy2bead
{
    public partial class Form1 : Form
    {
        private List<Animals> animals;
        private List<Dogs> dogs;
        private List<Owners> owners;
        public Form1()
        {
            InitializeComponent();
            Initializecb_OwnORAni();
            Initializedgv_table();
            UpdateDgv_table();
        }

        private void Initializecb_OwnORAni()
        {
            cb_OwnORAni.Items.Add("Owner");
            cb_OwnORAni.Items.Add("Animal");
            cb_OwnORAni.SelectedItem = cb_OwnORAni.Items[0];
        }

        private void btn_NewOwner_Click(object sender, EventArgs e)
        {
            NewOwner newOwner = new NewOwner(this);
            newOwner.ShowDialog();
        }

        private void btn_newanimal_Click(object sender, EventArgs e)
        {
            NewAnimal newAnimal = new NewAnimal();
            newAnimal.ShowDialog();
        }

        private void cb_OwnORAni_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cb_OwnORAni.SelectedIndex == 0)
            {
                Initializedgv_table();
                UpdateDgv_table();
            }
            else
            {
                Initializedgv_table();
                UpdateDgv_table();
            }
        }

        private void Initializedgv_table()
        {
            dgv_table.Columns.Clear();
            dgv_table.Rows.Clear();
            dgv_table.AutoGenerateColumns = false;
            if (cb_OwnORAni.SelectedItem == cb_OwnORAni.Items[0])
            {
                dgv_table.Columns.Add("Id", "Id");
                dgv_table.Columns.Add("Fname", "Family name");
                dgv_table.Columns.Add("Sname", "Surname");
                dgv_table.Columns.Add("BirthDate", "Birth date");
            }
            else
            {
                dgv_table.Columns.Add("Id", "Id");
                dgv_table.Columns.Add("Animalname", "Animal name");
                dgv_table.Columns.Add("BirthDate", "Birth date");
                dgv_table.Columns.Add("Neme", "Gender");
                dgv_table.Columns.Add("Ivartalanitott", "Neutered");
            }

            DataGridViewButtonColumn dgvbcRead = new DataGridViewButtonColumn();
            dgvbcRead.Text = "View";
            dgvbcRead.Name = "Datas";
            dgvbcRead.UseColumnTextForButtonValue = true;
            dgv_table.Columns.Add(dgvbcRead);
            dgv_table.AutoResizeColumns();

            DataGridViewButtonColumn dgvbcSet = new DataGridViewButtonColumn();
            dgvbcSet.Text = "Change";
            dgvbcSet.Name = "Change";
            dgvbcSet.UseColumnTextForButtonValue = true;
            dgv_table.Columns.Add(dgvbcSet);
            dgv_table.AutoResizeColumns();

            DataGridViewButtonColumn dgvbcDelete = new DataGridViewButtonColumn();
            dgvbcDelete.Text = "Delete";
            dgvbcDelete.Name = "Delete";
            dgvbcDelete.UseColumnTextForButtonValue = true;
            dgv_table.Columns.Add(dgvbcDelete);
            dgv_table.AutoResizeColumns();

        }

        private void dgv_table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            switch (dgv.Columns[e.ColumnIndex].Name)
            {
                case "Datas":
                    if (cb_OwnORAni.SelectedIndex == 0)
                    {
                       OwnerDatas ownerDatas = new OwnerDatas(int.Parse(dgv.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                        ownerDatas.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Nem lett kidolgozva");
                    }

                    break;
                case "Change":
                    ChangeDatas(int.Parse(dgv.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                    break;
                case "Delete":
                    DeleteDatas(int.Parse(dgv.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                    break;
                default:
                    break;
            }
        }

        public void UpdateDgv_table()
        {
            dgv_table.Rows.Clear();
            if (cb_OwnORAni.SelectedIndex == 0)
            {
                OwnerTableHandler ownerTableHandler = new OwnerTableHandler();
                owners = ownerTableHandler.Select();
                foreach (Owners o in owners)
                {
                    dgv_table.Rows.Add(new object[]
                    {
                        o.OwnerId,
                        o.FamilyName,
                        o.Surname,
                        o.BirthDate.ToString("yyyy.MM.dd")
                    });
                }
            }
            else
            {
                AnimalTableHandler animalTableHandler = new AnimalTableHandler();
                animals = animalTableHandler.Select();
                foreach (Animals animal in animals)
                {
                    dgv_table.Rows.Add(new object[]
                    {
                        animal.Id,
                        animal.AnimalName,
                        animal.BirthDate.ToString("yyyy.MM.dd"),
                        animal.Neme,
                        animal.Neutered == 0 ?  "nem" : "igen"
                    });
                }
            }
        }

        public void DeleteDatas(int number)
        {
            if (cb_OwnORAni.SelectedIndex == 0)
            {
                OwnerTableHandler ownerTableHandler = new OwnerTableHandler();
                int deletedrecordnumber = ownerTableHandler.Delete(number);
                MessageBox.Show(deletedrecordnumber + "rekord sikeresen törölve!");
                owners = ownerTableHandler.Select();
                ownerTableHandler.Delete(number);
                Initializedgv_table();
                UpdateDgv_table();
            }
            else
            {
                AnimalTableHandler animalTableHandler = new AnimalTableHandler();
                int deletedrecordnumber = animalTableHandler.Delete(number);
                MessageBox.Show(deletedrecordnumber + "rekord sikeresen törölve!");
                animals = animalTableHandler.Select();
                Initializedgv_table();
                UpdateDgv_table();
            }
        }

        public void ChangeDatas(int number)
        {
            if (cb_OwnORAni.SelectedIndex == 0)
            {
                ChangeOwnerDatas set = new ChangeOwnerDatas(number, this);
                set.ShowDialog();
            }
            //else
            //{
            //    ChangeAnimalDatas set = new ChangeAnimalDatas(number, this);
            //    set.ShowDialog();
            //}
        }
    }
}
