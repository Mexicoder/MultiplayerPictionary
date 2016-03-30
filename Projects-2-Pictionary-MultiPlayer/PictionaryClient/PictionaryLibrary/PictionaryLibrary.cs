using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PictionaryLibrary
{
    public interface IUserCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendCanvas(Canvas canvas);
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
        void PostCanvas(Canvas canvas);
        [OperationContract]
        Canvas GetCanvas();
    }

    /*--------------------------------- Service Implementation -------------------------------*/

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DrawCanvasBoard : IUser
    {
        private Dictionary<string, IUserCallback> userCallbacks = new Dictionary<string, IUserCallback>();
        private Canvas drawCanvas;

        /*----------------------------------- IUser methods ----------------------------------*/

        public bool Join(string name)
        {
            if (userCallbacks.ContainsKey(name.ToUpper()))
                // User alias must be unique
                return false;
            else
            {

                // Retrieve client's callback proxy
                IUserCallback cb = OperationContext.Current.GetCallbackChannel<IUserCallback>();

                // Save alias and callback proxy    
                userCallbacks.Add(name.ToUpper(), cb);
                drawCanvas = new Canvas();

                return true;
            }
        }

        public void Leave(string name)
        {
            if (userCallbacks.ContainsKey(name.ToUpper()))
            {
                userCallbacks.Remove(name.ToUpper());
                drawCanvas = new Canvas();
            }
        }

        public void PostCanvas(Canvas canvas)
        {
            drawCanvas = canvas;
            updateAllUsers();
        }

        public Canvas GetCanvas()
        {
            return drawCanvas;
        }

        /*---------------------------------- Helper methods ----------------------------------*/

        private void updateAllUsers()
        {
            //TODO try getting rid of this 
            Canvas c = drawCanvas;
            foreach (IUserCallback cb in userCallbacks.Values)
                cb.SendCanvas(c);
        }


    }
}
