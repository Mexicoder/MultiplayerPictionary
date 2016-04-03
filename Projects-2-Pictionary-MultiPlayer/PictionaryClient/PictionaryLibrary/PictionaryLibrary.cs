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
        void FinishCurrentGame(string winnerUser, bool status = false);
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
        string GetWordHint();
        [OperationContract]
        string GetWord();
        [OperationContract]
        string getDrawer();
        [OperationContract]
        bool isDrawer(string userName);
        [OperationContract]
        string[] getCanvas();
    }

    /*--------------------------------- Service Implementation -------------------------------*/

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DrawCanvasBoard : IUser
    {
        private Dictionary<string, IUserCallback> _userCallbacks = new Dictionary<string, IUserCallback>();
        private string _drawerUser = "";
        private string _drawLine;
        private List<string> _drawCanvas = new List<string>();
        private DrawWord _drawWord = null;

        /*----------------------------------- IUser methods ----------------------------------*/

        public bool Join(string name)
        {
            if (_userCallbacks.ContainsKey(name))
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
                _userCallbacks.Add(name, cb);
                //drawLine = new string();
                if (_drawWord == null)
                {
                    _drawWord = DrawWord.GenerateDrawWord();
                }

                // the first player to join will become the drawer for first game
                if (_userCallbacks.Count == 1)
                {
                    _drawerUser = name;
                }
                return true;
            }
        }

        public void Leave(string name)
        {
            if (_userCallbacks.ContainsKey(name))
            {
                _userCallbacks.Remove(name);
                Console.WriteLine("User leave");

                //drawLine = new string();
            }
        }

        public void PostLine(string jsonLine)
        {
            Console.WriteLine("drawLine Post");

            _drawLine = jsonLine;
            _drawCanvas.Add(jsonLine);
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

        public string GetWord()
        {
            return _drawWord.word_;
        }

        public bool CheckWord(string word, string userName)
        {
            if (string.Equals(word ,_drawWord.word_, StringComparison.OrdinalIgnoreCase))
            {
                _drawWord = DrawWord.GenerateDrawWord();
                updateAllUsersGameStatus(userName);
                _drawerUser = userName;
                return true;
            }
            return false;
        }

        private void updateAllUsersGameStatus(string userWinner)
        {
            Console.WriteLine("Game Status callback");

            foreach (var user in _userCallbacks)
            {
                if (string.Equals(user.Key ,userWinner, StringComparison.OrdinalIgnoreCase))
                {
                    user.Value.FinishCurrentGame(userWinner,true);
                    continue;
                }
                user.Value.FinishCurrentGame(userWinner);
            }
        }

        public string getDrawer()
        {
            return _drawerUser;
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

        public string[] getCanvas()
        {
            return _drawCanvas.ToArray();
        }
    }
}
