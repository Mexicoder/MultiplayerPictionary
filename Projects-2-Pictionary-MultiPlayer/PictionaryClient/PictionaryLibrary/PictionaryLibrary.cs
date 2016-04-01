using System;
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
        void FinishCurrentGame(bool status = false);
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
        [OperationContract]
        bool CheckWord(string word, string userName);
        [OperationContract]
        string GetWord();
    }

    /*--------------------------------- Service Implementation -------------------------------*/

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DrawCanvasBoard : IUser
    {
        private Dictionary<string, IUserCallback> _userCallbacks = new Dictionary<string, IUserCallback>();
        private string _drawLine;
        private DrawWord _drawWord = null;

        /*----------------------------------- IUser methods ----------------------------------*/

        public bool Join(string name)
        {
            if (_userCallbacks.ContainsKey(name.ToUpper()))
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
                _userCallbacks.Add(name.ToUpper(), cb);
                //drawLine = new string();
                if (_drawWord == null)
                {
                    _drawWord = DrawWord.GenerateDrawWord();
                }

                return true;
            }
        }

        public void Leave(string name)
        {
            if (_userCallbacks.ContainsKey(name.ToUpper()))
            {
                _userCallbacks.Remove(name.ToUpper());
                Console.WriteLine("User leave");

                //drawLine = new string();
            }
        }

        public void PostLine(string jsonLine)
        {
            Console.WriteLine("drawLine Post");

            _drawLine = jsonLine;
            updateAllUsersCanvas();
        }

        // TODO: make this return all line later
        public string GetLine()
        {
            Console.WriteLine("drawLine get");
            return _drawLine;

        }

        private void updateAllUsersCanvas()
        {
            //TODO try getting rid of this 
            //string c = drawLine;
            Console.WriteLine("drawLine callback");
            foreach (IUserCallback cb in _userCallbacks.Values)
                cb.SendLine(_drawLine);
        }

        public string GetWord()
        {
            return _drawWord.word_;
        }

        public bool CheckWord(string word, string userName)
        {
            if (word == _drawWord.word_)
            {

                updateAllUsersGameStatus(userName);
                return true;
            }
            else
                return false;
        }

        private void updateAllUsersGameStatus(string userWinner)
        {
            Console.WriteLine("Game Status callback");

            foreach (var user in _userCallbacks)
            {
                if (user.Key == userWinner)
                {
                    user.Value.FinishCurrentGame(true);
                    continue;
                }
                user.Value.FinishCurrentGame();
            }
        }
    }
}
