﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PictionaryLibrary
{
    public interface IUserCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendLine(string jsonLine);
        [OperationContract(IsOneWay = true)]
        void SendWord();

    }

    /*----------------------------------- Service Contracts ----------------------------------*/

    [ServiceContract(CallbackContract = typeof(IUserCallback))]
    public interface IUser
    {
        [OperationContract]
        bool Join(string name);
        [OperationContract(IsOneWay = true)]
        void Leave(string name);
        [OperationContract(IsOneWay = true)]
        void PostLine(string jsonLine);
        [OperationContract]
        string GetLine();
    }

    /*--------------------------------- Service Implementation -------------------------------*/

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DrawCanvasBoard : IUser
    {
        private Dictionary<string, IUserCallback> userCallbacks = new Dictionary<string, IUserCallback>();
        private string drawLine;

        /*----------------------------------- IUser methods ----------------------------------*/

        public bool Join(string name)
        {
            if (userCallbacks.ContainsKey(name.ToUpper()))
            {
                // User alias must be unique
                return false;
            }
            else
            {
                Console.WriteLine("User join");

                // Retrieve client's callback proxy
                IUserCallback cb = OperationContext.Current.GetCallbackChannel<IUserCallback>();

                // Save alias and callback proxy    
                userCallbacks.Add(name.ToUpper(), cb);
                //drawLine = new string();

                return true;
            }
        }

        public void Leave(string name)
        {
            if (userCallbacks.ContainsKey(name.ToUpper()))
            {
                userCallbacks.Remove(name.ToUpper());
                Console.WriteLine("User leave");

                //drawLine = new string();
            }
        }

        public void PostLine(string jsonLine)
        {
            Console.WriteLine("drawLine Post");

            drawLine = jsonLine;
            updateAllUsersCanvas();
        }

        // TODO: make this return all line later
        public string GetLine()
        {
            return drawLine;
            Console.WriteLine("drawLine get");

        }

        /*---------------------------------- Helper methods ----------------------------------*/

        private void updateAllUsersCanvas()
        {
            //TODO try getting rid of this 
            string c = drawLine;
            Console.WriteLine("drawLine callback");
            foreach (IUserCallback cb in userCallbacks.Values)
                cb.SendLine(c);
        }


    }
}
