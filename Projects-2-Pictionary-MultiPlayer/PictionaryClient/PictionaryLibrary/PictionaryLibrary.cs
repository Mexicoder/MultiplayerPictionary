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
        [OperationContract(IsOneWay = true)]
        void CheckWord(string word, string userName);
        [OperationContract]
        string GetWordHint();

        [OperationContract]
        bool isDrawer(string userName);
    }

    /*--------------------------------- Service Implementation -------------------------------*/

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DrawCanvasBoard : IUser
    {
        private Dictionary<string, IUserCallback> _userCallbacks = new Dictionary<string, IUserCallback>();
        private string _drawerUser = "";
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

                // the first player to join will become the drawer for first game
                if (_userCallbacks.Count == 1)
                {
                    _drawerUser = name.ToUpper();
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

        public string GetWordHint()
        {
            return _drawWord.wordHintFirstLetter_;
        }

        public void CheckWord(string word, string userName)
        {
            if (string.Equals(word ,_drawWord.word_, StringComparison.OrdinalIgnoreCase))
            {
                updateAllUsersGameStatus(userName);
                _drawerUser = userName;
            }
        }

        private void updateAllUsersGameStatus(string userWinner)
        {
            Console.WriteLine("Game Status callback");

            foreach (var user in _userCallbacks)
            {
                if (string.Equals(user.Key ,userWinner, StringComparison.OrdinalIgnoreCase))
                {
                    user.Value.FinishCurrentGame(true);
                    continue;
                }
                user.Value.FinishCurrentGame();
            }
        }

        public bool isDrawer(string userName)
        {
            if (_drawerUser == userName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
