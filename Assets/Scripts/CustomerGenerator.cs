using UnityEngine;
using System.Collections.Generic;
using System.IO;



public class CustomerGenerator : MonoBehaviour
{
    public class Customer
    {
        public float arrivalTime;
        public float serviceTime;
    }
    public float meanInterarrivalTime = 5.0f;
    public float interarrivalTimeStdDev = 1.0f;
    public float meanServiceTime = 2.0f;
    public float serviceTimeStdDev = 0.5f;
    public int numCustomers = 10;

    void Start()
    {
        // read customer arrival and service time data from CSV file
        List<Customer> customers = new List<Customer>();
        string path = "customer_data.txt";
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            string[] values = reader.ReadLine().Split(',');
            Customer customer = new Customer();
            customer.arrivalTime = float.Parse(values[0]);
            customer.serviceTime = float.Parse(values[1]);
            customers.Add(customer);
        }

        // generate interarrival time and service time data
        System.Random rand = new System.Random();
        for (int i = 0; i < numCustomers; i++)
        {
            float interarrivalTime = (float)rand.NextDouble() * interarrivalTimeStdDev +
                                      meanInterarrivalTime;
            float serviceTime = (float)rand.NextDouble() * serviceTimeStdDev +
                                meanServiceTime;
            Customer customer = new Customer();
            customer.arrivalTime = i == 0 ? 0.0f : customers[i - 1].arrivalTime + interarrivalTime;
            customer.serviceTime = serviceTime;
            customers.Add(customer);
        }

        // display customer data
        foreach (Customer customer in customers)
        {
            Debug.Log("Arrival time: " + customer.arrivalTime + ", Service time: " + customer.serviceTime);
        }
    }
}
