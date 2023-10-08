﻿using EmployeeDetails.Models;
using Newtonsoft.Json;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;

namespace EmployeeDetails.Service
{
    public class APIGateway
    {
        private string url = "http://localhost:7085/api/EmployeeAPI";
        private HttpClient HttpClient = new HttpClient();
        public APIGateway() 
        {
        } 

        public List<Employee> ListEmployee()
        {
            List<Employee> employee = new List<Employee>();
            string apiUrl = $"{url}/GetEmpData";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol= SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = HttpClient.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Employee>>(result);
                    if(data != null)
                    {
                        employee = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured:" + result);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured:" + ex.Message);
            }
            return employee;
        }
        public Employee CreateEmployee(Employee employee)
        {
            string apiUrl = $"{url}/AddEmpData";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string json = JsonConvert.SerializeObject(employee);
            try
            {
                HttpResponseMessage response = HttpClient.PostAsync(apiUrl,new StringContent(json,Encoding.UTF8,"application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Employee>(result);
                    if (data != null)
                    {
                        employee = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured:" + result);
                }
            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message); 
            }
            return employee;
        }
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            string apiUrl = $"{url}/GetEmpDataById?Empid={id}";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = HttpClient.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Employee>(result);
                    if (data != null)
                    {
                        employee = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured:" + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured:" + ex.Message);
            }
            return employee;

        }

        public void DeleteEmployee(int id)
        {
            string apiUrl = $"{url}/DeleteEmp?id={id}";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = HttpClient.DeleteAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured:" + result);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured:" + ex.Message);
            }
           
        }
    }
}
