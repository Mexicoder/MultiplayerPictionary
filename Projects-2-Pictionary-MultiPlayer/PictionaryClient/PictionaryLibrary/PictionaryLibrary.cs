/// <summary>
///     Author:     Alex Seceanschi & John Friesen
///     Student#:   0690827 & 0666315
///     Date:       Created: April 6, 2016
///     Purpose:    A Visual Studio C# Library project called PictionaryLibrary. 
///                     The main purpose of this project is to implement the logic and callbacks need 
///                     to play the game pictionay on multiple clients and get live updates of current 
///                     standing in the game.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace PictionaryLibrary
{
    public interface IUserCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendLine(string jsonLine);
        [OperationContract(IsOneWay = true)]
        void FinishCurrentGame(string winnerUser, bool status = false);
        [OperationContract(IsOneWay = true)]
        void ResetClientsGame(string newDrawerName);
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

        #region SeriveClientMethods

        /// <summary>
        /// allows a new client to join and adds then to _userCallbacks
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Join(string name)
        {
            try
            {
                if (_userCallbacks.ContainsKey(name))
                {
                    // User alias must be unique
                    return false;
                }
                else
                {
                    Console.WriteLine($"User Joined: {name}");

                    // Retrieve client's callback proxy
                    IUserCallback cb = OperationContext.Current.GetCallbackChannel<IUserCallback>();
                    // Save alias and callback proxy    
                    _userCallbacks.Add(name, cb);
                    if (_drawWord == null)
                    {
                        _drawWord = DrawWord.GenerateDrawWord();
                    }

                    // the first player to join will become the Drawer for first game
                    if (_userCallbacks.Count == 1)
                    {
                        _drawerUser = name;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error user couldn't join properly. User: {name}, Error Message: {ex.Message}");
            }
        }

        /// <summary>
        /// when a client disconnects then removed them from the _userCallbacks
        /// </summary>
        /// <param name="name"></param>
        public void Leave(string name)
        {
            try
            {
                if (_userCallbacks.ContainsKey(name))
                {
                    Console.WriteLine($"User Left: {name}");
                    if (name == _drawerUser)
                    {
                        _drawerUser = _userCallbacks.FirstOrDefault(x => x.Key != name).Key;
                        foreach (var user in _userCallbacks)
                        {
                            user.Value.ResetClientsGame(_drawerUser);
                        }
                    }
                    _userCallbacks.Remove(name);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error user didn't exit properly. User: {name}, Error Message: {ex.Message}");
            }
        }

        /// <summary>
        /// updates all clients accordingly after one of the guessers submits the correct answer 
        /// </summary>
        /// <param name="userWinner"></param>
        private void updateAllUsersGameStatus(string userWinner)
        {
            try
            {
                foreach (var user in _userCallbacks)
                {
                    if (string.Equals(user.Key, userWinner, StringComparison.OrdinalIgnoreCase))
                    {
                        user.Value.FinishCurrentGame(userWinner, true);
                        continue;
                    }
                    user.Value.FinishCurrentGame(userWinner);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error updating all clients with game status .Error Message: " + ex.Message);
            }
        }

        /// <summary>
        /// get a string containing the drawers userName
        /// </summary>
        /// <returns></returns>
        public string getDrawer()
        {
            return _drawerUser;
        }
       
        /// <summary>
        /// check to see if userName matches the drawers user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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

        #endregion

        #region ClientUpdateMethods

        /// <summary>
        /// update all guesser client canvases with the last line
        /// </summary>
        /// <param name="jsonLine"></param>
        public void PostLine(string jsonLine)
        {
            try
            {
                _drawLine = jsonLine;
                _drawCanvas.Add(jsonLine);
                updateAllUsersCanvas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Posting Line .Error Message: " + ex.Message);
            }
        }

        /// <summary>
        /// get all the lines that make up the image in the drawers client canvas
        /// </summary>
        /// <returns>string[]</returns>
        public string[] getCanvas()
        {
            return _drawCanvas.ToArray();
        }

        /// <summary>
        /// get last line that was drawn by Drawer
        /// </summary>
        /// <returns></returns>
        public string GetLine()
        {
            return _drawLine;
        }

        /// <summary>
        /// calls SendLine Callback
        /// updated all guesser clients with the line that was just drawn by Drawer
        /// </summary>
        private void updateAllUsersCanvas()
        {
            try
            {
                foreach (var user in _userCallbacks.Where(x => !String.Equals(x.Key, _drawerUser, StringComparison.CurrentCultureIgnoreCase)))
                {
                    user.Value.SendLine(_drawLine);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error updating all clients .Error Message: " + ex.Message);
            }
        }

        #endregion

        #region DrawWordMethods

        /// <summary>
        /// gets the draw word hint for the cleitns guessing
        /// </summary>
        /// <returns></returns>
        public string GetWordHint()
        {
            return _drawWord.wordHintFirstLetter_;
        }

        /// <summary>
        /// gets the draw word and returns it
        /// </summary>
        /// <returns></returns>
        public string GetWord()
        {
            return _drawWord.word_;
        }

        /// <summary>
        /// word that is passed gets check if it matches the Draw Word
        /// </summary>
        /// <param name="word"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckWord(string word, string userName)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error checking word .Error Message: " + ex.Message);
            }
        }

        #endregion


    }
}
