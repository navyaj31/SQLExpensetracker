using SqlExpenseTracker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Input;

namespace SqlExpenseTracker
{
    public  class Expensetracker
    {
        public void AddTransaction()
        {
            SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=Expensetrackerapp; Integrated security=true");
            con.Open();
            

            SqlCommand cmd = new SqlCommand($"insert into Expensetracker values(@title,@description,@amount,@date)", con);
            Console.WriteLine("Enter Transaction Title: ");
            string Title = Console.ReadLine();

            Console.WriteLine("Enter the description of the Transaction:  ");
            string Description = Console.ReadLine();

            Console.WriteLine("Enter the amount");
            double Amount = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Date(DD/MM/YYYY): ");
            DateTime Date = DateTime.Parse(Console.ReadLine());

            cmd.Parameters.AddWithValue("@title", Title);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@amount", Amount);
            cmd.Parameters.AddWithValue("@date", Date);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Transaction  successfully added");

            con.Close();
        }
        public void viewExpenses()
        {
            Console.WriteLine("Expenses:");
            Console.WriteLine("Title\t\t\tDescription\t\t\tAmount\t\t\tDate");
            SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=Expensetrackerapp; Integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand($"Select * from Expensetracker where Amount<0",con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int no_of_columns = dr.FieldCount;
                for (int i = 0; i < no_of_columns; i++)
                {
                    Console.Write($"{dr[i]}\t");
                }
                Console.WriteLine();
            }
            con.Close();
        }

        public void ViewIncome()
        {
            Console.WriteLine("Income:");
            Console.WriteLine("Title\t\t\t\tDescription\t\t\t\tAmount\t\t\tDate");
            SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=Expensetrackerapp; Integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand($"Select * from Expensetracker where Amount>0", con);
            SqlDataReader dr= cmd.ExecuteReader();
            while (dr.Read())
            {
                int no_of_columns = dr.FieldCount;
                for (int i = 0; i < no_of_columns; i++)
                {
                    Console.Write($"{dr[i]}\t");
                }
                Console.WriteLine();
            }
            con.Close();
        }

        public void CheckAvailableBalance()
        {
            SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=Expensetrackerapp; Integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand($"select sum(Amount) as TotalBalance from Expensetracker", con);
            var Total_Balance = cmd.ExecuteScalar();
            Console.WriteLine($"Total Balance = {(int)Total_Balance}");
            con.Close();
        }
    }
 }

internal class Program
{
    static void Main(string[] args)
    {
        Expensetracker expensetracker = new Expensetracker();

        while (true)
        {
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View Expense");
            Console.WriteLine("3. View Income");
            Console.WriteLine("4. CheckAvailableBalance");
            Console.WriteLine("Enter Your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        expensetracker.AddTransaction();
                        break;
                    }
                case 2:
                    {
                        expensetracker.viewExpenses();
                        break;
                    }
                case 3:
                    {
                        expensetracker.ViewIncome();
                        break;
                    }
                case 4:
                    {
                        expensetracker.CheckAvailableBalance();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Choice");
                        break;
                    }
            }
        }
    }
}
    


    
            
